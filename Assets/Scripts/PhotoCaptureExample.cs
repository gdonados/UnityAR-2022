using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Windows.WebCam;

public class PhotoCaptureExample : MonoBehaviour
{
    PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;
    public MQTTSendImage MQTTSend;
    public int delay;
    public Resolution cameraResolution;

    // Use this for initialization
    void Start()
    {
        cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).Last();
        targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);
    }

    private void Update()
    {
        if (delay == 120)
        {
            /*String bottom = UnityEngine.Random.Range(0f, 720f).ToString();
            String left = UnityEngine.Random.Range(0f, 1080f).ToString();
            String width = UnityEngine.Random.Range(100f, 200f).ToString();
            String height = UnityEngine.Random.Range(100f, 200f).ToString();
            //String label = "1";


            TOM.setBoundingBox(bottom, left, width, height, ((int)Time.time%2).ToString());*/
            //StartCoroutine(CaptureImage());
            TakePhoto();
            delay = 0;
        }
        delay++;
    }

    public void TakePhoto()
    {
        // Create a PhotoCapture object
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject) {
            photoCaptureObject = captureObject;
            CameraParameters cameraParameters = new CameraParameters();
            cameraParameters.hologramOpacity = 0.0f;
            cameraParameters.cameraResolutionWidth = 1280;//cameraResolution.width;
            cameraParameters.cameraResolutionHeight = 720;// cameraResolution.height;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

            // Activate the camera
            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result) {
            // Take a picture
            photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
            });
        });
    }
    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        // Copy the raw image data into our target texture
        photoCaptureFrame.UploadImageDataToTexture(targetTexture);
        StartCoroutine(MQTTSend.CaptureImage(targetTexture, 1280f, 720f));//cameraResolution.width, cameraResolution.height));

        // Create a gameobject that we can apply our texture to
        //GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        //Renderer quadRenderer = quad.GetComponent<Renderer>() as Renderer;
        //quadRenderer.material = new Material(Shader.Find("Unlit/Texture"));

        //quad.transform.parent = this.transform;
        //quad.transform.localPosition = new Vector3(0.0f, 0.0f, 3.0f);

        //quadRenderer.material.SetTexture("_MainTex", targetTexture);

        // Deactivate our camera
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        // Shutdown our photo capture resource
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }
}