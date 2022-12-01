using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvenChildAmount : MonoBehaviour
{
    private GameObject currentObj;
    private RectTransform rectTransform;
    private HorizontalLayoutGroup layoutGroup;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
    }

    public void KeepEvenChildAmount(){
        int childAmount = transform.childCount;
        if(childAmount%2 != 0){
            GameObject obj = new GameObject("padding object");
            obj.AddComponent<RectTransform>();
            obj.transform.SetParent(transform);
            currentObj = obj;
        }
        else{
            if(currentObj)
                Destroy(currentObj);
        }
    }

    public void ChangePos(){
        int childAmount = transform.childCount;
        print(childAmount);
        if(childAmount%2 != 0){
            rectTransform.anchoredPosition += Vector2.right*100;
            rectTransform.localPosition += Vector3.right*100;
            rectTransform.position += Vector3.right*100;
            layoutGroup.spacing = layoutGroup.spacing;
            print("número impar de crianças");
        }
        else{
            
        }
    }
}
