using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Antlr4.Runtime;

public class RunCode : MonoBehaviour
{
    private string path;
    private Transform dropArea;
    public GameObject winScreen;
    public GameObject loseScreen;
    
    void Start()
    {
        path = "Assets/Resources/programm.txt";
        dropArea = GameObject.Find("DropArea").transform;
    }

    public void TestFile(){
        var fileText = new StreamWriter(path);
        fileText.Write("texto extra");
        fileText.Write(" mais texto");
        fileText.Close();
        var fileContents = new StreamReader(path);
        Debug.Log(fileContents.ReadToEnd());
        fileContents.Close();
    }

    public void Run(){
        WriteProgramm();
        var fileContents = new StreamReader(path);

        //verificação da sintaxe
        var inputStream = new AntlrInputStream(fileContents.ReadToEnd());
        var cardCodeLexer = new CardCodeLexer(inputStream);
        var commonTokenStream = new CommonTokenStream(cardCodeLexer);
        var CardCodeParser = new CardCodeParser(commonTokenStream);
        var cardCodeContext = CardCodeParser.program();
        var visitor = new CardCodeVisitor();
        visitor.Visit(cardCodeContext);

        //verificação da estrutura
        ChallengeCard.instance.CheckCode();
        if(ChallengeCard.instance.hasStructure){
            winScreen.SetActive(true);
        }
        else{
            loseScreen.SetActive(true);
        }
    }

    private void WriteProgramm(){
        var writer = new StreamWriter(path);
        writer.Write("");

        foreach(Transform obj in dropArea){
            obj.gameObject.GetComponent<IWritable>()?.WriteCode(writer);
        }
        writer.Close();
    }
}
