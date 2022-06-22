using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MQTTReceiveHR : MonoBehaviour
{
    public TextMeshProUGUI HR;
    public string myHRString;
    // Start is called before the first frame update
    void Start()
    {
        HR = transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        HR.text = "HR: " + myHRString;
        HR.color = Color.Lerp(Color.white, Color.red, 1f - (120f - float.Parse(myHRString)) / 50f);//120 is max, 50 is range.
    }

    public void SetHR(string HRString)
    {
        myHRString = HRString;
        //Debug.Log(HRString);
    }
}
