using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; //Need this for calling UI scripts
using UnityEngine.SceneManagement;

public class pauseManager : MonoBehaviour {

[SerializeField]
Transform UIPanel; //Will assign our panel to this variable so we can enable/disable it

[SerializeField]
Text timeText; //Will assign our Time Text to this variable so we can modify the text it displays.

public KeyCode mouseCursorShowHotKey = KeyCode.Tab;

bool isPaused; //Used to determine paused state
private bool showingMouseCursor = false;
public static bool pause;

void Start ()
{
UIPanel.gameObject.SetActive(false); //make sure our pause menu is disabled when scene starts
isPaused = false; //make sure isPaused is always false when our scene opens
SetMouseShowing(false);
}

void Update ()
{
HotKeys();
//timeText.text = "Time Since Startup: " + Time.timeSinceLevelLoad; //Tells us the time since the scene loaded

//If player presses escape and game is not paused. Pause game. If game is paused and player presses escape, unpause.
if(Input.GetKeyDown(KeyCode.Tab) && !isPaused)
{
/*if (Input.GetKeyDown(mouseCursorShowHotKey))
{
    SetMouseShowing(true);
}*/
Pause();
}
else if(Input.GetKeyDown(KeyCode.Tab) && isPaused)
UnPause();
}

public void Pause()
{
//pause = true;
//AudioListener.pause;
isPaused = true;
UIPanel.gameObject.SetActive(true); //turn on the pause menu
Time.timeScale = 0; //pause the game
}

public void UnPause()
{
isPaused = false;
UIPanel.gameObject.SetActive(false); //turn off pause menu
Time.timeScale = 1; //resume game
}

public void QuitGame()
{
//Application.Quit();
SceneManager.LoadScene("Main Menu");
}

public void Restart()
{
Application.LoadLevel(0);
}


void SetMouseShowing(bool value)
{
    //bool value;
    //Enable or disable the cursor visibility:
    Cursor.visible = value;
    showingMouseCursor = value;
    //Set the cursor lock state based on 'value':
    if (value)
        Cursor.lockState = CursorLockMode.None;
    else
        Cursor.lockState = CursorLockMode.Locked;
}

void HotKeys()
{
if (Input.GetKeyDown(mouseCursorShowHotKey))
    SetMouseShowing(true);
}
}