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
    public bool isVariable = false;
    public GameObject inputCard;
    public TMP_InputField inputField;
    /* public enum ContentType {
        Standard,
        IntegerNumber,
        DecimalNumber
    } */
    public TMP_InputField.ContentType contentType;

    void Awake()
    {
        inputField = inputCard.transform.GetChild(2).GetComponent<TMP_InputField>();
        inputField.contentType = contentType;

        bigCardArea = GameObject.Find("BigCardArea").transform.GetChild(0);
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
        if(isVariable)
            GameManager.instance.showAvailableVariables = true;
        else
            GameManager.instance.showAvailableVariables = false;
        bigCardArea.gameObject.SetActive(true);
        GameObject obj = Instantiate(inputCard);
        obj.transform.SetParent(cardPoint, false);
        obj.transform.position = cardPoint.position;
        InputCard card = obj.GetComponent<InputCard>();
        card.color = color;
        card.cardText = cardText;
    }
}
