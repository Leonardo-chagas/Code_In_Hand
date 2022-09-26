using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cards", menuName = "ScriptableObjects/Cards", order = 2)]
public class CardsScriptable : ScriptableObject
{
    public GameObject[] cards;
}
