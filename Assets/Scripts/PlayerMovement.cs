using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player movement")]
    [SerializeField] private float playerForce;
    [SerializeField] private float playerAimHitbox;
    [Header("Player movement visualisation")]
    [SerializeField] private Transform forceArrow;
    [SerializeField] private Transform forceArrowBody;
    [SerializeField] private Transform forceArrowHead;

    private Controls controls;
    private bool leftMousePressedLastFrame = false;
    private Vector2 mousePositionLastFrame = Vector2.zero;
    private new Rigidbody2D rigidbody;
    private bool aimingWallet = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        controls = new Controls();
        controls.Enable();
    }

    private void Update()
    {
        bool leftMousePressed = controls.Gameplay.LeftMouse.ReadValue<float>() == 1;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(controls.Gameplay.MousePosition.ReadValue<Vector2>());
        // Player launching wallet
        if (leftMousePressed && !leftMousePressedLastFrame &&
            // Checking if the player clicked on the wallet
            Vector2.Distance(transform.position, mousePosition) < playerAimHitbox)
        {
            aimingWallet = true;
        }
        else if (leftMousePressed && leftMousePressedLastFrame)
        {
            DrawLaunchDirectionArrow();
        }
        else if (aimingWallet && !leftMousePressed && leftMousePressedLastFrame)
        {
            LaunchWallet(mousePosition);
        }
        leftMousePressedLastFrame = leftMousePressed;
        mousePositionLastFrame = mousePosition;
    }

    private void LaunchWallet(Vector2 mousePosition)
    {
        aimingWallet = false;
        Vector2 force = (Vector2)transform.position - mousePosition;
        force *= playerForce;
        rigidbody.AddForce(force);
    }

    private void DrawLaunchDirectionArrow()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerAimHitbox);
    }
}
