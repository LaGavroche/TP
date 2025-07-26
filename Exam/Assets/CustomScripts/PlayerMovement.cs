using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float gravity = -9.81f;

    private Animator playerController;
    private CharacterController characterController;
    private Vector3 velocity;

    void Start()
    {
        playerController = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        // Vérifier si le Character Controller existe
        if (characterController == null)
        {
            Debug.LogError("Character Controller manquant sur " + gameObject.name);
        }
    }

    void Update()
    {
        // Input WASD + ZQSD (j'ai un clavier suisse déso :))
        float horizontal = 0f;
        float vertical = 0f;

        // Avant (W ou Z)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z)) vertical = 1f;

        // Arrière (S)
        if (Input.GetKey(KeyCode.S)) vertical = -1f;

        // Tourner gauche (A ou Q)
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q)) horizontal = -1f;

        // Tourner droite (D)
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;

        // Rotation gauche/droite
        transform.Rotate(0f, horizontal * rotationSpeed * Time.deltaTime, 0f);

        // Mouvement avec Character Controller
        if (characterController != null)
        {
            // Mouvement horizontal (avant/arrière)
            Vector3 move = transform.forward * vertical * moveSpeed;

            // Appliquer la gravité si pas au sol
            if (characterController.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Petite valeur pour rester collé au sol
            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
            }

            // Combiner mouvement horizontal et vertical (gravité)
            Vector3 finalMovement = move * Time.deltaTime;
            finalMovement.y = velocity.y * Time.deltaTime;

            // Appliquer le mouvement
            characterController.Move(finalMovement);
        }

        // Animation
        if (playerController != null)
        {
            playerController.SetFloat("Speed", Mathf.Abs(vertical));
        }
    }
}