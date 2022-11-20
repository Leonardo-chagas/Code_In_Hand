using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VariableOption : MonoBehaviour
{
    public TMP_Text text;

    public void Selected(){
        InputCard.instance.ConfirmText(text.text);
    }
}
