using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomCard : MonoBehaviour
{
    private Transform mainCanvas;
    private Transform startParent;
    private GameObject zoomCard;
    private GameObject zoomCardClone;
    private DragDrop dragDrop;
    float targetHeight = 1080;

    void Awake()
    {
        dragDrop = GetComponent<DragDrop>();
        mainCanvas = GameObject.Find("Main Canvas").transform;
        StartCoroutine("DefineStart");
        zoomCard = gameObject;
        var components = zoomCard.GetComponents(typeof(Component));
        foreach(Component comp  in components){
            if(!comp is RectTransform){
                Destroy(comp);
            }
        }
    }

    IEnumerator DefineStart(){
        yield return new WaitForSeconds(0.05f);
        startParent = transform.parent;
    }

    public void OnHoverEnter(){
        if(transform.parent == startParent && !dragDrop.isDragging){
            print("adicionou carta");
            zoomCardClone = Instantiate(zoomCard);
            zoomCardClone.transform.SetParent(mainCanvas, false);
            float height = Screen.height/targetHeight;
            zoomCardClone.transform.position = new Vector3(transform.position.x, transform.position.y + 450*height, transform.position.z);
            zoomCardClone.transform.localScale = new Vector3(zoomCard.transform.localScale.x*2, zoomCard.transform.localScale.y*2, zoomCard.transform.localScale.z);
        }
        if(dragDrop.isDragging){
            OnHoverExit();
        }
    }

    public void OnHoverExit(){
        if(transform.parent == startParent){
            print("removeu carta");
            Destroy(zoomCardClone);
        }
    }
}
