using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider audioSlider, musicSlider;
    public Toggle fullscreenToggle;
    void Start()
    {
        audioSlider.value = PlayerPrefs.GetFloat("Audio Volume", 1);
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume", 1);
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1 ? true : false;
    }

    public void SetAudioVolume(){
        PlayerPrefs.SetFloat("Audio Volume", audioSlider.value);
        AudioListener.volume = audioSlider.value;
    }

    public void SetMusicVolume(){
        PlayerPrefs.SetFloat("Music Volume", musicSlider.value);
    }

    public void SetFullscreen(){
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, fullscreenToggle.isOn);
    }
}
