using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerForce;
    [SerializeField] private float playerAimHitbox;

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
        if (leftMousePressed && !leftMousePressedLastFrame)
        {
            if (Vector2.Distance(transform.position, mousePosition) < playerAimHitbox)
            {
                aimingWallet = true;
            }
        }
        else if (leftMousePressed && leftMousePressedLastFrame)
        {
            // TODO draw arrow to show force
        }
        else if (!leftMousePressed && leftMousePressedLastFrame)
        {
            if (aimingWallet)
            {
                aimingWallet = false;
                Vector2 force = (Vector2)transform.position - mousePosition;
                force *= playerForce;
                rigidbody.AddForce(force);
            }
        }
        leftMousePressedLastFrame = leftMousePressed;
        mousePositionLastFrame = mousePosition;
    }
}
