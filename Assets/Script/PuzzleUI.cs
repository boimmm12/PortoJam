using UnityEngine;
using UnityEngine.UI;

public class PuzzleUI : MonoBehaviour
{
    public float duration = 5f;
    private float timer = 0f;
    public Text timerText;
    public GameObject player;
    public QTESystem qteSystem;

    void OnEnable()
    {
        timer = 0f;
        if (qteSystem != null)
            qteSystem.enabled = true;

    }

    void Update()
    {
        timer += Time.unscaledDeltaTime;
        float remaining = duration - timer;
        if (timerText != null)
            timerText.text = remaining.ToString("F1");

        if (timer >= duration || qteSystem.isPassed)
        {
            ClosePuzzle();
        }
    }

    public void ClosePuzzle()
    {
        if (!gameObject.activeSelf) return;

        gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameController.Instance.isPuzzle = false;

        if (qteSystem != null)
        {
            qteSystem.isPassed = false;
            qteSystem.enabled = false;
        }

        if (player != null)
        {
            player.SetActive(true);
            player.GetComponent<Player>().enabled = true;
        }
        else
        {
            Debug.LogWarning("Player belum di-assign ke PuzzleUI");
        }
    }

}
