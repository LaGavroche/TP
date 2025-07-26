using UnityEngine;

public class WaterScroller : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private Renderer waterRenderer;
    private Material waterMaterial;

    void Start()
    {
        waterRenderer = GetComponent<Renderer>();

        if (waterRenderer != null)
        {
            waterMaterial = waterRenderer.sharedMaterial;
        }
        else
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (waterRenderer == null || waterMaterial == null) return;

        if (Camera.main != null && waterMaterial.shader != null && waterMaterial.shader.isSupported)
        {
            Camera.main.depthTextureMode |= DepthTextureMode.Depth;
        }

        float offset = Time.time * scrollSpeed;
        Vector2 textureOffset = new Vector2(offset / 10.0f, offset);

        if (waterMaterial.HasProperty("_BaseMap"))
        {
            waterMaterial.SetTextureOffset("_BaseMap", textureOffset);
        }
        else if (waterMaterial.HasProperty("_MainTex"))
        {
            waterMaterial.SetTextureOffset("_MainTex", textureOffset);
        }
        else if (waterMaterial.HasProperty("_AlbedoMap"))
        {
            waterMaterial.SetTextureOffset("_AlbedoMap", textureOffset);
        }
    }
}