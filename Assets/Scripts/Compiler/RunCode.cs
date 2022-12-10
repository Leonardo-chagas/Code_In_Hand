using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Antlr4.Runtime;
using TMPro;

public class RunCode : MonoBehaviour
{
    private string path, dataPath, outputPath, logPath, currentError;
    private bool completedChallenge = false;
    private int challengeIndex;
    private Transform dropArea;
    public GameObject winScreen;
    public GameObject loseScreen;
    public TMP_Text loseText;

    public ErrorListener errorListener = new ErrorListener();
    
    void Start()
    {
        challengeIndex = ChallengeCard.instance.challengeIndex;
        path = "Assets/Resources/programm.txt";
        dataPath = "Assets/Resources/data.txt";
        outputPath = "Assets/Resources/output.txt";
        logPath = "Assets/Resources/log.txt";
        dropArea = GameObject.Find("DropArea").transform;
        GameManager.instance.variables.Clear();
    }


    public void Run(){
        currentError = "";
        //limpar dicionário?
        //GameManager.instance.variables.Clear();
        WriteProgram();
        ResetFiles();
        var fileContents = File.ReadAllText(path);

        //print(fileContents.ReadToEnd());

        //verificação da sintaxe
        var inputStream = new AntlrInputStream(fileContents);
        var cardCodeLexer = new CardCodeLexer(inputStream);
        var commonTokenStream = new CommonTokenStream(cardCodeLexer);
        var cardCodeParser = new CardCodeParser(commonTokenStream);
        
        var cardCodeContext = cardCodeParser.program();
        var visitor = new CardCodeVisitor(errorListener);
        visitor.Visit(cardCodeContext);

        var logContent = File.ReadAllLines(logPath);

        if(logContent.Length > 0){
            //StreamReader reader = new StreamReader(logPath);
            string line = logContent[0];
            string error = logContent[1];
            currentError = error;
            loseScreen.SetActive(true);
            loseText.text = $"Erro na linha {line}: {error}";
            //reader.Close();
        }
        else{

            //verificação da estrutura
            ChallengeCard.instance.CheckCode();
            if(ChallengeCard.instance.hasStructure){
                winScreen.SetActive(true);
                completedChallenge = true;
            }
            else{
                StreamReader reader = new StreamReader(logPath);
                string error = reader.ReadToEnd();
                currentError = error;
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
        print("Logando dados");
        var reader = File.ReadAllLines(dataPath);
        int cont = 0;

        foreach(string line in reader){
            if(line.StartsWith(ChallengeCard.instance.id)){
                string[] currentLine = line.Split("|", System.StringSplitOptions.None);
                if(currentLine[2] == "false"){
                    print("logou data");
                    string completed = completedChallenge ? "true" : "false";
                    currentLine[1] = (Convert.ToInt32(currentLine[1]) + 1).ToString();
                    currentLine[2] = completed;
                    currentLine[3] = currentError != "" ? currentLine[3]+currentError+"," : currentLine[3];

                    string newLine = string.Join("|", currentLine, 0, currentLine.Length);

                    reader[cont] = newLine;
                    File.WriteAllLines(dataPath, reader);

                    break;
                }
            }
            cont++;
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
