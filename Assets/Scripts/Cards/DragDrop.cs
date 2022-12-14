using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DragDrop : MonoBehaviour
{
    [HideInInspector] public bool isDragging = false;
    private Transform startParent;
    private Vector3 startPosition;
    private Transform dropZone;
    [HideInInspector] public Transform dropZoneParent;
    private Dropzone droparea;
    private TMP_Text text;
    private RectTransform rectTransform;
    private GameObject mainCanvas;
    private string startText;
    private float height;
    [HideInInspector]public float width;

    void Awake(){
        rectTransform = GetComponent<RectTransform>();
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;
        //mainCanvas = GameObject.Find("Main Canvas");
    }
    
    void Start()
    {
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
            if(droparea)
                droparea.CardRemoved(transform);
            //Dropzone.instance.CardRemoved(transform);
            //remove a variável do dicionário se a carta de variável for removida
            if(startText == "VAR"){
                GameManager.instance.variables[text.text] = GameManager.instance.variables[text.text] - 1;
                if(GameManager.instance.variables[text.text] <= 0){
                    GameManager.instance.variables.Remove(text.text);
                }
            }
            text.text = startText;

            if(text.text == "IF " || text.text == "ELSE"){
                transform.parent.transform.parent.GetChild(transform.parent.GetSiblingIndex()+1).GetComponent<CardRemover>()?.RemoveCards();
                transform.parent.GetComponent<CardRemover>()?.RemoveCards();
            }
        }
    }

    public void StopDrag(){
        isDragging = false;
        if(dropZone && droparea){
            gameObject.GetComponent<IPlaceable>()?.PlaceCard();
            /* transform.SetParent(mainCanvas.transform, false);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height); */
            droparea.CardAdded(transform, dropZone, dropZoneParent);
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
            droparea = dropZoneParent.parent.gameObject.GetComponent<Dropzone>();
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("dropzone") && dropZone == col.transform){
            dropZone = null;
            dropZoneParent = null;
            droparea = null;
        }
    }
}
