using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{

    public static int scene;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoadNextLevel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(scene);
    }

}
