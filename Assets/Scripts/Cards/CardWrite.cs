using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardWrite : MonoBehaviour, IWritable
{
    private string cardText;
    private string path;
    public string appendLeft = "";
    public string appendRight = "";
    void Start()
    {
        path = "Assets/Resources/programm.txt";
    }

    public void WriteCode(StreamWriter writer){
        foreach(Transform child in transform){
            if(child.name == "type"){
                cardText = child.gameObject.GetComponent<TMP_Text>().text;
            }
        }
        
        writer.Write(appendLeft + cardText + appendRight);
    }
}
