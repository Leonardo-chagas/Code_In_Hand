using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputBoolCard : MonoBehaviour
{
    private GameObject bigCardArea;
    public TMP_Text text;
    public TMP_Text cardText;
    void Start()
    {
        bigCardArea = GameObject.Find("BigCardArea").transform.GetChild(0).gameObject;
    }

    void OnDisable(){
        Destroy(gameObject);
    }

    public void ConfirmBool(){
        cardText.text = text.text;
        bigCardArea.SetActive(false);
    }
}
