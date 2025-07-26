using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Text scoreText;

    void Start()
    {
        // Trouver le texte automatiquement si pas assigné
        if (scoreText == null)
        {
            scoreText = GetComponent<Text>();
        }

        // Initialiser l'affichage
        UpdateScore(0);
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}