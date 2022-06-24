using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackedObjectManager : MonoBehaviour
{
    [SerializeField]
    private float globalx;
    [SerializeField]
    private float globaly;
    [SerializeField]
    private float globalWidth;
    [SerializeField]
    private float globalHeight;

    [SerializeField]
    private float newx;
    [SerializeField]
    private float newy;
    [SerializeField]
    private float newWidth;
    [SerializeField]
    private float newHeight;

    public bool newDataReceieved;

    public bool UpdateBoundingBoxes;



    public Texture DebugTexture;

    // Start is called before the first frame update
    void Start()
    {
        globalWidth = 420f;
        globalHeight = 421f;
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
            if(newDataReceieved)
            {
                StartCoroutine(PanBB(globalx, globaly, globalWidth, globalHeight, newx, newy, newWidth, newHeight, 1f));//1 second interval
                newDataReceieved = false;
            }
            transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector2(globalx - transform.GetComponent<RectTransform>().rect.width / 2,
                                                                                            transform.GetComponent<RectTransform>().rect.height -
                                                                                            globaly - transform.GetComponent<RectTransform>().rect.height / 2);
            transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(globalWidth, globalHeight);

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

    public void setBoundingBox(string inx, string iny, string inWidth, string inHeight, string label)
    {
        //Debug.Log("setting new values");
        //Debug.Log(newx);
        //Debug.Log(newy);
        Debug.Log(label);
        //Debug.Log(newWidth);
        //Debug.Log(newHeight);
        if (label.Trim() == "1")
        {
            newDataReceieved = true;

            float.TryParse(inx.Trim(), out newx);
            float.TryParse(iny.Trim(), out newy);

            float.TryParse(inWidth.Trim(), out newWidth);
            float.TryParse(inHeight.Trim(), out newHeight);

            Debug.Log("Left is: " + newx);
            Debug.Log("Bottom is: " + newy);

            Debug.Log("Width is: " + newWidth);
            Debug.Log("Height is " + newHeight);
        }
        //Debug.Log("after");

        //transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(int.Parse(x), int.Parse(y));
        //transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(int.Parse(width), int.Parse(height));
    }
    public IEnumerator PanBB(float oldx, float oldy, float oldwidth, float oldheight, float inNewx, float inNewy, float inNewwidth, float inNewheight, float interval)
    {
        float interpolant;
        //interval is time in seconds that this for loop will complete, time invariant.
        for (interpolant = 0f; interpolant < 1f; interpolant += Time.deltaTime / interval)//60 fps, time.delta = 1/60, 60 frames 
        {
            globalx = Mathf.Lerp(oldx, inNewx, interpolant);
            globaly = Mathf.Lerp(oldy, inNewy, interpolant);
            globalWidth = Mathf.Lerp(oldwidth, inNewwidth, interpolant);
            globalHeight = Mathf.Lerp(oldheight, inNewheight, interpolant);

            yield return null;
        }
        globalx = inNewx;
        globaly = inNewy;
        globalWidth = inNewwidth;
        globalHeight = inNewheight;
    }
}
