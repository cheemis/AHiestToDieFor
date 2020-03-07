using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunrise : MonoBehaviour
{
    private float duration = 300f;
    public float timePassed = 0f;
    public Material mat;

    private Color night = new Color32(68, 1, 141, 255);
    private Color day = new Color32(253, 184, 93, 255);

    private void Start()
    {
        mat.color = night;
    }

    // Update is called once per frame
    private void Update()
    {
        ColorShift();
    }

    private void ColorShift()
    {
        if (this.tag == "Sky")
        {
            mat.color = Color.Lerp(night, day, timePassed);

            if (timePassed < 1)
            {
                timePassed += Time.deltaTime / duration;
            }
        }
    }
}
