using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputCard : MonoBehaviour
{
    private List<char> floatChars = new List<char>{'1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.', '-'};
    private bool isFloat = false;
    public static InputCard instance;
    GameObject bigCardArea;
    public Color color;
    public Image borders;
    public TMP_Text text;
    public TMP_Text cardText;
    public TMP_InputField inputField;

    void Start()
    {
        instance = this;
        borders.color = color;
        text.color = color;
        bigCardArea = GameObject.Find("BigCardArea").transform.GetChild(0).gameObject;
        if(inputField.contentType == TMP_InputField.ContentType.DecimalNumber){
            inputField.contentType = TMP_InputField.ContentType.Standard;
            inputField.onValueChanged.AddListener(delegate {FloatText();});
            isFloat = true;
        }
    }

    public void ConfirmText(string varText){
        //formata o texto de float
        if(isFloat){
            if(!inputField.text.Contains('.')){
                inputField.text = inputField.text + ".0";
            }
            else{
                string[] splitText = inputField.text.Split('.');
                string first = splitText[0];
                string second = splitText.Length > 1 ? splitText[1] : "0";
                print(first);
                print(second);
                inputField.text = string.Format("{0}.{1}", first, second);
            }
        }

        //define o texto da carta
        if(varText != "" && varText != null){
            cardText.text = varText;
        }
        else{
            cardText.text = inputField.text;
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

    public void FloatText(){
        if(inputField.text.Length > 0){
            if(!floatChars.Contains(inputField.text[inputField.text.Length - 1]) ||
            inputField.text.Count(f => f == '.') > 1 ||
            (inputField.text[inputField.text.Length - 1] == '-' && inputField.text.Length > 1) ||
            inputField.text[0] == '.'){
                inputField.text = inputField.text.Remove(inputField.text.Length - 1, 1);
            }
        }
    }
}
