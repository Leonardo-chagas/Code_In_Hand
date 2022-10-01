using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropzone : MonoBehaviour
{
    public static Dropzone instance;
    public GameObject dropzone;
    float space = 50f;
    void Start()
    {
        instance = this;
    }

    public void CardAdded(Transform currentDropzone){
        Transform pos = currentDropzone;
        Destroy(currentDropzone.gameObject);
        int mask = 1 << LayerMask.NameToLayer("Dropzone");
        RaycastHit2D hit = Physics2D.Raycast(currentDropzone.position, Vector2.down, 100000, mask);
        if(!hit.collider){
            GameObject drop = Instantiate(dropzone);
            drop.transform.SetParent(transform, false);
            drop.transform.position = new Vector3(pos.position.x, pos.position.y - space, pos.position.z);
        }
    }

    public void CardRemoved(Transform card){
        GameObject drop = Instantiate(dropzone);
        drop.transform.SetParent(transform, false);
        drop.transform.position = new Vector3(card.position.x, card.position.y, card.position.z);
    }
}
