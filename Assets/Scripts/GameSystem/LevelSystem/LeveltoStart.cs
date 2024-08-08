using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class LeveltoStart : MonoBehaviour
{
    public Button returnButton;
    public Animator animator;

    void Start()
    {
        if (returnButton != null)
        {
            returnButton.onClick.AddListener(OnReturnButtonClicked);
        }
    }

    private void OnReturnButtonClicked()
    {
        StartCoroutine(ReturnToStartScene());
    }

    IEnumerator ReturnToStartScene()
    {
        if (animator != null)
        {
            animator.enabled = true;
            animator.Play("Fade_Out");

            yield return new WaitForSeconds(1);
        }

        SceneManager.LoadScene("Start");
    }
}
