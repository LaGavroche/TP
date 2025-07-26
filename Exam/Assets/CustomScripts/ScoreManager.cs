using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Score Settings")]
    public int currentScore = 0;

    [Header("UI Reference")]
    public TextMeshProUGUI scoreText;

    private GameTimer gameTimer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("ScoreManager Instance cr��e");
        }
        else
        {
            Debug.Log("ScoreManager Instance d�j� existante, destruction de ce GameObject");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("ScoreManager Start() - Initialisation");

        if (scoreText == null)
        {
            Debug.LogWarning("ScoreText pas assign�, tentative de recherche automatique...");
            scoreText = FindFirstObjectByType<TextMeshProUGUI>();

            if (scoreText != null)
            {
                Debug.Log("TextMeshPro trouv� automatiquement: " + scoreText.name);
            }
        }

        UpdateScoreUI();
    }

    // Fonction appel�e par GameTimer pour se r�f�rencer
    public void SetGameTimer(GameTimer timer)
    {
        gameTimer = timer;
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        Debug.Log($"Score ajout�: +{points}, Total: {currentScore}");
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
        Debug.Log("Score remis � z�ro");
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            string timerText = "Time : 45";
            if (gameTimer != null)
            {
                float timeRemaining = gameTimer.GetTimeRemaining();
                int totalSeconds = Mathf.FloorToInt(timeRemaining);
                timerText = "Time : " + totalSeconds.ToString("00");
            }

            string newText = "Score: " + currentScore + "\n" + timerText;
            scoreText.text = newText;

            // Changer couleur quand il reste moins de 10 secondes
            if (gameTimer != null && gameTimer.GetTimeRemaining() <= 10f && gameTimer.IsGameRunning())
            {
                scoreText.color = Color.red;
            }
            else
            {
                scoreText.color = Color.white;
            }
        }
        else
        {
            Debug.LogError("ScoreText n'est pas assign� dans le ScoreManager !");
        }
    }

    public void UpdateUI()
    {
        UpdateScoreUI();
    }

    public int GetScore()
    {
        return currentScore;
    }
}