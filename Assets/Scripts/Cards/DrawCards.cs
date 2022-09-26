using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public CardsScriptable Cards;
    void Start()
    {
        int cont = 0;
        foreach(GameObject card in Cards.cards){
            for(int i = 0; i < GameManager.instance.cardsToDraw[cont]; i++){
                GameObject obj = Instantiate(card, transform.position, transform.rotation);
                obj.transform.SetParent(transform, false);
            }
            cont += 1;
        }
    }

    
    void Update()
    {
        
    }
}
