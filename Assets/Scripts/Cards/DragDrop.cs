using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DragDrop : MonoBehaviour
{
    [HideInInspector] public bool isDragging = false;
    private Transform startParent;
    private Vector3 startPosition;
    private Transform dropArea;
    private Transform dropZone;
    private Transform dropZoneParent;
    private TMP_Text text;
    private string startText;
    
    void Start()
    {
        //startPosition = transform.position;
        dropArea = GameObject.Find("DropArea").transform;
        StartCoroutine("DefineStart");
        foreach(Transform child in transform){
            if(child.name == "type"){
                text = child.gameObject.GetComponent<TMP_Text>();
                startText = text.text;
            }
        }
    }

    IEnumerator DefineStart(){
        yield return new WaitForSeconds(0.05f);
        startPosition = transform.position;
        startParent = transform.parent;
    }

    
    void Update()
    {
       if(isDragging){
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
       } 
    }

    public void StartDrag(){
        isDragging = true;
        if(transform.parent != startParent){
            Dropzone.instance.CardRemoved(transform);
            text.text = startText;
            print("passou");
        }
        //startPosition = transform.position;
        //startParent = transform.parent.gameObject;
    }

    public void StopDrag(){
        isDragging = false;
        if(dropZone != null){
            //transform.SetParent(dropZoneParent, false);
            //transform.position = dropZone.position;
            Dropzone.instance.CardAdded(transform, dropZone, dropZoneParent);
            gameObject.GetComponent<IPlaceable>()?.PlaceCard();
        }
        else{
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("dropzone")){
            dropZone = col.transform;
            dropZoneParent = col.transform.parent;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("dropzone")){
            dropZone = null;
            dropZoneParent = null;
        }
    }
}
