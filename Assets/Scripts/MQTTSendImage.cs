using System;
using System.Threading;
using System.Collections;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using UnityEngine;
using System.IO;
using System.Text;
using TMPro;
using UnityEditor;

public class MQTTSendImage : MonoBehaviour
{
    public MQTTReceiveHR MQTTHR;
    public TrackedObjectManager TOM;

    String IP;
    MqttFactory factory;
    public IMqttClient mqttClient;
    IMqttClientOptions options;
    AutoResetEvent semaphore;
    TimeSpan receiveTimeout;

    public int delay;
    byte[] imageToSend;
    byte[] data;
    //================
    public bool newData;
    public bool connected;

    void Start()
    {
        delay = 0;
        newData = true; //Informs internal code when new message comes in that needs to be procesed

        connected = false;
        //This will allow us to send another image to the python IDE for processing

        //----------
        String ValidIP = "10.38.4.212"; //########### This needs to be updated to current ip when testing!

        factory = new MqttFactory();
        mqttClient = factory.CreateMqttClient();
        options = new MqttClientOptionsBuilder()
                .WithClientId(UnityEngine.Random.Range(1000, 2000).ToString())
                .WithTcpServer(ValidIP, 1883)
                .WithCleanSession()
                .Build();

        mqttClient.ConnectAsync(options, CancellationToken.None);

        semaphore = new AutoResetEvent(false);
        receiveTimeout = TimeSpan.FromSeconds(0.02f);

        mqttClient.UseConnectedHandler(async e =>
        {
            Debug.Log("### CONNECTED TO MQTT CLIENT ###");
            connected = true;
            // Subscribe to a topic
            await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("HeartRate").Build());
            await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("BoundingBox").Build());


            Debug.Log("###SUBSCRIBED TO Temperature Topic ###");
        });

        mqttClient.UseDisconnectedHandler(async e =>
        {
            Console.WriteLine("### DISCONNECTED FROM SERVER ###");
            /*try
            {
                await mqttClient.ConnectAsync(options, CancellationToken.None); // Since 3.0.5 with CancellationToken
            }
            catch
            {
                Console.WriteLine("### RECONNECTING FAILED ###");
            }*/
        });

        //---------------------------------------------------------------------------------

        mqttClient.UseApplicationMessageReceivedHandler(e => //Recieves data packet
        {
            semaphore.Set();
            //Debug.Log("Recieved new message");
            //Debug.Log(e.ApplicationMessage.Topic);
            if (e.ApplicationMessage.Payload != null)
            {
                data = e.ApplicationMessage.Payload;
                //Debug.Log(e.ApplicationMessage.Topic);
                if (e.ApplicationMessage.Topic == "HeartRate")
                {
                    //Debug.Log("Heartrate message recieved");
                    //Debug.Log(System.Text.Encoding.UTF8.GetString(data));
                    MQTTHR.SetHR(System.Text.Encoding.UTF8.GetString(data));
                }
                if (e.ApplicationMessage.Topic == "TEMPERATURE")
                {
                    //Debug.Log("Temp topic recieved");
                    //Debug.Log(System.Text.Encoding.UTF8.GetString(data));
                }
                if (e.ApplicationMessage.Topic == "BoundingBox")
                {
                    newData = true;
                    data = e.ApplicationMessage.Payload;
                    //Debug.Log(System.Text.Encoding.UTF8.GetString(data));
                    string[] BBData = (System.Text.Encoding.UTF8.GetString(data)).Split("--");
                    /*foreach (string datum in BBData)
                    {
                        Debug.Log(datum);
                    }*/
                    //Grows from bottom left. Index 2, 5, 6, 7, 1 will be sent. (Left, Bottom, Width, Height, ClassID)
                    //Debug.Log(BBData[3].Split(":")[1]);//Left
                    //Debug.Log(BBData[6].Split(":")[1]);//Bottom
                    //Debug.Log(BBData[7].Split(":")[1]);//Width
                    //Debug.Log(BBData[8].Split(":")[1]);//Height

                    TOM.setBoundingBox(BBData[3].Split(":")[1], BBData[6].Split(":")[1], BBData[7].Split(":")[1], BBData[8].Split(":")[1], BBData[1].Split(":")[1]);
                    //semaphore.Set();
                    /* Raw message string.
                     * <jetson.inference.Detection object> 0th index
                    -- ClassID: 3 //1 is person
                    -- Confidence: 0.581882
                    -- Left: 1671.0
                    -- Top: 435.0
                    -- Right: 1933.0
                    -- Bottom: 695.0
                    -- Width: 262.0
                    -- Height: 260.0
                    -- Area: 68120.0
                    -- Center: (1802.0, 565.0)
                    */
                }
            }

        }); //end of mqttClient.UseApplicationMessageReceivedHandler

    }

    // Update is called once per frame
    void Update()
    {
        semaphore.WaitOne((int)receiveTimeout.TotalMilliseconds, true);
        
        //Debug.Log(newData);
        if (data == null)
        {
            //Debug.Log("null data");
        }
        if (delay == 120)
        {
            /*String bottom = UnityEngine.Random.Range(0f, 720f).ToString();
            String left = UnityEngine.Random.Range(0f, 1080f).ToString();
            String width = UnityEngine.Random.Range(100f, 200f).ToString();
            String height = UnityEngine.Random.Range(100f, 200f).ToString();
            //String label = "1";


            TOM.setBoundingBox(bottom, left, width, height, ((int)Time.time%2).ToString());*/
            //StartCoroutine(CaptureImage());
            delay = 0;
        }
        delay++;
    }

    public IEnumerator CaptureImage(Texture2D inTexture2D, float width, float height) //Capture image from camera and set bytes to imageToSend
    {
        //var imagePath = @"C:\Users\grant\Documents\College\Grad\MLImages";

        //RenderTexture rt = new RenderTexture(width, height, 24);
        //imageCaptureCam.targetTexture = rt;
        //Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        yield return new WaitForEndOfFrame();//Make sure the frame has ended before capturing the texture;

        //imageCaptureCam.Render();
        //RenderTexture.active = rt;

        //inTexture2D.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        inTexture2D.Apply();

        byte[] bytes; //these need to be sent
        bytes = inTexture2D.EncodeToPNG();

        imageToSend = bytes;
        //send these bytes
        //System.IO.File.WriteAllBytes(tempPath, bytes);

        StartCoroutine(SendData());
    }

    public IEnumerator SendData() //Send the data
    {
        if (mqttClient.IsConnected)
        {
            var message = new MqttApplicationMessageBuilder()
            .WithTopic("ImageData") //Publish message to Imagedata topic
            .WithPayload(imageToSend)
            .Build();
            mqttClient.PublishAsync(message, CancellationToken.None);
            //mqttClient.PublishAsync(Time.t, CancellationToken.None);

            Debug.Log("Sending image to python");
        }

        yield return new WaitForSeconds(0.1f);
    }

}
/*
[InitializeOnLoad]
public static class PlayStateNotifier
{

    static PlayStateNotifier()
    {
        EditorApplication.playModeStateChanged += ModeChanged;
    }

    static void ModeChanged(PlayModeStateChange playModeState)
    {
        if (playModeState == PlayModeStateChange.ExitingPlayMode)
        {
            GameObject.Find("MQTT").GetComponent<MQTTSendImage>().mqttClient.Dispose();
            Debug.Log("Exiting play mode.");
        }
    }
}*/












