using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerForce;
    [SerializeField] private float playerAimHitbox;

    // particle systems
    [SerializeField] private GameObject loseParticleSystem;
    [SerializeField] private GameObject fakeParticleSystem;

    // camera's
    [SerializeField] private GameObject virtualPlayerCam;

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

                // add particle effects
                Instantiate(loseParticleSystem, transform);
                Instantiate(fakeParticleSystem, transform);

                // camera shake
                virtualPlayerCam.GetComponent<CinemachineCameraShaker>().ShakeCamera(0.1f);

                // add function to remove money from score
                // losemoney(force, direction);
            }
        }
        leftMousePressedLastFrame = leftMousePressed;
        mousePositionLastFrame = mousePosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerAimHitbox);
    }
}
