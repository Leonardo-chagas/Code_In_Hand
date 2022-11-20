using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputCard : MonoBehaviour
{
    public static InputCard instance;
    GameObject bigCardArea;
    public Color color;
    public Image borders;
    public TMP_Text text;
    public TMP_Text cardText;

    void Start()
    {
        instance = this;
        borders.color = color;
        text.color = color;
        bigCardArea = GameObject.Find("BigCardArea").transform.GetChild(0).gameObject;
    }

    public void ConfirmText(string varText){
        //define o texto da carta
        if(varText != "" && varText != null){
            cardText.text = varText;
        }
        else{
            cardText.text = text.text;
        }
        //adiciona a variável para o dicionário
        if(!GameManager.instance.variables.ContainsKey(cardText.text)){
            GameManager.instance.variables.Add(cardText.text, 1);
        }
        else{
            GameManager.instance.variables[cardText.text] = GameManager.instance.variables[cardText.text] + 1;
        }

        Destroy(gameObject, 0.2f);
        bigCardArea.SetActive(false);
    }
}
