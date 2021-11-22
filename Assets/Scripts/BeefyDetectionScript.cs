using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeefyDetectionScript : MonoBehaviour
{
    public float distance;
    public LayerMask layerMask;
    public int damage;
    private CashCount cashCounter;
    private PlayerMovement player;
    
    private void Start()
    {
        cashCounter = GameObject.FindGameObjectWithTag("CashCounter").GetComponent<CashCount>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, distance, layerMask);

        if(hit.Length > 0 && hit[0].gameObject != null)
        {
            player.WalletDamage(hit[0].gameObject, damage);
            if (damage < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, distance);
    }
}
