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

        
        
    }
}
