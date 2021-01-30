using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string levelSelectScene;

    public void StartGame()
    {
        SceneManager.LoadScene(levelSelectScene);
    }
}
