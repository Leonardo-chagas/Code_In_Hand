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
    public string id;
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
        IEnumerable<string> variables = GameManager.instance.variables.Keys.ToList();
        
        string[] code = File.ReadAllLines(path);
        List<string> lines = new List<string>(code);
        Dictionary<string, int> structureCopy = new Dictionary<string, int>(structure);

        //verifica se o programa possui todos os elementos necessários
        foreach(string key in structureCopy.Keys){
            
            //bloco que faz match se tiver variável
            if(key.Contains("VAR")){
                int cont = 1;
                foreach(string line in lines){
                    bool matched = false;
                    string[] splitKey = key.Split('|', StringSplitOptions.None);

                    foreach(string option in splitKey){
                        string[] splitString = option.Split(new[] {"VAR"}, StringSplitOptions.None);
                        
                        IEnumerable<IEnumerable<string>> permutations = GetPermutations(variables, splitString.Length-1);

                        foreach(IEnumerable<string> i in permutations){
                            string result = "";
                            int count = 0;

                            foreach(string value in i){
                                if(count==0){
                                    result = result + string.Join(value, splitString, count, 2);
                                    count+=2;
                                }
                                else{
                                    result = result+value;
                                    result = result+splitString[count];
                                    count++;
                                }
                            }
                            line.Replace("\u200B", "");
                            result.Replace("\u200B", "");
                            if(RemoveDiacritics(result).ToLower().Trim() == RemoveDiacritics(line).ToLower().Trim() &&
                            (!string.IsNullOrEmpty(result) && !string.IsNullOrWhiteSpace(result)) &&
                            (!string.IsNullOrEmpty(line) && !string.IsNullOrWhiteSpace(line))){
                                print("Deu match");
                                structure[key] = cont;
                                matched = true;
                                break;
                            }
                        }
                        if(matched)
                            break;
                    }
                    if(matched)
                        break;
                    cont++;
                }
            }
            //bloco que faz match se não tiver variável
            else{
                foreach(string line in lines){
                    int cont = 1;
                    if(RemoveDiacritics(key).ToLower().Trim() == RemoveDiacritics(line).ToLower().Trim()){
                        structure[key] = cont;
                        break;
                    }
                    cont++;
                }
            }
        }
            
            
        

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
                writer.Write("A ordem da estrutura do código está incorreta");
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
    private IEnumerable<int> ConstructSetFromBits(int i)
    {
        for (int n = 0; i != 0; i /= 2, n++)
        {
            if ((i & 1) != 0)
                yield return n;
        }
    }

    private IEnumerable<List<string>> ProduceEnumeration(List<string> allValues)
    {
        for (int i = 0; i < (1 << allValues.Count); i++)
        {
            yield return
                ConstructSetFromBits(i).Select(n => allValues[n]).ToList();
        }
    }

    static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length){
    if (length == 1) return list.Select(t => new T[] { t });

    return GetPermutations(list, length - 1)
        .SelectMany(t => list.Where(e => !t.Contains(e)),
            (t1, t2) => t1.Concat(new T[] { t2 }));
    }
}
