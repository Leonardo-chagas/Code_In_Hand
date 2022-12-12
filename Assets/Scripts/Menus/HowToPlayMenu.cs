using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HowToPlayMenu : MonoBehaviour
{
    private int currentIndex = 0;
    private int tutorialAmount;

    public Image forwardImage;
    public Image backwardImage;
    public TMP_Text text;

    void OnEnable()
    {
        currentIndex = 0;
        tutorialAmount = transform.childCount-1;

        if(currentIndex >= tutorialAmount){
            forwardImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }

        if(currentIndex <= 0){
            backwardImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }

        text.text = $"{currentIndex+1}/{tutorialAmount+1}";
        ChangeTutorial();
    }

    
    private void ChangeTutorial(){
        foreach(Transform child in transform){
            child.gameObject.SetActive(false);
        }

        transform.GetChild(currentIndex).gameObject.SetActive(true);
    }

    public void Forward(){
        if(currentIndex >= tutorialAmount){
            forwardImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
            return;
        }
        backwardImage.color = new Color(1, 1, 1, 1);
        currentIndex++;
        ChangeTutorial();
        text.text = $"{currentIndex+1}/{tutorialAmount+1}";

        if(currentIndex >= tutorialAmount){
            forwardImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }

    public void Backward(){
        if(currentIndex <= 0){
            backwardImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
            return;
        }
        forwardImage.color = new Color(1, 1, 1, 1);
        currentIndex--;
        ChangeTutorial();
        text.text = $"{currentIndex+1}/{tutorialAmount+1}";

        if(currentIndex <= 0){
            backwardImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }
}
