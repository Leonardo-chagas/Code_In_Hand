using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FitContent : MonoBehaviour
{
    private VerticalLayoutGroup layoutGroup;
    private float spacing;
    private float childHeight;
    private RectTransform rectTransform;
    
    void Start()
    {
        layoutGroup = GetComponent<VerticalLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        spacing = layoutGroup.spacing;
        childHeight = transform.GetChild(0).GetComponent<RectTransform>().rect.height;
    }

    public void Fit(float width){
        //float width = cardWidth + lineSpacing ;
        if(width >= rectTransform.rect.width)
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        float newHeight = (childHeight*transform.childCount) + (spacing*(transform.childCount-1));
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
    }
}
