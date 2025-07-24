using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 100f;

    private Animator playerController;

    void Start()
    {
        playerController = GetComponent<Animator>();
    }

    void Update()
    {
        // Input WASD (j'ai un clavier suisse sorry)
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.W)) vertical = 1f;      // Avant
        if (Input.GetKey(KeyCode.S)) vertical = -1f;     // Arrière  
        if (Input.GetKey(KeyCode.A)) horizontal = -1f;   // Tourner gauche
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;    // Tourner droite

        // Rotation gauche/droite
        transform.Rotate(0f, horizontal * rotationSpeed * Time.deltaTime, 0f);

        if (vertical != 0f)
        {
            transform.Translate(Vector3.forward * vertical * moveSpeed * Time.deltaTime);
        }

        // Animation
        if (playerController != null)
        {
            playerController.SetFloat("Speed", Mathf.Abs(vertical));
        }
    }
}