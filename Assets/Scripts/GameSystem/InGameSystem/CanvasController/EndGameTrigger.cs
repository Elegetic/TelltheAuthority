using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public GameObject endGamePanel;
    private AudioSource[] bgmSources;

    private void Start()
    {
        GameObject[] bgmObjects = GameObject.FindGameObjectsWithTag("BGM");
        bgmSources = new AudioSource[bgmObjects.Length];
        for (int i = 0; i < bgmObjects.Length; i++)
        {
            bgmSources[i] = bgmObjects[i].GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PauseBGM();
            EndGame();
            Debug.Log("End Collision Detected! ");
        }
    }

    private void PauseBGM()
    {
        foreach (AudioSource bgm in bgmSources)
        {
            if (bgm.isPlaying)
            {
                bgm.Pause();
            }
        }
    }

    private void EndGame()
    {
        Time.timeScale = 0f;

        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true);
        }
    }
}