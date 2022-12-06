using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropzone : MonoBehaviour
{
    private Vector2 startPosition;
    private RectTransform rectTransform;
    private FitContent fitContent;
    public GameObject dropzone;
    public GameObject line;
    float space = 100f;

    void Start()
    {
        startPosition = transform.GetChild(0).localPosition;
        print(startPosition);
        rectTransform = GetComponent<RectTransform>();
        fitContent = GetComponent<FitContent>();
    }

    public void CardAdded(Transform card, Transform currentDropzone, Transform currentDropzoneParent){
        Transform pos = currentDropzone;
        
        //cria área para baixo
        if(currentDropzone.GetSiblingIndex() == 0){
            print("primeiro sibling");
            if(currentDropzoneParent.GetSiblingIndex() == transform.childCount - 1 ||
            (currentDropzoneParent.GetSiblingIndex() == transform.childCount - 2 && transform.GetChild(transform.childCount-1).name == "block(Clone)")){
                print("nova linha");
                GameObject newLine = Instantiate(line);
                newLine.transform.SetParent(transform, false);
                GameObject drop = Instantiate(dropzone);
                drop.transform.SetParent(newLine.transform, false);
            }
        }

        //cria área para a direita
        if(currentDropzone.GetSiblingIndex() == currentDropzoneParent.childCount - 1){
            GameObject drop = Instantiate(dropzone);
            drop.transform.SetParent(currentDropzoneParent, false);
        }

        card.SetParent(currentDropzoneParent, false);
        card.SetSiblingIndex(currentDropzone.GetSiblingIndex());
        Destroy(currentDropzone.gameObject);

        float cardWidth = card.gameObject.GetComponent<RectTransform>().rect.width;
        float lineSpacing = currentDropzoneParent.gameObject.GetComponent<HorizontalLayoutGroup>().spacing;
        float lineWidth = currentDropzoneParent.gameObject.GetComponent<RectTransform>().rect.width;
        float width = cardWidth+lineSpacing+lineWidth;
        fitContent.Fit(width);
    }

    public void CardRemoved(Transform card){
        GameObject drop = Instantiate(dropzone);
        drop.transform.SetParent(transform, false);
        drop.transform.position = new Vector3(card.position.x, card.position.y, card.position.z);
    }
}
