using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject optionCanvas;
    public GameObject instructionCanvas;
    public Button optionButton;
    public Button instructionButton;
    public Button optionBackButton;
    public Button instructionBackButton;

    private bool isPaused = false;
    private GameObject activeCanvas = null;
    private AudioSource[] bgmAudioSources;

    private void Start()
    {
        optionButton.onClick.AddListener(OpenOptionCanvas);
        instructionButton.onClick.AddListener(OpenInstructionCanvas);
        optionBackButton.onClick.AddListener(CloseOptionCanvas);
        instructionBackButton.onClick.AddListener(CloseInstructionCanvas);

        GameObject[] bgmObjects = GameObject.FindGameObjectsWithTag("BGM");
        bgmAudioSources = new AudioSource[bgmObjects.Length];
        for (int i = 0; i < bgmObjects.Length; i++)
        {
            bgmAudioSources[i] = bgmObjects[i].GetComponent<AudioSource>();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                if (activeCanvas == null)
                {
                    OpenOptionCanvas();
                }
                else
                {
                    ResumeGame();
                }
            }
        }
    }

    void OpenOptionCanvas()
    {
        PauseGame();
        if (optionCanvas != null)
        {
            optionCanvas.SetActive(true);
            activeCanvas = optionCanvas;
        }
    }

    void OpenInstructionCanvas()
    {
        PauseGame();
        if (instructionCanvas != null)
        {
            instructionCanvas.SetActive(true);
            activeCanvas = instructionCanvas;
        }
    }

    void CloseOptionCanvas()
    {
        if (optionCanvas != null)
        {
            optionCanvas.SetActive(false);
            activeCanvas = null;
        }
        ResumeGame();
    }

    void CloseInstructionCanvas()
    {
        if (instructionCanvas != null)
        {
            instructionCanvas.SetActive(false);
            activeCanvas = null;
        }
        ResumeGame();
    }


    void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;

        foreach (AudioSource bgm in bgmAudioSources)
        {
            if (bgm != null)
            {
                bgm.Pause();
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        foreach (AudioSource bgm in bgmAudioSources)
        {
            if (bgm != null)
            {
                bgm.UnPause();
            }
        }

        if (activeCanvas != null)
        {
            activeCanvas.SetActive(false);
            activeCanvas = null;
        }
    }
}
