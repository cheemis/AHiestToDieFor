using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTitle : MonoBehaviour
{

    public Animator fadeAnim;
    private bool WaitCoOn = false;

    public void TitleScreen()
    {
        if(!WaitCoOn) {StartCoroutine("FadeOut");}
    }

    IEnumerator FadeOut()
    {
        WaitCoOn = true;
        fadeAnim.SetBool("Fade",true);
        yield return new WaitForSeconds(1.01f);
        SceneManager.LoadSceneAsync(0);
    }
}
