using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropzone : MonoBehaviour
{
    private Vector2 startPosition;
    public static Dropzone instance;
    public GameObject dropzone;
    float space = 50f;
    void Start()
    {
        instance = this;
        startPosition = transform.GetChild(0).position;
    }

    public void CardAdded(Transform currentDropzone){
        Transform pos = currentDropzone;
        Destroy(currentDropzone.gameObject);
        int mask = 1 << LayerMask.NameToLayer("Dropzone");
        //cria área para baixo
        if(currentDropzone.position.x == startPosition.x){
            RaycastHit2D hitDown = Physics2D.Raycast(currentDropzone.position, Vector2.down, 100000, mask);
            if(!hitDown.collider){
                GameObject drop = Instantiate(dropzone);
                drop.transform.SetParent(transform, false);
                drop.transform.position = new Vector3(pos.position.x, pos.position.y - space, pos.position.z);
            }
        }
        //cria área para a direita
        RaycastHit2D hitRight = Physics2D.Raycast(currentDropzone.position, Vector2.right, 100000, mask);
        if(!hitRight.collider){
            GameObject drop = Instantiate(dropzone);
            drop.transform.SetParent(transform, false);
            drop.transform.position = new Vector3(pos.position.x + space, pos.position.y, pos.position.z);
        }
    }

    public void CardRemoved(Transform card){
        GameObject drop = Instantiate(dropzone);
        drop.transform.SetParent(transform, false);
        drop.transform.position = new Vector3(card.position.x, card.position.y, card.position.z);
    }
}
