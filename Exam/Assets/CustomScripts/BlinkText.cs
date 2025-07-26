using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float blinkSpeed = 2f; // Vitesse du clignotement

    private float timer;

    void Update()
    {
        if (textMesh != null)
        {
            timer += Time.deltaTime * blinkSpeed;
            float alpha = (Mathf.Sin(timer) + 1f) / 2f; // oscillation entre 0 et 1
            Color c = textMesh.color;
            c.a = alpha;
            textMesh.color = c;
        }
    }
}
