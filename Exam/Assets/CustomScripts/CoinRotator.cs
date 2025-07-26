using UnityEngine;

public class CoinRotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 80f;
    [Header("Test Different Axes")]
    public bool rotateX = false; // Rotation autour de X
    public bool rotateY = true;  // Rotation autour de Y (toupie)
    public bool rotateZ = false; // Rotation autour de Z

    [Header("Optional Bob Effect")]
    public bool enableBobbing = true;
    public float bobSpeed = 2f;
    public float bobHeight = 0.3f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Test des différents axes
        Vector3 rotationAxis = Vector3.zero;

        if (rotateX) rotationAxis += Vector3.right;
        if (rotateY) rotationAxis += Vector3.up;
        if (rotateZ) rotationAxis += Vector3.forward;

        // Rotation
        if (rotationAxis != Vector3.zero)
        {
            transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
        }

        // Effet de bobbing
        if (enableBobbing)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }
    }
}