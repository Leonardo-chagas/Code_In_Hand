using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChallengeSelect : MonoBehaviour
{
    private String dataPath;
    public string challengeToComplete;
    public GameObject challengeCard;
    public Transform challengePosition;
    public Color completedColor;
    private bool unlocked = true;
    private string challengeId;
    private Button button;
    private Image image;
    private EventTrigger eventTrigger;
    
    
    void Awake(){

        button = GetComponent<Button>();
        image = GetComponent<Image>();
        eventTrigger = GetComponent<EventTrigger>();

        button.onClick.AddListener(SelectChallenge);

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => {ShowChallenge();});
        eventTrigger.triggers.Add(entry);

        challengeId = challengeCard.GetComponent<ChallengeCard>()?.id;
    }

    void OnEnable(){
        dataPath = "Assets/Resources/data.txt";

        var lines = File.ReadAllLines(dataPath);
        foreach(string line in lines){
            if(line.StartsWith(challengeId)){
                string[] currentLine = line.Split("|", System.StringSplitOptions.None);
                if(currentLine[2] == "true"){
                    image.color = completedColor;
                }
            }

            if(!string.IsNullOrEmpty(challengeToComplete) && !string.IsNullOrWhiteSpace(challengeToComplete)){
                if(line.StartsWith(challengeToComplete)){
                    string[] currentLine = line.Split("|", System.StringSplitOptions.None);
                    if(currentLine[2] == "false"){
                        unlocked = false;
                        image.color = new Color(0.5f, 0.5f, 0.5f, image.color.a);
                    }
                    else{
                        unlocked = true;
                        image.color = new Color(1, 1, 1, image.color.a);
                    }
                }
            }
        }
    }
    
    public void SelectChallenge(){
        if(!unlocked) return;
        GameManager.instance.challenge = challengeCard;
        SceneManager.LoadScene("Game");
    }

    public void ShowChallenge(){
        if(!unlocked) return;
        if(challengePosition.childCount > 0){
            Destroy(challengePosition.GetChild(0).gameObject);
        }
        GameObject challenge = Instantiate(challengeCard);
        challenge.transform.SetParent(challengePosition, false);
        challenge.transform.position = challengePosition.position;
        challenge.transform.localScale = new Vector3(challenge.transform.localScale.x*2, challenge.transform.localScale.y*2, challenge.transform.localScale.z);
    }
}
