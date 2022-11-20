using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropzoneLife : MonoBehaviour
{
    private RectTransform rectTransform;
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine("Check");
    }

    private IEnumerator Check(){
        yield return new WaitForSeconds(0.3f);
        Vector2 pos = transform.position;
        
        int mask = 1 << LayerMask.NameToLayer("Dropzone");
        int mask2 = 1 << LayerMask.NameToLayer("Card");
        int bothMasks = mask | mask2;

        RaycastHit2D checkCardsRight = Physics2D.Raycast(pos, Vector2.right, 10000, mask2);
        RaycastHit2D checkDropLeft = Physics2D.Raycast(new Vector2(pos.x - rectTransform.rect.width - 30, pos.y), Vector2.left, 100, mask);

        if(checkDropLeft.collider && !checkCardsRight.collider){
            Destroy(gameObject);
        }

        StartCoroutine("Check");
    }
}
