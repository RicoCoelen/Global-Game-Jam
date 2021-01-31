using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private CashCount cashCount;

    private void Awake()
    {
        cashCount = FindObjectOfType<CashCount>();
    }

    private void Start()
    {
        cashCount.ResetCash();
    }
}
