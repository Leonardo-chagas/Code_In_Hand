using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public CardsScriptable Cards;
    void Start()
    {
        foreach(string key in ChallengeCard.instance.cardsToTake.Keys){
            foreach(GameObject card in Cards.cards){
                if(card.name == key){
                    for(int i = 0; i < ChallengeCard.instance.cardsToTake[key]; i++){
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
