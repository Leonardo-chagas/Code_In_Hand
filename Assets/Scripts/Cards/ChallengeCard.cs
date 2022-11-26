using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeCard : MonoBehaviour
{
    private string path, dataPath, outputPath, logPath;
    private GameObject zoomCard;
    private Transform zoomCardArea, cardPoint;
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

        if(SceneManager.GetActiveScene().name == "Game"){
            zoomCardArea = GameObject.Find("ChallengeZoomCardArea").transform.GetChild(0);
            cardPoint = zoomCardArea.GetChild(1);
            zoomCard = gameObject;
            var components = zoomCard.GetComponents(typeof(Component));
            foreach(Component comp in components){
                if(!comp is RectTransform){
                    Destroy(comp);
                }
            }
        }
    }

    public void CheckCode(){
        hasStructure = false;
        var reader = new StreamReader(path);
        string line;
        int cont = 1;
        

        while((line = reader.ReadLine()) != null){
            
            foreach(string key in structure.Keys){
                if(key.Contains("VAR")){
                    
                }
            }
            
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

        //se estrutura está correta verifica o output
        bool hasOutput = false;
        StreamReader outputReader = new StreamReader(outputPath);
        string outputContent = outputReader.ReadToEnd();
        outputContent = outputContent.TrimEnd();

        foreach(string output in expectedOutput){
            if(outputContent == output){
                hasOutput = true;
                break;
            }
        }

        if(hasOutput){
            hasStructure = true;
        }
        //output está incorreto
        else{
            StreamWriter writer = new StreamWriter(logPath);
            writer.Write($"O desafio espera o resultado {expectation}, entretanto a saída recebida foi {outputContent}");
            writer.Close();
        }
    }

    public void ZoomCard(){
        if(SceneManager.GetActiveScene().name == "Game"){
            zoomCardArea.gameObject.SetActive(true);
            if(zoomCardArea.childCount < 3){
                GameObject obj = Instantiate(zoomCard);
                obj.transform.SetParent(zoomCardArea, false);
                obj.transform.position = cardPoint.position;
                obj.transform.localScale = new Vector3(3, 3, 1);
            }
        }
    }
}
