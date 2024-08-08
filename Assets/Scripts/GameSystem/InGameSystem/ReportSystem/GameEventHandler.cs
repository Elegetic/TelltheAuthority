using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IllegalBehavior"))
        {
            ScoreManager.Instance.AddScore(2);
        }
        else if (other.CompareTag("LegalBehavior"))
        {
            ScoreManager.Instance.SubtractScore(1);
        }
    }
}