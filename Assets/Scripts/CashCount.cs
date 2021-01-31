using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashCount : MonoBehaviour
{
    [SerializeField] private int startingCash = 0;
    [Header("Cash loss calculations")]
    [SerializeField] private int minCashLoss;
    [SerializeField] private int cashForceLossMultiplier;
    [SerializeField] GameObject moneyText;

    [Header("Overlay")]
    [SerializeField] private GameObject deathScreen;

    public int Cash { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ResetCash();
    }

    private void LateUpdate()
    {
        if (moneyText)
        {
            moneyText.GetComponent<TMPro.TextMeshProUGUI>().text = $"$ {this.Cash}";
        }
    }

    private void ShowLoseOverlay()
    {
        Time.timeScale = 0;
        deathScreen.SetActive(true);
    }

    public void JumpLoseCash(float force)
    {
        if (0 >= Cash)
        {
            ShowLoseOverlay();
        }
        else
        {
            RemoveCash((int)(minCashLoss + force * cashForceLossMultiplier));
        }
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
