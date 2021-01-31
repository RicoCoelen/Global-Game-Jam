using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == playerLayerMask.value)
        {
            GetComponent<SceneSwitcher>().switchScene();
        }
    }
}
