using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    public Text PlayerNameText;
    public static string playerNameInput;
    Scene startScene;
    Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerNameText = Text.FindObjectOfType<Text>();
        //PlayerNameText.text = "Test";
        DontDestroyOnLoad(gameObject);
        //startScene = SceneManager.GetActiveScene();

    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene != SceneManager.GetSceneByName("Menu"))
        {
            Destroy(GameObject.Find("Start Button"));
            Destroy(GameObject.Find("Name Input"));

            //PlayerNameText.transform.position = new Vector2(1, 1);
        }
    }

    public void StartNew() // new custom method
    {
        SceneManager.LoadScene(1);
    }

    public void ReadStringInput(string s)
    {
        playerNameInput = s;
        Debug.Log(playerNameInput);
        SetPlayerName(playerNameInput);
    }

    public void SetPlayerName (string playerNameInput)
    {
        PlayerNameText.text = "Player Name: " + playerNameInput;
    }
}
