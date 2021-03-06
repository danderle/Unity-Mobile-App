﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Private Properties

    private static LevelManager mInstance;
    private float mStartTime;
    private GameObject mPlayer;

    #endregion

    #region Public Proprties

    public static LevelManager Instance { get { return mInstance; } }

    public GameObject PauseMenu;
    public Transform RespawnPoint;


    public float SilverTime;
    public float GoldTime;
    #endregion
    
    /// <summary>
    /// Default start
    /// </summary>
    private void Start()
    {
        mInstance = this;
        PauseMenu.SetActive(false);
        mStartTime = Time.time;
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mPlayer.transform.position = RespawnPoint.position;
    }

    private void Update()
    {
        if (mPlayer.transform.position.y < -25.0f)
            Death();
    }

    /// <summary>
    /// Pause / Unpauses the game
    /// </summary>
    public void TogglePauseMenu()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
    }

    /// <summary>
    /// To the Main menu scene.
    /// </summary>
    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Calls the Winbox victory function
    /// </summary>
    public void Victory()
    {
        float duration = Time.time - mStartTime;
       
        if (duration < GoldTime)
        {
            GameManager.Instance.Currency += 50;
        }
        else if (duration < SilverTime)
        {
            GameManager.Instance.Currency += 25;
        }
        else
            GameManager.Instance.Currency += 10;
        GameManager.Instance.Save();

        string saveString = string.Empty;

        LevelData levelData = new LevelData(SceneManager.GetActiveScene().name);

        saveString += (levelData.BestTime > duration || levelData.BestTime == 0.0f) ? duration.ToString() : levelData.BestTime.ToString();
        saveString += '&';
        saveString += SilverTime.ToString();
        saveString += '&';
        saveString += GoldTime.ToString();

        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, saveString);

        SceneManager.LoadScene("MainMenu");
    }

    public void Death()
    {
        mPlayer.transform.position = RespawnPoint.position;
        Rigidbody rigid = mPlayer.GetComponent<Rigidbody>();

        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
}
