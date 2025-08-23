using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    Vector2 initialPosition;
    [SerializeField]
    GameObject InstructionsPC;

    InputSystem_Actions inputActions;
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    float speedMultiplier { get; set; }
    float initialSpeed;

    void Awake()
    {
        initialSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputActions = new InputSystem_Actions();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Sprint.performed += OnSprint;
        inputActions.Player.Sprint.canceled += StopSprint;
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    void FixedUpdate()
    {
        if (GameStateManager.Instance.CurrentGameState == GameState.PLAYING)
        {
            if (moveInput != Vector2.zero)
                InstructionsPC.SetActive(false);

            // Restricción para evitar movimiento diagonal  
            if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
                moveInput.y = 0;
            else
                moveInput.x = 0;

            transform.Translate(speed * Time.deltaTime * moveInput);

            // Animacion movimiento
            AnimateMovement();
        }
    }

    void AnimateMovement()
    {
        var infoAnim = animator.GetCurrentAnimatorStateInfo(0);

        if (!infoAnim.IsName("Ninja_Green_Idle") && moveInput == Vector2.zero)
            animator.Play("Ninja_Green_Idle");
        else if (!infoAnim.IsName("Ninja_Green_Walk_Top") && moveInput.y > 0)
            animator.Play("Ninja_Green_Walk_Top");
        else if (!infoAnim.IsName("Ninja_Green_Walk_Down") && moveInput.y < 0)
            animator.Play("Ninja_Green_Walk_Down");
        else if (!infoAnim.IsName("Ninja_Green_Walk_Right") && moveInput.x > 0)
            animator.Play("Ninja_Green_Walk_Right");
        else if (!infoAnim.IsName("Ninja_Green_Walk_Left") && moveInput.x < 0)
            animator.Play("Ninja_Green_Walk_Left");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food") && GameStateManager.Instance.CurrentGameState == GameState.PLAYING)
        {
            ScoreManager.Instance.UpdateScore(1);
            FoodManager.Instance.ChangeActiveFood(false);
        }
    }

    void StopSprint(InputAction.CallbackContext callbackContext)
    {
        speed = initialSpeed;
    }

    void OnSprint(InputAction.CallbackContext callbackContext)
    {
        if (GameStateManager.Instance.CurrentGameState == GameState.PLAYING)
            speed *= 1.5f;
    }

    public void ResetPosition()
    {
        InstructionsPC.SetActive(true);
        StopMovement();
        transform.position = initialPosition;
    }

    void StopMovement()
    {
        speed = initialSpeed;
        moveInput = Vector2.zero;
    }
}
