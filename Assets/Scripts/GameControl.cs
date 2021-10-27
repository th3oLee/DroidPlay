using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public bool gameOver;
    public bool victory;
    public GameObject victoryText;
    public GameObject gameOverText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Victory()
    {
        victory = true;
        victoryText.SetActive(true);
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.SetActive(true);
    }
}
