using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    public int previousIndex;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void getCurrentIndex()
    {
        print(SceneManager.GetActiveScene().buildIndex);
        previousIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
