using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class PirateButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Hover Effects")]
    public float fontSizeMultiplier = 1.1f;
    public float glowIntensity = 1.5f;
    public float animationSpeed = 8f;

    [Header("Colors")]
    public Color hoverColor = new Color(1f, 0.8f, 0f, 1f); // Or brillant
    public Color glowColor = new Color(1f, 1f, 0f, 0.8f);  // Jaune éclatant

    private TextMeshProUGUI textMesh;
    private Color originalColor;
    private float originalFontSize;
    private Material originalMaterial;
    private Material glowMaterial;
    private bool isHovering = false;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        originalColor = textMesh.color;
        originalFontSize = textMesh.fontSize; // SAUVEGARDER la taille originale

        // Créer un material pour l'effet glow
        originalMaterial = textMesh.fontMaterial;
        glowMaterial = new Material(originalMaterial);

        // Configuration de l'effet underlay pour le glow
        glowMaterial.SetFloat("_UnderlayOffsetX", 0);
        glowMaterial.SetFloat("_UnderlayOffsetY", 0);
        glowMaterial.SetFloat("_UnderlayDilate", 0.8f);
        glowMaterial.SetFloat("_UnderlaySoftness", 0.3f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHovering)
        {
            isHovering = true;
            StartCoroutine(HoverEffect());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHovering)
        {
            isHovering = false;
            StartCoroutine(ExitEffect());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(ClickEffect());
    }

    IEnumerator HoverEffect()
    {
        float elapsed = 0f;

        // Animation d'entrée - SEULEMENT couleur et glow, pas de scale/position
        while (elapsed < 1f && isHovering)
        {
            elapsed += Time.deltaTime * animationSpeed;
            float progress = Mathf.SmoothStep(0f, 1f, elapsed);

            // Changement de couleur vers or brillant
            textMesh.color = Color.Lerp(originalColor, hoverColor, progress);

            // Effet glow qui apparaît progressivement
            textMesh.fontMaterial = glowMaterial;
            glowMaterial.SetColor("_UnderlayColor", Color.Lerp(Color.clear, glowColor, progress));

            // Augmentation subtile de la taille de font (ne casse pas le layout)
            textMesh.fontSize = Mathf.Lerp(originalFontSize, originalFontSize * fontSizeMultiplier, progress);

            yield return null;
        }

        // Maintenir l'effet tant qu'on survole
        while (isHovering)
        {
            // Glow qui pulse
            Color pulsatingGlow = glowColor;
            pulsatingGlow.a = Mathf.Sin(Time.time * 3f) * 0.3f + 0.7f;
            glowMaterial.SetColor("_UnderlayColor", pulsatingGlow);

            yield return null;
        }
    }

    IEnumerator ExitEffect()
    {
        float elapsed = 0f;
        Color currentColor = textMesh.color;
        float currentFontSize = textMesh.fontSize;

        // Animation de sortie - SEULEMENT couleur et font size
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * animationSpeed;
            float progress = Mathf.SmoothStep(0f, 1f, elapsed);

            // Retour à la couleur normale
            textMesh.color = Color.Lerp(currentColor, originalColor, progress);

            // Retour à la taille de font ORIGINALE
            textMesh.fontSize = Mathf.Lerp(currentFontSize, originalFontSize, progress);

            // Disparition du glow
            Color fadingGlow = glowMaterial.GetColor("_UnderlayColor");
            fadingGlow.a = Mathf.Lerp(fadingGlow.a, 0f, progress);
            glowMaterial.SetColor("_UnderlayColor", fadingGlow);

            yield return null;
        }

        // Restaurer l'état initial
        textMesh.color = originalColor;
        textMesh.fontSize = originalFontSize; // Retour à la taille ORIGINALE
        textMesh.fontMaterial = originalMaterial;
    }

    IEnumerator ClickEffect()
    {
        // Effet de flash au clic - sans scale pour ne pas casser le layout
        float elapsed = 0f;

        while (elapsed < 0.15f)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / 0.15f;

            // Flash blanc rapide
            textMesh.color = Color.Lerp(hoverColor, Color.white, Mathf.Sin(progress * Mathf.PI));

            yield return null;
        }

        // Retour à l'état hover si toujours survolé
        if (isHovering)
        {
            textMesh.color = hoverColor;
        }
    }

    void OnDestroy()
    {
        if (glowMaterial != null)
        {
            DestroyImmediate(glowMaterial);
        }
    }
}