using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private bool isRestart = false;
    [SerializeField] private int sceneIndex;
    private GameObject restartManager;
    private RestartScript restartFunction;
    private bool runOnce = false;

    [SerializeField] private AudioClip selectSound = null;
    private SoundManager soundManager;

    public void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        restartManager = GameObject.FindGameObjectWithTag("LevelRestarter");
        restartFunction = restartManager.GetComponent<RestartScript>();

    }

    public void switchScene()
    {
        soundManager.PlaySingle(selectSound);
        SceneManager.LoadScene(sceneIndex);
    }

    public void nextLevel()
    {
        soundManager.PlaySingle(selectSound);
        SceneManager.LoadScene(restartFunction.previousIndex + 1);
    }

    public void Update()
    {
        if (runOnce == false)
        {
            if (isRestart)
            {
                sceneIndex = restartFunction.previousIndex;
            }
            runOnce = true;
        }
    }

}
