using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChallengeSelect : MonoBehaviour
{
    [Serializable]
    public struct Cards{
        public string cardName;
        public int cardAmount;
    }
    public Cards[] cards;
    public GameObject challengeCard;
    public Transform challengePosition;
    Button button;
    EventTrigger eventTrigger;
    public Dictionary<string, int> cardsToTake = new Dictionary<string, int>();
    
    void Start(){
        button = GetComponent<Button>();
        eventTrigger = GetComponent<EventTrigger>();
        button.onClick.AddListener(SelectChallenge);
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => {ShowChallenge();});
        eventTrigger.triggers.Add(entry);

        foreach(Cards card in cards){
            cardsToTake.Add(card.cardName, card.cardAmount);
        }
    }
    public void SelectChallenge(){
        GameManager.instance.challenge = challengeCard;
        GameManager.instance.cardsToDraw = cardsToTake;
        SceneManager.LoadScene("Game");
    }

    public void ShowChallenge(){
        if(challengePosition.childCount > 0){
            Destroy(challengePosition.GetChild(0).gameObject);
        }
        GameObject challenge = Instantiate(challengeCard);
        challenge.transform.SetParent(challengePosition, false);
        challenge.transform.position = challengePosition.position;
        challenge.transform.localScale = new Vector3(challenge.transform.localScale.x*2, challenge.transform.localScale.y*2, challenge.transform.localScale.z);
    }
}
