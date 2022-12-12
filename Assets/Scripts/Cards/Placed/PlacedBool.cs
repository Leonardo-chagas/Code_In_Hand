using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlacedBool : MonoBehaviour, IPlaceable
{
    private Transform bigCardArea;
    private Transform cardPoint;
    private TMP_Text cardText;
    public GameObject inputCard;
    void Awake()
    {
        bigCardArea = GameObject.Find("BigCardArea").transform.GetChild(0);
        cardPoint = bigCardArea.GetChild(1);
        foreach(Transform child in transform){
            if(child.name == "type"){
                cardText = child.gameObject.GetComponent<TMP_Text>();
            }
        }
    }

    public void PlaceCard(){
        GameManager.instance.showAvailableVariables = false;
        bigCardArea.gameObject.SetActive(true);

        GameObject card1 = Instantiate(inputCard);
        card1.transform.SetParent(cardPoint, false);
        card1.transform.position = cardPoint.position + Vector3.right*150;
        InputBoolCard input1 = card1.GetComponent<InputBoolCard>();
        input1.cardText = cardText;
        input1.text.text = "false";

        GameObject card2 = Instantiate(inputCard);
        card2.transform.SetParent(cardPoint, false);
        card2.transform.position = cardPoint.position + Vector3.left*150;
        InputBoolCard input2 = card2.GetComponent<InputBoolCard>();
        input2.cardText = cardText;
        input2.text.text = "true";
    }
}
