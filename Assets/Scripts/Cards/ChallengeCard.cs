using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeCard : MonoBehaviour
{
    private string path, dataPath, outputPath, logPath;
    public static ChallengeCard instance;
    public string[] lines, expectedOutput;
    public string expectation;
    public int challengeIndex;
    [HideInInspector] public bool hasStructure;
    Dictionary<string, int> structure = new Dictionary<string, int>();
    void Start()
    {
        instance = this;
        path = "Assets/Resources/programm.txt";
        dataPath = "Assets/Resources/data.txt";
        outputPath = "Assets/Resources/output.txt";
        logPath = "Assets/Resources/log.txt";
        foreach(string str in lines){
            structure.Add(str, 0);
        }
    }

    public void CheckCode(){
        hasStructure = false;
        var reader = new StreamReader(path);
        string line;
        int cont = 1;

        while((line = reader.ReadLine()) != null){
            /* print(line);
            foreach(string key in structure.Keys){
                //print(key == line);
                print(key + "=" + line);
            } */
            
            if(structure.ContainsKey(line)){
                structure[line] = cont;
                print("achou linha");
            }
            cont++;
        }
        reader.Close();

        int baseValue = 0;
        bool error = false;
        if(!structure.ContainsValue(0)){
            foreach(int val in structure.Values){
                if(val > baseValue){
                    baseValue = val;
                }
                else{
                    error = true;
                    break;
                }
            }
            //estrutura do código está correta
            if(!error){
                print("estrutura está correta");
            }
            //ordem da estrutura do código está incorreta
            else{
                StreamWriter writer = new StreamWriter(logPath);
                writer.Write("A estrutura do código não condiz com as especificações do desafio");
                writer.Close();
                hasStructure = false;
                return;
            }
        }
        //estrutura do código está incorreta
        else{
            StreamWriter writer = new StreamWriter(logPath);
            writer.Write("A estrutura do código não condiz com as especificações do desafio");
            writer.Close();
            hasStructure = false;
            return;
        }

        bool hasOutput = false;
        StreamReader outputReader = new StreamReader(outputPath);
        string outputContent = outputReader.ReadToEnd();

        foreach(string output in expectedOutput){
            if(outputContent == output){
                hasOutput = true;
                break;
            }
        }

        if(hasOutput){
            hasStructure = true;
        }
        else{
            StreamWriter writer = new StreamWriter(logPath);
            writer.Write($"O desafio espera o resultado {expectation}, entretanto a saída recebida foi {outputContent}");
            writer.Close();
        }
    }
}
