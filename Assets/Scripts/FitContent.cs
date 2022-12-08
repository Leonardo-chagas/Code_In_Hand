using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FitContent : MonoBehaviour
{
    private VerticalLayoutGroup layoutGroup;
    private float spacing;
    private float childHeight;
    private float paddingLeft;
    private RectTransform rectTransform;
    
    void Start()
    {
        layoutGroup = GetComponent<VerticalLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        spacing = layoutGroup.spacing;
        childHeight = transform.GetChild(0).GetComponent<RectTransform>().rect.height;
        paddingLeft = layoutGroup.padding.left;
    }

    public void Fit(float width=0){
        int childrenAmount = LineCount();
        childHeight = transform.GetChild(0).GetComponent<RectTransform>().rect.height;
        rectTransform = GetComponent<RectTransform>();
        if(width >= rectTransform.rect.width)
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width+paddingLeft);
        float newHeight = (childHeight*childrenAmount) + (spacing*(childrenAmount-1));
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
    }

    public int LineCount(){
        int count = 0;
        foreach(Transform child in transform){
            if(child.gameObject.name.StartsWith("Line"))
                count++;
            else{
                count += child.gameObject.GetComponent<FitContent>().LineCount();
            }
        }
        return count;
    }
}
