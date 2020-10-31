using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VBP.Ball;
using VBP.Player;
using VBP.Puppy;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject quitButton;

    private void Start()
    {

    }

    private void Update()
    {
        StartingGame();
    }

    #region Pause

    public void Pause()
    {
        Time.timeScale = 0;
        resumeButton.SetActive(true);
        quitButton.SetActive(true);
    }

    #endregion

    #region ReturnToMenu

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

    #region Resume

    public void Resume()
    {
        Time.timeScale = 1;
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
    }

    #endregion

    #region StartingGame

    [SerializeField] private GameObject puppyManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject sensei;
    [SerializeField] private GameObject neko;
    [SerializeField] private GameObject jumpButton;
    [SerializeField] private GameObject kickButton;
    [SerializeField] private Text startCount;
    [SerializeField] private float count;
    
    private void StartingGame()
    {
        startCount.text = count.ToString("f0");
        if (count <= 0)
        {
            count = 0;
            startCount.gameObject.SetActive(false);
            puppyManager.SetActive(true);
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<Animator>().enabled = true;
            ball.GetComponent<SetBall>().enabled = true;
            sensei.GetComponent<Animator>().enabled = true;
            neko.GetComponent<Animator>().enabled = true;
            jumpButton.GetComponent<Button>().enabled = true;
            kickButton.GetComponent<Button>().enabled = true;
        }
        else
        {
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<Animator>().enabled = false;
            ball.GetComponent<SetBall>().enabled = false;
            sensei.GetComponent<Animator>().enabled = false;
            neko.GetComponent<Animator>().enabled = false;
            jumpButton.GetComponent<Button>().enabled = false;
            kickButton.GetComponent<Button>().enabled = false;
            count -= Time.deltaTime;
        }
    }

    #endregion
}
