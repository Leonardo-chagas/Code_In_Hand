using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Antlr4.Runtime;

public class RunCode : MonoBehaviour
{
    private string path;
    private string dataPath;
    private bool completedChallenge = false;
    private int challengeIndex;
    private int tries = 0;
    private Transform dropArea;
    public GameObject winScreen;
    public GameObject loseScreen;
    
    void Start()
    {
        challengeIndex = ChallengeCard.instance.challengeIndex;
        path = "Assets/Resources/programm.txt";
        dataPath = "Assets/Resources/data.txt";
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
        var cardCodeParser = new CardCodeParser(commonTokenStream);
        cardCodeParser.AddErrorListener(new ErrorListener());
        if(cardCodeParser.NumberOfSyntaxErrors > 0){

        }
        else{
            var cardCodeContext = cardCodeParser.program();
            var visitor = new CardCodeVisitor();
            visitor.Visit(cardCodeContext);

            //verificação da estrutura
            ChallengeCard.instance.CheckCode();
            if(ChallengeCard.instance.hasStructure){
                winScreen.SetActive(true);
                completedChallenge = true;
            }
            else{
                loseScreen.SetActive(true);
            }
        }
        LogData();
    }

    //writes player data to file
    void LogData(){
        var writer = File.ReadAllLines(dataPath);
        string line = writer[challengeIndex];

        string[] currentLine = line.Split("|");
        if(currentLine[1] == "false"){
            tries++;
            string completed = completedChallenge ? "true" : "false";
            string newLine = tries.ToString() + "|" + completed;
            writer[challengeIndex] = newLine;
        }
    }

    //writes the program based on cards placed
    private void WriteProgramm(){
        var writer = new StreamWriter(path);
        writer.Write("");

        foreach(Transform obj in dropArea){
            obj.gameObject.GetComponent<IWritable>()?.WriteCode(writer);
        }
        writer.Close();
    }
}
