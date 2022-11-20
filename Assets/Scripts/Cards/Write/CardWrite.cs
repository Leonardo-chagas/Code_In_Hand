using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardWrite : MonoBehaviour, IWritable
{
    private string cardText;
    /* private string path; */
    public string appendLeft = "";
    public string appendRight = "";
    public bool isVariable = false;
    /* void Start()
    {
        path = "Assets/Resources/programm.txt";
    } */

    public void WriteCode(StreamWriter writer){
        foreach(Transform child in transform){
            if(child.name == "type"){
                cardText = child.gameObject.GetComponent<TMP_Text>().text;
            }
        }
        //adicionar variável para dicionário ao rodar programa?
        /* if(isVariable && !GameManager.instance.variables.ContainsKey(cardText)){
            GameManager.instance.variables.Add(cardText, 1);
        } */
        writer.Write(appendLeft + cardText.Replace("\u200B", "") + appendRight);
    }
}
