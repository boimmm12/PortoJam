using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] public float GlobalSpeed = 5f;
    [SerializeField] GameObject puzzleUI;
    [SerializeField] GameObject gameover;
    [SerializeField] Text coinText;
    [SerializeField] Text coinTotalText;

    [Header("Game Over UI Buttons")]
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button exitButton;
    public bool isPlayerDead = false;
    public bool isPuzzle = false;
    public int coinScore;

    private bool hasGameOverDisplayed = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(OnPlayAgainButton);

        if (exitButton != null)
            exitButton.onClick.AddListener(OnExitButton);
        Time.timeScale = 1f;
        coinScore = 0;
    }
    void Update()
    {
        if (isPlayerDead)
        {
            ShowGameOver();
            // #if UNITY_EDITOR
            //             EditorApplication.isPlaying = false;
            // #endif
        }
        if (isPuzzle && puzzleUI != null && !puzzleUI.activeSelf)
        {
            puzzleUI.SetActive(true);
        }

        coinText.text = "" + coinScore;
    }
    void ShowGameOver()
    {
        hasGameOverDisplayed = true;

        if (gameover != null)
        {
            gameover.SetActive(true);

            if (coinTotalText != null)
            {
                coinTotalText.text = "" + coinScore;
            }
            Time.timeScale = 0f;
        }
    }
    public void OnPlayAgainButton()
    {
        Time.timeScale = 1f; // Unpause
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnExitButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
// #if UNITY_EDITOR
//         EditorApplication.isPlaying = false;
// #else
//         Application.Quit();
// #endif
    }
}
