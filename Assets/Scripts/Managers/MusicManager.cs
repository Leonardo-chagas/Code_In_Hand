using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public static MusicManager instance;

    void Awake()
    {
        if(instance == null){
            DontDestroyOnLoad(transform);
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void PlayMusic(){
        audioSource.Play();
    }

    public void StopMusic(){
        audioSource.Stop();
    }

    public void ChangeMusic(AudioClip music){
        audioSource.clip = music;
    }

    public void FullVolume(){
        audioSource.volume = 1;
    }

    public void Mute(){
        audioSource.volume = 0;
    }

    public void ChangeVolume(float volume){
        audioSource.volume = volume;
    }

    public void FadeIn(){
        float timeToFade = 1;
        float timeElapsed = 0;

        while(timeElapsed < timeToFade){
            audioSource.volume = Mathf.Lerp(0, PlayerPrefs.GetFloat("MusicVolume", 1), timeElapsed/timeToFade);
            timeElapsed += Time.deltaTime;
        }
    }

    public void FadeOut(){
        float timeToFade = 1;
        float timeElapsed = 0;

        while(timeElapsed < timeToFade){
            audioSource.volume = Mathf.Lerp(PlayerPrefs.GetFloat("MusicVolume", 1), 0, timeElapsed/timeToFade);
            timeElapsed += Time.deltaTime;
        }
    }
}
