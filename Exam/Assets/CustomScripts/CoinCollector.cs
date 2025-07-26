using UnityEngine;
using System.Collections;

public class CoinCollector : MonoBehaviour
{
    [Header("Collection Settings")]
    public string playerTag = "Player";
    public float respawnTime = 3f;
    public int coinValue = 10;

    [Header("Effects (Optional)")]
    public GameObject collectEffect;
    public AudioClip collectSound;

    private Vector3 originalPosition;
    private bool isCollected = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision détectée avec: {other.name}, Tag: {other.tag}"); // Debug

        if (other.CompareTag(playerTag) && !isCollected)
        {
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        isCollected = true;

        // Vérifier si ScoreManager existe
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(coinValue);
            Debug.Log($"Score ajouté ! Nouveau score: {ScoreManager.Instance.GetScore()}");
        }
        else
        {
            Debug.LogError("ScoreManager.Instance est null ! Assure-toi qu'il y a un ScoreManager dans la scène.");
        }

        // Effets
        PlayCollectEffects();

        // Désactiver la pièce
        gameObject.SetActive(false);

        // Programmer le respawn
        StartCoroutine(RespawnCoin());

        Debug.Log($"Pièce collectée ! +{coinValue} points");
    }

    IEnumerator RespawnCoin()
    {
        yield return new WaitForSeconds(respawnTime);

        // Réapparaître
        transform.position = originalPosition;
        gameObject.SetActive(true);
        isCollected = false;

        Debug.Log("Pièce respawnée !");
    }

    void PlayCollectEffects()
    {
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, transform.rotation);
        }

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }
    }
}