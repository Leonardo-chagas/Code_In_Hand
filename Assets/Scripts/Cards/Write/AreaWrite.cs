using System;
using System.IO;
using static System.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWrite : MonoBehaviour, IWritable
{
    public string appendLeft = "";
    public string appendRight = "";
    public bool writeNewLineLeft = false;
    public bool writeNewLineRight = false;
    private bool isDropzone;

    void Start(){
        isDropzone = GetComponent<Dropzone>() != null ? true : false;
    }

    public void WriteCode(StreamWriter writer){
        if(transform.childCount > 1 && !isDropzone)
            appendRight = ";";

        writer.Write(appendLeft);
        if(writeNewLineLeft)
            writer.Write(NewLine);

        foreach(Transform child in transform){
            child.GetComponent<IWritable>()?.WriteCode(writer);
        }

        writer.Write(appendRight);

        if(writeNewLineRight){
            writer.Write(NewLine);
        }
    }
}
