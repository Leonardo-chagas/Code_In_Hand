using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputCard : MonoBehaviour
{
    GameObject bigCardArea;
    public Color color;
    public Image borders;
    public TMP_Text text;
    public TMP_Text cardText;

    void Start()
    {
        borders.color = color;
        text.color = color;
        bigCardArea = GameObject.Find("BigCardArea");
    }

    public void ConfirmText(){
        cardText.text = text.text;
        Destroy(gameObject, 0.2f);
        bigCardArea.SetActive(false);
    }
}
