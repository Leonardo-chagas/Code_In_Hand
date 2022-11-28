using System;
using System.Text;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeCard : MonoBehaviour
{
    [Serializable]
    public struct Cards{
        public string cardName;
        public int cardAmount;
    }
    public Cards[] cards;
    public Dictionary<string, int> cardsToTake = new Dictionary<string, int>();
    private string path, dataPath, outputPath, logPath;
    private GameObject zoomCard;
    private Transform zoomCardArea, cardPoint;
    public static ChallengeCard instance;
    public string[] lines, expectedOutput;
    public string expectation;
    public int challengeIndex;
    [HideInInspector] public bool hasStructure;
    Dictionary<string, int> structure = new Dictionary<string, int>();

    void Awake(){
        foreach(Cards card in cards){
            cardsToTake.Add(card.cardName, card.cardAmount);
        }
    }

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
        //var reader = new StreamReader(path);
        //string line;
        int cont = 1;
        List<string> variables = GameManager.instance.variables.Keys.ToList();
        

        foreach(string line in File.ReadAllLines(path)){
            //print(line);
            
            //bloco que faz match se tiver variável
            foreach(string key in structure.Keys){
                bool matched = false;
                if(key.Contains("VAR")){
                    string[] splitString = key.Split(new[] {"VAR"}, StringSplitOptions.None);

                    foreach(IEnumerable<string> i in Combinations(variables, splitString.Length-1)){
                        string result = "";
                        int count = 0;
                        print(i);

                        foreach(string value in i){
                            result = result + string.Join(value, splitString, count, count+1);
                            count++;
                        }
                        /* print(RemoveDiacritics(result).ToLower().Trim());
                        print(RemoveDiacritics(line).ToLower().Trim()); */
                        if(RemoveDiacritics(result).ToLower().Trim() == RemoveDiacritics(line).ToLower().Trim()){
                            print("Deu match");
                            structure[key] = cont;
                            matched = true;
                            break;
                        }
                    }
                }
                if(matched){
                    break;
                }
            }
            
            //bloco que faz match se não tiver variável
            if(structure.ContainsKey(line)){
                structure[line] = cont;
                print("achou linha");
            }
            cont++;
        }
        //reader.Close();

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
    
    //remove os acentos de uma string
    private string RemoveDiacritics(string text){
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

        for(int i = 0; i < normalizedString.Length; i++){
            char c = normalizedString[i];
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if(unicodeCategory != UnicodeCategory.NonSpacingMark){
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
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

    //devolve todas as k combinações de elementos de uma lista
    private bool NextCombination(IList<int> num, int n, int k){
        bool finished;
        var changed = finished = false;

        if(k <= 0) return false;

        for(var i = k - 1; !finished && !changed; i--){
            if(num[i] < n - 1 - (k - 1) + i){
                num[i]++;

                if(i < k - 1)
                    for(var j = i + 1; j< k; j++)
                        num[j] = num[j - 1] + 1;
                    changed = true;
                
                finished = i == 0;
            }
        }
        return changed;
    }

    private IEnumerable Combinations<T>(IEnumerable<T> elements, int k){
        var elem = elements.ToArray();
        var size = elem.Length;

        if(k > size) yield break;
        if(k == 1 && size == 1){
            print("devolvendo lista original");
            yield return elements;
            yield break;
        }

        var numbers = new int[k];

        for(var i = 0; i < k; i++)
            numbers[i] = i;

        do{
            yield return numbers.Select(n => elem[n]);
        } while(NextCombination(numbers, size, k));
    }
}
