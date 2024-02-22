using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class TestManager : MonoBehaviour
{

    public TMP_InputField playerNameInputField;
    // Custom class to hold both string and int values
    public class StringIntPair
    {
        public string stringValue;
        public int intValue;

        public StringIntPair(string stringValue, int intValue)
        {
            this.stringValue = stringValue;
            this.intValue = intValue;
        }
    }

    private string playerName = ":3";
    
    public static TestManager instance = null;
    
    private float time;
    
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOver;
    private List<StringIntPair> highScores;
    public string highScoresString = "";
    
    
    public int score;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            
            Debug.Log("score changed");
            if (isHighScore(score))
            {
                int highScoreSlot = 0;
                for (int i = 0; i < HighScores.Count; i++)
                {
                    if (score > highScores[i].intValue)
                    {
                        highScoreSlot = i;
                        break;
                    }
                    
                }
                StringIntPair newPair = 
                    new StringIntPair(playerName,score);
                highScores.Insert(highScoreSlot, newPair);
                highScores = highScores.GetRange(0, 5);

                string scoreBoardText = "";
                foreach (var highScore in highScores)
                {
                    scoreBoardText += highScore.stringValue + "," + highScore.intValue + "\n";
                } 
                highScoresString = scoreBoardText; 
                File.WriteAllText(FILE_FULL_PATH, highScoresString);
            }
        }
    }

    const string DATA_DIR = "/Data/";
    const string DATA_HS_FILE = "highscore.txt";
    string FILE_FULL_PATH;
    
    public List<StringIntPair> HighScores
    {
        get
        {
            if (highScores == null)
            {
                highScores = new List<StringIntPair>();
                highScoresString = File.ReadAllText(FILE_FULL_PATH);
                highScoresString = highScoresString.Trim();//returns only unique value
                string[] highScoreArray = highScoresString.Split("\n");
                //add scores to array
                foreach (string scoreEntry in highScoreArray)
                {
                    string[] parts = scoreEntry.Split(",");
                    if (parts.Length == 2)
                    {
                        string stringValue = parts[0];
                        int intValue = 0;
                        if (int.TryParse(parts[1], out intValue))
                        {
                            highScores.Add(new StringIntPair(stringValue, intValue));
                        }
                    }
                }
            }
            return highScores;
        }

        set
        {
            //If no directory named string DATA_DIR's value exists, create one
            if (!Directory.Exists(Application.dataPath + DATA_DIR))
            {
                Directory.CreateDirectory(Application.dataPath + DATA_DIR);
            }
        }
    }
    
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        //name data file path after path+dir+name
        FILE_FULL_PATH = Application.dataPath + DATA_DIR + DATA_HS_FILE;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        scoreText.text = "Score: " + Score;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public bool isHighScore(int score)
    {
        for (int i = 0; i < HighScores.Count; i++)
        {
            if (highScores[i].intValue < score)
            {
                return true;
            }
        }

        return false;
    }
    
}
