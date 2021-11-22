using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCountShower : MonoBehaviour
{
    private CashCount cashCount;
    private Text text;

    private void Awake()
    {
        //cashCount = FindObjectOfType<CashCount>();
       // text = GetComponent<Text>();
    }

    private void Start()
    {
        //text.text = "$" + cashCount.Cash;
    }
}
