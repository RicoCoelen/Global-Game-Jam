using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private bool isRestart = false;
    [SerializeField] private string sceneName;
    private GameObject restartManager;
    private RestartScript restartFunction;
    private bool runOnce = false;

    [SerializeField] private AudioClip selectSound = null;
    private SoundManager soundManager;

    public void Start()
    {
        var scuffed = GameObject.FindGameObjectWithTag("SoundManager");
        if (scuffed)
        soundManager = scuffed.GetComponent<SoundManager>();

        restartManager = GameObject.FindGameObjectWithTag("LevelRestarter");
        if (restartManager)
        restartFunction = restartManager.GetComponent<RestartScript>();

    }

    public void switchScene()
    {
        if(selectSound)
        soundManager.PlaySingle(selectSound);

        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void nextLevel()
    {
        soundManager.PlaySingle(selectSound);
        SceneManager.LoadScene(restartFunction.previousIndex + 1);
        Time.timeScale = 1;
    }
}
