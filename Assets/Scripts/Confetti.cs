using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    private ParticleSystem particleSystem;
    void Start(){
        particleSystem = GetComponent<ParticleSystem>();
    }

    void OnEnable(){
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
    }

    void OnDisable(){
        particleSystem.Stop();
    }
}
