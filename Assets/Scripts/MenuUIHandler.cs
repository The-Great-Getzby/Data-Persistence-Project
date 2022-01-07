using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    public Text PlayerNameText; // Text object to display the input playername
    public static string playerNameInput; // string that the player inputs
    Scene currentScene; // scene variable used to get the current scene

    // Start is called before the first frame update
    void Start()
    {
        //Dont destroy Canvas, the gameObject this is attached to. Only works on root objects, not children
        DontDestroyOnLoad(gameObject); 
    }

    // Update is called once per frame
    void Update()
    {
        //Get the current scene
        currentScene = SceneManager.GetActiveScene();

        //check if the current scene is Menu. If it's not, destory the button and input field
        if (currentScene != SceneManager.GetSceneByName("Menu"))
        {
            Destroy(GameObject.Find("Start Button"));
            Destroy(GameObject.Find("Name Input"));

            //attempted to move Player Name object once in the new scene, but it had bugs
            //PlayerNameText.transform.position = new Vector2(1, 1);
        }
    }


    // Methods


    public void StartNew() // Assigned to start button, clikc to leave Menu and enter main
    {
        SceneManager.LoadScene(1);
    }

    public void ReadStringInput(string s) // assigned to text field, get the player's input text
    {
        playerNameInput = s;
        Debug.Log(playerNameInput); // show input name in log
        SetPlayerName(playerNameInput);
    }

    public void SetPlayerName (string playerNameInput) // the text of the "PlayerNameText" Text object will include the player's input 
    {
        PlayerNameText.text = "Player Name: " + playerNameInput;
    }
}
