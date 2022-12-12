using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRemover : MonoBehaviour
{
    private Transform playerArea;
    void Start()
    {
        playerArea = GameObject.Find("PlayerArea").transform;
    }

    public void RemoveCards(){
        foreach(Transform child in transform){
            if(child.gameObject.layer == LayerMask.NameToLayer("Card")){
                child.SetParent(playerArea, false);
                print("tirando cartas de linha");
            }
            else{
                child.gameObject.GetComponent<CardRemover>()?.RemoveCards();
                print("removendo linha");
            }
        }
        Destroy(gameObject);
    }
}
