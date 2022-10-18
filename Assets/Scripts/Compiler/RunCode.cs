using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Antlr4.Runtime;

public class RunCode : MonoBehaviour
{
    private string path;
    
    void Start()
    {
        path = "Assets/Resources/programm.txt";
    }

    public void TestFile(){
        WriteProgramm();
        var fileContents = new StreamReader(path);
        Debug.Log(fileContents.ReadToEnd());
        fileContents.Close();
    }

    public void Run(){
        var fileContents = new StreamReader(path);

        var inputStream = new AntlrInputStream(fileContents.ReadToEnd());
        var cardCodeLexer = new CardCodeLexer(inputStream);
        var commonTokenStream = new CommonTokenStream(cardCodeLexer);
        var CardCodeParser = new CardCodeParser(commonTokenStream);
        var cardCodeContext = CardCodeParser.program();
        var visitor = new CardCodeVisitor();
        visitor.Visit(cardCodeContext);
    }

    private void WriteProgramm(){
        var fileContents = new StreamWriter(path);
        fileContents.Write("mudei o texto do arquivo\n é isso aí");
        fileContents.Close();
    }
}
