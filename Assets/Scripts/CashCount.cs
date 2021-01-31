using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashCount : MonoBehaviour
{
    [SerializeField] private int startingCash = 0;
    [Header("Cash loss calculations")]
    [SerializeField] private int minCashLoss;
    [SerializeField] private int cashForceLossMultiplier;
    public int Cash { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ResetCash();
    }

    public void JumpLoseCash(float force)
    {
        RemoveCash((int)(minCashLoss + force * cashForceLossMultiplier));
    }

    public void ResetCash()
    {
        Cash = startingCash;
    }

    public void AddCash(int amount)
    {
        Cash += amount;
    }

    public void RemoveCash(int amount)
    {
        Cash -= amount;
    }
}
