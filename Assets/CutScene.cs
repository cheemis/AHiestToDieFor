using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public Camera camera;
    public GameObject firstScene;
    public GameObject secondScene;
    public GameObject thirdScene;

    private Vector3 nextPoint = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nextPoint != Vector3.zero)
        {
            Vector3 slerped = Vector3.Lerp(camera.transform.position, nextPoint, Time.deltaTime * 2);
            camera.transform.position = slerped;
            if(slerped.magnitude < .1f) {nextPoint = Vector3.zero;}
        }
    }

    public void LoadSecond()
    {
        secondScene.SetActive(true);
        nextPoint = new Vector3(-56.32609f, -14.5f, -2.6f);
        firstScene.SetActive(false);
    }

    public void LoadThird()
    {
        secondScene.SetActive(false);
        nextPoint = new Vector3(-107.02f, -12.5f, 14.68f);
        thirdScene.SetActive(true);
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(3);
    }
}
