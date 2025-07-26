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
    private Renderer coinRenderer;
    private Collider coinCollider;

    void Start()
    {
        originalPosition = transform.position;
        coinRenderer = GetComponent<Renderer>();
        coinCollider = GetComponent<Collider>();

        // Vérification des composants
        if (coinRenderer == null)
            Debug.LogError("Aucun Renderer trouvé sur " + gameObject.name);
        if (coinCollider == null)
            Debug.LogError("Aucun Collider trouvé sur " + gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision détectée avec: {other.name}, Tag: {other.tag}");

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
            Debug.LogError("ScoreManager.Instance est null !");
        }

        // Effets
        PlayCollectEffects();

        // Cacher la pièce au lieu de la désactiver
        if (coinRenderer != null)
            coinRenderer.enabled = false;
        if (coinCollider != null)
            coinCollider.enabled = false;

        // Programmer le respawn (maintenant la coroutine peut s'exécuter)
        StartCoroutine(RespawnCoin());

        Debug.Log($"Pièce collectée ! +{coinValue} points");
    }

    IEnumerator RespawnCoin()
    {
        yield return new WaitForSeconds(respawnTime);

        // Réapparaître
        transform.position = originalPosition;

        if (coinRenderer != null)
            coinRenderer.enabled = true;
        if (coinCollider != null)
            coinCollider.enabled = true;

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