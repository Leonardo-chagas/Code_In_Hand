using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropzone : MonoBehaviour
{
    private Vector2 startPosition;
    private RectTransform rectTransform;
    public static Dropzone instance;
    public GameObject dropzone;
    public GameObject line;
    public GameObject horizontalScroll;
    public GameObject verticalScroll;
    float space = 100f;
    void Start()
    {
        instance = this;
        startPosition = transform.GetChild(0).localPosition;
        print(startPosition);
        rectTransform = GetComponent<RectTransform>();
        /* horizontalScroll.SetActive(false);
        verticalScroll.SetActive(false); */
    }

    public void CardAdded(Transform card, Transform currentDropzone, Transform currentDropzoneParent){
        Transform pos = currentDropzone;
        
        int mask = 1 << LayerMask.NameToLayer("Dropzone");
        int mask2 = 1 << LayerMask.NameToLayer("Card");
        int bothMasks = mask | mask2;
        
        //cria área para baixo
        if(currentDropzone.GetSiblingIndex() == 0){
            RaycastHit2D hitDown = Physics2D.Raycast(currentDropzone.position, Vector2.down, 100000, mask);
            if(currentDropzoneParent.GetSiblingIndex() == transform.childCount - 1){
                //CheckVerticalCard(currentDropzone);
                GameObject newLine = Instantiate(line);
                newLine.transform.SetParent(transform);
                GameObject drop = Instantiate(dropzone);
                drop.transform.SetParent(newLine.transform);
                //drop.transform.position = new Vector3(currentDropzone.position.x, pos.position.y - space, pos.position.z);
            }
        }

        //cria área para a direita
        RaycastHit2D hitRight = Physics2D.Raycast(currentDropzone.position, Vector2.right, 100000, mask);
        if(currentDropzone.GetSiblingIndex() == currentDropzoneParent.childCount - 1){
            //CheckHorizontalCard(currentDropzone);
            GameObject drop = Instantiate(dropzone);
            drop.transform.SetParent(currentDropzoneParent);
            //drop.transform.position = new Vector3(currentDropzone.position.x + space, pos.position.y, pos.position.z);
        }
        card.SetParent(currentDropzoneParent);
        card.SetSiblingIndex(currentDropzone.GetSiblingIndex());
        Destroy(currentDropzone.gameObject);
    }

    private void CheckVerticalCard(Transform obj){
        if(obj.localPosition.y - space < rectTransform.rect.height/2 - 30){
            rectTransform.offsetMin = Vector2.down * (space + 30);
            verticalScroll.SetActive(true);
        }
    }

    private void CheckHorizontalCard(Transform obj){
        if(obj.localPosition.x + space > rectTransform.rect.width/2 - 30){
            rectTransform.offsetMax = Vector2.right * (space + 30);
            horizontalScroll.SetActive(true);
        }
    }

    public void CardRemoved(Transform card){
        GameObject drop = Instantiate(dropzone);
        drop.transform.SetParent(transform, false);
        drop.transform.position = new Vector3(card.position.x, card.position.y, card.position.z);
    }
}
