using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;
    private Animator animator;
    public float playerSpeed;
    private Vector2 playerVector;
    private static readonly int Left = Animator.StringToHash("Left");
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Down = Animator.StringToHash("Down");
    private static readonly int Up = Animator.StringToHash("Up");
    private static readonly int Idle = Animator.StringToHash("Idle");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Vertical.started += Move;
        playerControls.Player.Vertical.performed += Stop;
        playerControls.Player.Horizontal.started += Move;
        playerControls.Player.Horizontal.performed += Stop;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        playerVector = new Vector2(playerControls.Player.Horizontal.ReadValue<float>(),playerControls.Player.Vertical.ReadValue<float>());
        transform.Translate(playerVector * (playerSpeed * Time.deltaTime), Space.World);
    }

    private void Move(InputAction.CallbackContext callbackContext)
    {
        var movementInput = callbackContext.ReadValue<float>();
        if (callbackContext.action.name == "Vertical") {
            if (movementInput < 0) {
                animator.SetTrigger(Down);
            }
            else if (movementInput > 0) {
                animator.SetTrigger(Up);
            }
        }else if(callbackContext.action.name == "Horizontal"){
            if (movementInput < 0) {
                animator.SetTrigger(Left);
            }else if (movementInput > 0) {
                animator.SetTrigger(Right);
            }    
        }
    }

    private void Stop(InputAction.CallbackContext callbackContext)
    {
        var inputVector = new Vector2(playerControls.Player.Horizontal.ReadValue<float>(),
            playerControls.Player.Vertical.ReadValue<float>());
        if (inputVector.Equals(Vector2.zero))
            animator.SetTrigger(Idle);

        /*switch (callbackContext.action.name)
        {
            case "Vertical":
            {
                var movementInput = inputVector.x;
                if (movementInput < 0) {
                    animator.SetTrigger(Left);
                }
                else if (movementInput > 0) {
                    animator.SetTrigger(Right);
                }
                break;
            }
            case "Horizontal":
            {
                var movementInput = inputVector.y;
                if (movementInput < 0) {
                    animator.SetTrigger(Down);
                }else if (movementInput > 0) {
                    animator.SetTrigger(Up);
                }
                break;
            }
        }*/
    }
}
