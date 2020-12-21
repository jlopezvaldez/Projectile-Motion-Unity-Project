//Include libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Creates a button handle class
public class button_handle : MonoBehaviour
{
    
    public void MainMenu()
    {
        //scene switcher to main menu
        SceneManager.LoadScene("Main Menu");
    }
    
    //Creates a change function
    public void WorkingBoat()
    {
        //Switches the scene back to the main scene
        SceneManager.LoadScene("Working boat");
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("Game is Closing");
    }
}