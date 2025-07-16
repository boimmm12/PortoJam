using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] public float GlobalSpeed = 5f;
    [SerializeField] GameObject puzzleUI;
    public bool isPlayerDead = false;
    public bool isPuzzle = false;

    void Awake()
    {
        // Singleton pattern: pastikan hanya satu GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Bertahan saat ganti scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (isPlayerDead)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }

        if (isPuzzle && puzzleUI != null && !puzzleUI.activeSelf)
        {
            puzzleUI.SetActive(true);
        }
    }

}
