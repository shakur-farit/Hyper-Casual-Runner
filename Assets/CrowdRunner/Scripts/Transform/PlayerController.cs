using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float slideSpeed = 10f;

    private bool canMove;

    [SerializeField] private float chunkWidth = 10f;

    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;

    [SerializeField] private CrowdSystem crowdSystem;
    [SerializeField] private PlayerAnimator playerAnimator;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void OnEnable()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void Update()
    {
        if (canMove)
        {
            MoveForward();
            ManageControl();
        }           
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        if (gameState == GameState.Game)
            StartMoving();
        else if (gameState == GameState.GameOver || gameState == GameState.LevelComplete)
            StopMoving();
    }

    private void StartMoving() 
    {
        canMove = true;

        playerAnimator.Run();
    }

    private void StopMoving()
    {
        canMove = false;

        playerAnimator.Idle();
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition; 
            clickedPlayerPosition = transform.position;
        } 
        else if (Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickedScreenPosition.x;

            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;

            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifference;

            position.x = Mathf.Clamp(position.x, -chunkWidth / 2 + crowdSystem.GetCrowdRadius(),
                chunkWidth / 2 - crowdSystem.GetCrowdRadius());

            transform.position = position;
        }
    }
}
