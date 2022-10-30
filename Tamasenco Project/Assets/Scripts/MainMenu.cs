using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //functions to find the activeScene by index

    //Chnages Scene by finding its name
    public void changeSceneByName(string level)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        SceneManager.LoadScene(level);
    }
    

    //Quitting the game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
