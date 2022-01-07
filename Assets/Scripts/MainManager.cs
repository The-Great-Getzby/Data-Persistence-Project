using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public GameObject GameOverText;
    public Text ScoreText;// the object that displays the active player's current score
    public Text HighScoreText; // the object that displays all high score text
    //Get playerNameInput from other script MenuUIHandler

    private bool m_Started = false;
    private int m_Points; //track current points
    private int highScore = 0; //track high score, initialized to zero
    private string highScorePlayerName = "None"; // track high scorer, initialized to none
    
    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        //Check to see if there is saved data (done in method)
        //If there is, Load in High score and High score player name
        LoadHighScore();

        //Game Logic
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        //Game Logic
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            //write high score and hig score playername to save

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        //High Score Logic

        //check to see if current score is higher than High Score
        //if yes, replace high score and high score player name with current score and current player name
        if (highScore < m_Points)
        {
            highScore = m_Points;
            highScorePlayerName = MenuUIHandler.playerNameInput;

        }

        //display the high score and name. Update if it changes.
        HighScoreText.text = $"High Score: {highScorePlayerName} : {highScore}";

    }


    //Quit

    //save data when app is quit
    private void OnApplicationQuit()
    {
        Debug.Log("Application ended");
        SaveHighScore();
    }



    //Methods

    //Scoring
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }


    //Game OVer
    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        SaveHighScore(); //save high score data upon gameover 
    }


    //Saving Data


    [System.Serializable] // must be serializeable for JSON use
    class SaveData // new class for the data we want to save and load
    {
        public int dataHighScore; // internal variable for high score
        public string dataHighScorePlayerName; // internal variable for high score player name
    }


    //Method for saving high score/playername as a JSON file
    public void SaveHighScore()
    {
        SaveData data = new SaveData(); // create a new instance of the SaveData class called "data"
        data.dataHighScore = highScore; // assign the value of highScore within the object "data" to be equal to the current high score saved in MainManager
        data.dataHighScorePlayerName = highScorePlayerName; // . . .


        string json = JsonUtility.ToJson(data); //transform this instance to a JSON string called "json"

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); //write the JSON string to a file
    }


    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.dataHighScore;
            highScorePlayerName = data.dataHighScorePlayerName;
        }
    }

}
