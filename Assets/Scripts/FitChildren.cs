using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitChildren : MonoBehaviour
{
    private FitContent fitContent;
    void Start(){
        fitContent = GetComponent<FitContent>();
    }

    public void Fit(float width){
        fitContent = GetComponent<FitContent>();
        fitContent.Fit(width);
        foreach(Transform child in transform){
            if(!child.gameObject.name.StartsWith("Line")){
                child.gameObject.GetComponent<FitChildren>()?.Fit(width);
            }
        }
    }
}
