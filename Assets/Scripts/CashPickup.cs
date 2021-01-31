using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPickup : MonoBehaviour
{
    [SerializeField] private int cashAmount;
    [SerializeField] private LayerMask playerLayerMask;
    private CashCount cashCount;

    private void Awake()
    {
        cashCount = FindObjectOfType<CashCount>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("beef");
        print( playerLayerMask.value);
        print(1 << collision.gameObject.layer);
        if (1 << collision.gameObject.layer == playerLayerMask.value)
        {
            cashCount.AddCash(cashAmount);
            Destroy(gameObject);
        }
    }
}
