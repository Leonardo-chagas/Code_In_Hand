using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Transform startParent;
    private Vector3 startPosition;
    private Transform dropArea;
    private Transform dropZone;
    
    void Start()
    {
        //startPosition = transform.position;
        dropArea = GameObject.Find("DropArea").transform;
        StartCoroutine("DefineStart");
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
            print("passou");
        }
        //startPosition = transform.position;
        //startParent = transform.parent.gameObject;
    }

    public void StopDrag(){
        isDragging = false;
        if(dropZone != null){
            transform.SetParent(dropArea, false);
            transform.position = dropZone.position;
            Dropzone.instance.CardAdded(dropZone);
        }
        else{
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("dropzone")){
            dropZone = col.transform;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("dropzone")){
            dropZone = null;
        }
    }
}
