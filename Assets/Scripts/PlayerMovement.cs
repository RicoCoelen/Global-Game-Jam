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
    [SerializeField] private float arrowWalletSeparationDistance;

    [Header("Particle Systems")]
    [SerializeField] private GameObject loseParticleSystem;
    [SerializeField] private GameObject fakeParticleSystem;

    [Header("Cameras")]
    [SerializeField] private GameObject virtualPlayerCam;

    private Controls controls;
    private bool leftMousePressedLastFrame = false;
    private new Rigidbody2D rigidbody;
    private bool aimingWallet = false;
    private CashCount cashCount;

    [Header("Distance to ground")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float distance;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        controls = new Controls();
        controls.Enable();
        cashCount = FindObjectOfType<CashCount>();

    }

    private void Update()
    {

        bool leftMousePressed = controls.Gameplay.LeftMouse.ReadValue<float>() == 1;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(controls.Gameplay.MousePosition.ReadValue<Vector2>());
        // Player launching wallet
        if (leftMousePressed && !leftMousePressedLastFrame &&
            // Checking if the player clicked on the wallet
            Vector2.Distance(transform.position, mousePosition) < playerAimHitbox &&
            isGrounded()
            )
        {
            aimingWallet = true;
            forceArrow.gameObject.SetActive(true);
        }
        if (leftMousePressed && aimingWallet)
        {
            DrawLaunchDirectionArrow(mousePosition);
        }
        else if (aimingWallet && !leftMousePressed)
        {
            forceArrow.gameObject.SetActive(false);
            LaunchWallet(mousePosition);
            LaunchWalletVisuals();
            cashCount.JumpLoseCash(Vector2.Distance(transform.position, mousePosition));
        }
        leftMousePressedLastFrame = leftMousePressed;
    }

    private void LaunchWallet(Vector2 mousePosition)
    {
        aimingWallet = false;
        Vector2 force = (Vector2)transform.position - mousePosition;
        force *= playerForce;
        rigidbody.AddForce(force);
    }

    private void LaunchWalletVisuals()
    {
        // add particle effects
        Instantiate(loseParticleSystem, transform);
        Instantiate(fakeParticleSystem, transform);

        // camera shake
        if (cashCount.Cash > 0)
            virtualPlayerCam.GetComponent<CinemachineCameraShaker>().ShakeCamera(0.1f);
    }

    private void DrawLaunchDirectionArrow(Vector2 mousePosition)
    {
        float mouseToWallet = Vector2.Distance(transform.position, mousePosition);
        // force arrow position and rotation
        float arrowDirection = Vector2.SignedAngle(Vector2.right, (Vector2)transform.position - mousePosition);
        forceArrow.localRotation = Quaternion.Euler(0, 0, arrowDirection);
        forceArrow.position = (Vector2)transform.position + ((mouseToWallet + arrowWalletSeparationDistance) / 2 * ((Vector2)transform.position - mousePosition).normalized);
        // force arrow body length
        Vector3 bodyScale = forceArrowBody.localScale;
        bodyScale.x = mouseToWallet;
        forceArrowBody.localScale = bodyScale;
        // force arrow head position
        forceArrowHead.localPosition = new Vector3(mouseToWallet / 2, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerAimHitbox);
    }

    private bool isGrounded()
    {
        float xOffset = GetComponent<Collider2D>().bounds.size.x/2;
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(transform.position.x - xOffset, transform.position.y), direction, distance, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x + xOffset, transform.position.y), direction, distance, groundLayer);

        if (hit1.collider != null || hit2.collider != null)
            return true;

        return false;
    }

    private void OnDrawGizmos()
    {
        float xOffset = GetComponent<Collider2D>().bounds.size.x / 2;
        Vector2 direction = Vector2.down * distance;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(transform.position.x - xOffset, transform.position.y), direction);
        Gizmos.DrawRay(new Vector2(transform.position.x + xOffset, transform.position.y), direction);
    }
}
