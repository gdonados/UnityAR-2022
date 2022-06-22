using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackedObjectManager : MonoBehaviour
{
    [SerializeField]
    private float x;
    [SerializeField]
    private float y;
    [SerializeField]
    private float width;
    [SerializeField]
    private float height;

    public bool UpdateBoundingBoxes;



    public Texture DebugTexture;

    // Start is called before the first frame update
    void Start()
    {
        width = 420f;
        height = 421f;
        UpdateBoundingBoxes = true;
        foreach (Transform child in transform)
        {

            child.transform.GetChild(0).GetComponent<Image>().color = Color.white;//new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        }

    }

    // Update is called once per frame
    void Update()
    {

        if (UpdateBoundingBoxes == true)
        {
            transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector2(x - transform.GetComponent<RectTransform>().rect.width / 2,
                                                                                            transform.GetComponent<RectTransform>().rect.height -
                                                                                            y - transform.GetComponent<RectTransform>().rect.height / 2);
            transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            foreach (Transform child in transform)
            {
                //boundingBoxImage.rectTransform.position = new Vector3(mop.GetBoundingBoxDimensions_Rect().x, mop.GetBoundingBoxDimensions_Rect().y, 0);
                //child.GetComponent<RectTransform>().position = new Vector2(bbt.mop.objectRect.x, bbt.mop.objectRect.y);
                //child.GetComponent<RectTransform>().sizeDelta = new Vector2(bbt.mop.objectRect.width, bbt.mop.objectRect.height);


                //  .position = new Vector3(bbt.mop.GetScreenspacePosition().x, bbt.mop.GetScreenspacePosition().y, 0);
                //bbt.boundingBoxImage.rectTransform.sizeDelta = new Vector2(bbt.mop.GetBoundingBoxDimensions_Rect().width, bbt.mop.GetBoundingBoxDimensions_Rect().height);
            }

        }

        //boundingBoxImage.rectTransform.position = new Vector3(mop.GetBoundingBoxDimensions_Rect().x, mop.GetBoundingBoxDimensions_Rect().y, 0);
        //boundingBoxImage.rectTransform.position = new Vector3(mop.GetScreenspacePosition().x, mop.GetScreenspacePosition().y, 0);
        //boundingBoxImage.rectTransform.sizeDelta = new Vector2(mop.GetBoundingBoxDimensions_Rect().width, mop.GetBoundingBoxDimensions_Rect().height);

    }

    /*private void OnGUI()    //Debug Graphics
    {
        foreach (BoundingBoxTrack bbt in boundingBoxTracks) 
        {
            GUI.DrawTexture(new Rect(bbt.mop.rectPosition.x, Screen.height - bbt.mop.rectPosition.y, bbt.mop.sizeDelta.x, bbt.mop.sizeDelta.y), DebugTexture); //Origin is top left.
        }
    }*/

    public void setBoundingBox(string newx, string newy, string newWidth, string newHeight)
    {
        //Debug.Log("setting new values");
        //Debug.Log(newx);
        //Debug.Log(newy);

        //Debug.Log(newWidth);
        //Debug.Log(newHeight);

        float.TryParse(newx.Trim(), out x);
        float.TryParse(newy.Trim(), out y);

        float.TryParse(newWidth.Trim(), out width);
        float.TryParse(newHeight.Trim(), out height);

        Debug.Log("Left is: " + x);
        Debug.Log("Bottom is: " + y);

        Debug.Log("Width is: " + width);
        Debug.Log("Height is " + height);
        //Debug.Log("after");

        //transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(int.Parse(x), int.Parse(y));
        //transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(int.Parse(width), int.Parse(height));
    }
}
