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
                if(cardText == "PRINT"){
                    appendRight = "(";
                    /* int mask2 = 1 << LayerMask.NameToLayer("Card");
                    RaycastHit2D[] checkCardRight = Physics2D.RaycastAll(transform.position, Vector2.right, 10000, mask2);
                    CardWrite cardWrite = checkCardRight[checkCardRight.Length - 1].collider.gameObject.GetComponent<CardWrite>();
                    cardWrite.appendRight = cardWrite.appendRight + ")"; */
                    AreaWrite areaWrite = transform.parent.GetComponent<AreaWrite>();
                    areaWrite.appendRight = ")" + areaWrite.appendRight;
                }
                if(cardText == "IF" || cardText == "ELSE"){
                    appendRight = "(";
                    AreaWrite areaWrite = transform.parent.GetComponent<AreaWrite>();
                    areaWrite.appendRight = ")";
                }
            }
        }
        //adicionar variável para dicionário ao rodar programa?
        /* if(isVariable && !GameManager.instance.variables.ContainsKey(cardText)){
            GameManager.instance.variables.Add(cardText, 1);
        } */
        /* appendLeft = transform.GetSiblingIndex() > 0 ? " " : "";
        appendRight = transform.GetSiblingIndex() < transform.parent.childCount - 2 ? " " : ""; */
        writer.Write(appendLeft + cardText.Replace("\u200B", "") + appendRight);
    }
}
