using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Ganti "GameScene" dengan nama scene gameplay kamu
        SceneManager.LoadScene("GameScene");
    }

    public void Options()
    {
        // Belum ada opsi, nanti bisa buat panel setting
        Debug.Log("Options clicked");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit the game");
    }
}
