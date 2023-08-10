using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        if (scene == "Vehicle")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.ChangeGameState(GameState.VehicleState);
            }
        }
        else if (scene == "Options")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.ChangeGameState(GameState.OptionsState);
            }
        }
        else if (scene == "MainMenu")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.ChangeGameState(GameState.TitleState);
            }
        }
        else if (scene == "Game")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.ChangeGameState(GameState.GameplayState);
            }
        }
        SceneManager.LoadScene(scene);
    }
}
