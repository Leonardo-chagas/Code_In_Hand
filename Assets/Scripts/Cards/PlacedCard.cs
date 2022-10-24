using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlacedCard : MonoBehaviour, IPlaceable
{
    private Transform bigCardArea;
    private Transform cardPoint;
    private TMP_Text cardText;
    private Color color;
    public GameObject inputCard;

    void Awake()
    {
        bigCardArea = GameObject.Find("BigCardArea").transform;
        cardPoint = bigCardArea.GetChild(1);
        foreach(Transform child in transform){
            if(child.name == "type"){
                cardText = child.gameObject.GetComponent<TMP_Text>();
            }
            if(child.name == "borders"){
                color = child.gameObject.GetComponent<Image>().color;
            }
        }
    }

    public void PlaceCard(){
        bigCardArea.gameObject.SetActive(true);
        GameObject obj = Instantiate(inputCard);
        obj.transform.SetParent(cardPoint, false);
        obj.transform.position = cardPoint.position;
        InputCard card = obj.GetComponent<InputCard>();
        card.color = color;
        card.cardText = cardText;
    }
}
