using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Antlr4.Runtime;
using TMPro;

public class RunCode : MonoBehaviour
{
    private string path, dataPath, outputPath, logPath;
    private bool completedChallenge = false;
    private int challengeIndex;
    private int tries = 0;
    private Transform dropArea;
    public GameObject winScreen;
    public GameObject loseScreen;
    public TMP_Text loseText;
    
    void Start()
    {
        challengeIndex = ChallengeCard.instance.challengeIndex;
        path = "Assets/Resources/programm.txt";
        dataPath = "Assets/Resources/data.txt";
        outputPath = "Assets/Resources/output.txt";
        logPath = "Assets/Resources/log.txt";
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
        WriteProgram();
        ResetFiles();
        var fileContents = new StreamReader(path);

        //verificação da sintaxe
        var inputStream = new AntlrInputStream(fileContents.ReadToEnd());
        var cardCodeLexer = new CardCodeLexer(inputStream);
        var commonTokenStream = new CommonTokenStream(cardCodeLexer);
        var cardCodeParser = new CardCodeParser(commonTokenStream);
        cardCodeParser.AddErrorListener(new ErrorListener());

        if(cardCodeParser.NumberOfSyntaxErrors > 0){
            StreamReader reader = new StreamReader(logPath);
            string line = reader.ReadLine();
            string error = reader.ReadLine();
            loseScreen.SetActive(true);
            loseText.text = $"Erro na linha {line}: {error}";
            reader.Close();
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
                StreamReader reader = new StreamReader(logPath);
                string error = reader.ReadToEnd();
                loseScreen.SetActive(true);
                loseText.text = error;
            }
        }
        LogData();
    }

    private void ResetFiles(){
        StreamWriter logWriter = new StreamWriter(logPath);
        logWriter.Write("");
        logWriter.Close();

        StreamWriter outputWriter = new StreamWriter(outputPath);
        outputWriter.Write("");
        outputWriter.Close();
    }

    //writes player data to file
    private void LogData(){
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
    private void WriteProgram(){
        var writer = new StreamWriter(path);
        writer.Write("");

        foreach(Transform obj in dropArea){
            obj.gameObject.GetComponent<IWritable>()?.WriteCode(writer);
        }
        writer.Close();
    }
}
