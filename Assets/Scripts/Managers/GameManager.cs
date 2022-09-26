using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;
    public AudioSettingsScriptable audioSettings;

    public int[] cardsToDraw;
    void Start()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
        audioSettings.volume = PlayerPrefs.GetFloat("Audio Volume");
        MusicManager.instance.ChangeVolume(PlayerPrefs.GetFloat("Music Volume", 1));
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1 ? true : false;
        int width = PlayerPrefs.GetInt("Resolution Width", Screen.width);
        int height = PlayerPrefs.GetInt("Resolution Height", Screen.height);
        Screen.SetResolution(width, height, isFullscreen);
    }

    
}
