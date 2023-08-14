using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private GameObject[] screenFades;
    private bool fadedIn;
    private bool beginSwitching;
    private string sceneName;

    void Start()
    {
        
    }

    void Update()
    {
        int numberOfFades = 0;
        // Used to determine if the number of screen fades changed
        if (screenFades != null)
        {
            numberOfFades = screenFades.Length;
        }
        screenFades = GameObject.FindGameObjectsWithTag("SceneFade");
        if (numberOfFades != screenFades.Length)
        {
            fadedIn = false;
        }

        if (!fadedIn)
        {
            bool allFaded = false;
            if (screenFades.Length > 0)
            {
                // Fade the scene covers
                for (int i = 0; i < screenFades.Length; i++)
                {
                    Color screenFadeColor = screenFades[i].GetComponent<SpriteRenderer>().color;
                    screenFadeColor = new Color(screenFadeColor.r, screenFadeColor.g, screenFadeColor.b, screenFadeColor.a - 3f * Time.deltaTime);
                    screenFades[i].GetComponent<SpriteRenderer>().color = screenFadeColor;
                    if (screenFadeColor.a <= 0)
                    {
                        allFaded = true;
                    }
                    else
                    {
                        allFaded = false;
                    }
                }
            }
            else
            {
                allFaded = true;
            }

            // Check if all scene covers have faded
            if (allFaded)
            {
                fadedIn = true;
            }
        }
        else if (beginSwitching)
        {
            bool allFaded = false;
            if (screenFades.Length > 0)
            {
                // Fade the scene covers
                for (int i = 0; i < screenFades.Length; i++)
                {
                    Color screenFadeColor = screenFades[i].GetComponent<SpriteRenderer>().color;
                    screenFadeColor = new Color(screenFadeColor.r, screenFadeColor.g, screenFadeColor.b, screenFadeColor.a + 3f * Time.deltaTime);
                    screenFades[i].GetComponent<SpriteRenderer>().color = screenFadeColor;
                    if (screenFadeColor.a >= 1)
                    {
                        allFaded = true;
                    }
                    else
                    {
                        allFaded = false;
                    }
                }
            }
            else
            {
                allFaded = true;
            }

            // Check if all scene covers have faded
            if (allFaded)
            {
                fadedIn = false;
                beginSwitching = false;
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    public void ChangeScene(string scene)
    {
        fadedIn = true;

        if (scene == "Vehicle")
        {
            if (CreateManager.instance != null)
            {
                Destroy(CreateManager.instance.gameObject);
            }
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
                GameManager.instance.ResetGame();
                GameManager.instance.ChangeGameState(GameState.GameplayState);
            }
        }
        else if (scene == "GameOver")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.ChangeGameState(GameState.GameOverState);
            }
        }

        sceneName = scene;
        beginSwitching = true;
    }
}
