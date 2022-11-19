using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropzoneWrite : MonoBehaviour, IWritable
{
    public void WriteCode(StreamWriter writer){
        int mask = 1 << LayerMask.NameToLayer("Dropzone");
        int mask2 = 1 << LayerMask.NameToLayer("Card");
        int bothMasks = mask | mask2;

        RaycastHit2D[] checkIfLeft = Physics2D.RaycastAll(transform.position, Vector2.left, 10000, mask2);
        foreach(RaycastHit2D hit in checkIfLeft){
            if(hit.collider.gameObject.CompareTag("if card")){
                return;
            }
        }

        RaycastHit2D checkCardRight = Physics2D.Raycast(transform.position, Vector2.right, 100000, mask2);
        if(!checkCardRight.collider){
            writer.Write(";");
        }
    }
}
