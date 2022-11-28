using System.IO;
using static System.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWrite : MonoBehaviour, IWritable
{
    public string appendLeft = "";
    public string appendRight = "";
    public bool writeNewLine = false;

    public void WriteCode(StreamWriter writer){
        writer.Write(appendLeft);
        foreach(Transform child in transform){
            child.GetComponent<IWritable>()?.WriteCode(writer);
        }
        writer.Write(appendRight);
        if(writeNewLine){
            writer.Write(NewLine);
        }
    }
}
