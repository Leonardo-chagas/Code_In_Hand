using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Slider audioSlider, musicSlider;
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;

    public Resolution[] resolutions;
    public AudioSettingsScriptable audioSettings;

    void Start()
    {
        audioSlider.value = PlayerPrefs.GetFloat("Audio Volume", 1);
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume", 1);
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1 ? true : false;
    }

    void OnEnable(){
        resolutions = Screen.resolutions;
        Array.Reverse(resolutions);
        foreach(Resolution resolution in resolutions){
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolution.ToString()));
        }
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", 0);
    }

    public void SetAudioVolume(){
        PlayerPrefs.SetFloat("Audio Volume", audioSlider.value);
        audioSettings.volume = audioSlider.value;
    }

    public void SetMusicVolume(){
        PlayerPrefs.SetFloat("Music Volume", musicSlider.value);
        MusicManager.instance.ChangeVolume(musicSlider.value);
    }

    public void SetFullscreen(){
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, fullscreenToggle.isOn);
    }

    public void SetResolution(){
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("Resolution Width", resolutions[resolutionDropdown.value].width);
        PlayerPrefs.SetInt("Resolution Height", resolutions[resolutionDropdown.value].height);
    }
}
