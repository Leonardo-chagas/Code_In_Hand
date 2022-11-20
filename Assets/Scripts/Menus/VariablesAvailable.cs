using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesAvailable : MonoBehaviour
{
    //public static VariablesAvailable instance;
    public GameObject varCard;

    void Start()
    {
        //instance = this;
    }

    private void OnEnable(){
        if(GameManager.instance.showAvailableVariables){
            ShowVariables();
        }
    }

    private void OnDisable(){
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
    }

    private void ShowVariables(){
        foreach(string cardText in GameManager.instance.variables.Keys){
            GameObject obj = Instantiate(varCard, transform);
            obj.GetComponent<VariableOption>().text.text = cardText;
        }
    }
}
