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

    //MANETTE QUI JOUE
    public RemoteServerSide playingRemote;

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

    private void Update()
    {
        if (gameOver || victory)
        {
            if (!playingRemote.isConsumed && playingRemote.lastInput == "A")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
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

    public void RegisterRemote(RemoteServerSide remote)
    {
        playingRemote = remote;
    }
}
