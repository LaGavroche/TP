using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Score Settings")]
    public int currentScore = 0;

    [Header("UI Reference")]
    public TextMeshProUGUI scoreText; // Assignez votre TextMeshPro ici dans l'Inspector

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI(); // Initialiser l'affichage
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
        Debug.Log($"Score: {currentScore}");
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
            Debug.Log($"UI mise à jour: Score: {currentScore}");
        }
        else
        {
            Debug.LogError("ScoreText n'est pas assigné dans le ScoreManager !");
        }
    }

    public int GetScore()
    {
        return currentScore;
    }
}