using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedIf : MonoBehaviour, IPlaceable
{
    private Transform droparea;
    public GameObject block;

    void Start()
    {
        //droparea = GameObject.Find("DropArea").transform;
        //droparea = GetComponent<DragDrop>().droparea.transform;
    }

    public void PlaceCard(){
        droparea = GetComponent<DragDrop>().dropZoneParent;
        GameObject obj = Instantiate(block);
        obj.transform.SetParent(droparea.parent, false);
        obj.transform.SetSiblingIndex(droparea.GetSiblingIndex()+2);
    }
}
