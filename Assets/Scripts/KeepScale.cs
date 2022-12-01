using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepScale : MonoBehaviour
{
    public Vector3 scale = new Vector3(1,1,1);
    void Start()
    {
        transform.localScale = scale;
    }

    
    void Update()
    {
        if(transform.localScale != scale){
            transform.localScale = scale;
        }
    }
}
