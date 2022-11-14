using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedIf : MonoBehaviour, IPlaceable
{
    private Transform droparea;
    public GameObject block;

    void Start()
    {
        droparea = GameObject.Find("DropArea").transform;
    }

    public void PlaceCard(){
        GameObject obj = Instantiate(block);
        obj.transform.SetParent(droparea);
    }
}
