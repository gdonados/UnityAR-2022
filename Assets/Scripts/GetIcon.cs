using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetIcon : MonoBehaviour
{
    public Force force;
    public Label label;
    public float confidence;
    public Image overlayImage;
    // Start is called before the first frame update
    void Start()
    {
        overlayImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
       overlayImage.sprite = OverlayAssign.GetActiveIcon(force, label, confidence);
       //GetComponent<Image>().sprite = OverlayAssign.getIcon("Friendly", "air", 90f);

    }
}
