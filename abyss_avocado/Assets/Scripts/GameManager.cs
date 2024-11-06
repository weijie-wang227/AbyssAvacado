using System;
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
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            Player.Instance.PlayerDied += OnPlayerDeath;
        }
    }

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        EndGame();
    }

    public void StartGame()
    {
        RunStats.Instance.Reset();
        SceneManager.LoadScene("Main");
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