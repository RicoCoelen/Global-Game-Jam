using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeefyDetectionScript : MonoBehaviour
{
    public float distance;
    public LayerMask layerMask;
    public int damage;
    private CashCount cashCounter;

    private void Start()
    {
        cashCounter = GameObject.FindGameObjectWithTag("CashCounter").GetComponent<CashCount>();
    }

    void Update()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, distance, layerMask);

        if(hit.Length > 0)
            cashCounter.RemoveCash(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, distance);
    }
}
