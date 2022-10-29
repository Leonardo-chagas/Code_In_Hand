using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public CardsScriptable Cards;
    void Start()
    {
        foreach(string key in GameManager.instance.cardsToDraw.Keys){
            foreach(GameObject card in Cards.cards){
                if(card.name == key){
                    for(int i = 0; i < GameManager.instance.cardsToDraw[key]; i++){
                        GameObject obj = Instantiate(card, transform.position, transform.rotation);
                        obj.transform.SetParent(transform, false);
                    }
                    break;
                }
            }
        }
    }

    
    void Update()
    {
        
    }
}
