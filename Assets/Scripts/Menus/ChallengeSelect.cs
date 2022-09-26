using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChallengeSelect : MonoBehaviour
{
    public int[] cardsToTake;
    Button button;
    
    void Start(){
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectChallenge);
    }
    public void SelectChallenge(){
        GameManager.instance.cardsToDraw = cardsToTake;
        SceneManager.LoadScene("Game");
    }
}
