using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Antlr4.Runtime;
using System.IO;

public class ErrorListener
{
    public void HandleError(int line, string error){
        using(var writer = File.AppendText("Assets/Resources/log.txt")){
            writer.WriteLine(line);
            writer.WriteLine(error);
        }
    }
}
