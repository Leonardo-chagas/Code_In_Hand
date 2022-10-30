using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeCard : MonoBehaviour
{
    private string path;
    public static ChallengeCard instance;
    public string[] lines;
    [HideInInspector] public bool hasStructure;
    Dictionary<string, int> structure = new Dictionary<string, int>();
    void Start()
    {
        instance = this;
        path = "Assets/Resources/programm.txt";
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
            if(!error){
                hasStructure = true;
                print("estrutura está correta");
            }
            else{
                print("estrutura está errada");
            }
        }
        else{
            print("estrutura está errada, possui valor 0");
        }
    }
}
