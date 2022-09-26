using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool isDragging = false;
    private GameObject startParent;
    private Vector3 startPosition;
    private bool isOverDropZone;
    
    void Start()
    {
        
    }

    
    void Update()
    {
       if(isDragging){
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
       } 
    }

    public void StartDrag(){
        isDragging = true;
        startParent = transform.parent.gameObject;
    }

    public void StopDrag(){
        isDragging = false;

    }
}
