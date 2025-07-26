using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float gameTime = 45f;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    private float currentTime;
    private bool gameIsRunning = true;

    void Start()
    {
        currentTime = gameTime;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Se r�f�rencer au ScoreManager
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.SetGameTimer(this);
        }
    }

    void Update()
    {
        if (gameIsRunning)
        {
            currentTime -= Time.deltaTime;

            // Mettre � jour l'UI via ScoreManager
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.UpdateUI();
            }

            if (currentTime <= 0)
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        gameIsRunning = false;
        currentTime = 0;

        Debug.Log("TEMPS �COUL� ! Retour au menu dans 3 secondes...");

        // Arr�ter la collecte des pi�ces
        CoinCollector[] coins = FindFirstObjectByType<CoinCollector>() != null ?
                               FindObjectsByType<CoinCollector>(FindObjectsSortMode.None) :
                               new CoinCollector[0];
        foreach (CoinCollector coin in coins)
        {
            coin.enabled = false;
        }

        StartCoroutine(ShowGameOverAndReturnToMenu());
    }

    IEnumerator ShowGameOverAndReturnToMenu()
    {
        Debug.Log("ShowGameOverAndReturnToMenu appel�");

        if (gameOverPanel != null)
        {
            Debug.Log("Activation du GameOverPanel");
            gameOverPanel.SetActive(true);

            if (finalScoreText != null && ScoreManager.Instance != null)
            {
                int finalScore = ScoreManager.Instance.GetScore();
                string scoreMessage;

                // V�rifier si le score est sup�rieur � 1000
                if (finalScore > 1000)
                {
                    scoreMessage = "GG WP, Your score is: " + finalScore;
                    finalScoreText.color = Color.green; // Vert pour GG WP
                }
                else
                {
                    scoreMessage = "U noob, your score is: " + finalScore;
                    finalScoreText.color = Color.red; // Rouge pour noob
                }

                finalScoreText.text = scoreMessage;
                Debug.Log("Texte mis � jour: " + scoreMessage);
            }
            else
            {
                Debug.LogError("finalScoreText ou ScoreManager.Instance est null");
            }
        }
        else
        {
            Debug.LogError("gameOverPanel est null ! V�rifiez l'assignation dans l'Inspector");
        }

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MenuScene");
    }

    public float GetTimeRemaining()
    {
        return Mathf.Max(0, currentTime);
    }

    public bool IsGameRunning()
    {
        return gameIsRunning;
    }
}