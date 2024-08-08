using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscCanvas : MonoBehaviour
{
    public GameObject canvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvas != null)
            {
                canvas.SetActive(false);
            }
        }
    }
}
