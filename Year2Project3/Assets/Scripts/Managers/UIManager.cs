﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour 
{

    public static UIManager instance;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public Animator screenFadeAnim;
    public Animator gameOverAnimator;
    public Transform gameOverCamSpawn;

    [Header("Pause")]
    public GameObject pausePanel;
    private bool canPause = true;

    [Header("Wave")]
    public TextMeshProUGUI waveText;
    public Image waveHealthFill;
    public TextMeshProUGUI waveHealthText;

    [Header("Other")]
    public GameObject gameInfoPanel;
    public GameObject waveTimerPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator GameOver()
    {
        CastleUpgradeManager.instance.CloseAllUI();
        canPause = false;

        CameraManager mainCamManager = Camera.main.GetComponent<CameraManager>();
        mainCamManager.enabled = false;

        screenFadeAnim.SetTrigger("Fade");

        yield return new WaitForSeconds(1.2f);

        //float speed = 0;
        //while (Vector3.Distance(mainCamManager.transform.position, gameOverCamSpawn.position) > 0.1f)
        //{
        //    if (speed < 20)
        //    {
        //        speed += 0.5f;
        //    }
        //    mainCamManager.transform.position = Vector3.MoveTowards(mainCamManager.transform.position, gameOverCamSpawn.position, Time.deltaTime * speed);
        //    mainCamManager.transform.rotation = Quaternion.RotateTowards(mainCamManager.transform.rotation, gameOverCamSpawn.rotation, Time.deltaTime * 30);

        //    if (Camera.main.fieldOfView < mainCamManager.maxZoomOut)
        //    {
        //        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, mainCamManager.maxZoomOut, Time.deltaTime * 3);
        //    }
        //    yield return null;
        //}

        gameInfoPanel.SetActive(false);
        waveTimerPanel.SetActive(false);
        gameOverAnimator.enabled = true;

        yield return new WaitForSeconds(gameOverAnimator.GetCurrentAnimatorStateInfo(0).length);

        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    public void PauseButton()
    {
        if (!canPause)
        {
            return;
        }

        if (Time.timeScale == 1)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
