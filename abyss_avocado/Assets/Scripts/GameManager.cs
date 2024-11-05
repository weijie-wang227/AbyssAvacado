using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton that manages scene loading; persists between scenes
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void StartGame()
    {
        RunStats.Instance.Reset();
        SceneManager.LoadScene("abyss");
    }

    public void EndGame()
    {
        RunStats.Instance.RecordDepth();
        SceneManager.LoadScene("Death");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Start");
    }
}