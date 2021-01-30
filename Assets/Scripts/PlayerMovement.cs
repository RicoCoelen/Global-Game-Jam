using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Controls controls;
    private bool leftMousePressedLastFrame = false;
    private Vector2 mousePositionLastFrame = Vector2.zero;
    private new Rigidbody2D rigidbody;

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
        leftMousePressedLastFrame = leftMousePressed;
        mousePositionLastFrame = mousePosition;
    }
}
