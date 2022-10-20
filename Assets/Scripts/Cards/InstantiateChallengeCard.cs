using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateChallengeCard : MonoBehaviour
{
    
    void Start()
    {
        GameObject obj = Instantiate(GameManager.instance.challenge, transform.position, transform.rotation);
        obj.transform.SetParent(transform, false);
    }
}
