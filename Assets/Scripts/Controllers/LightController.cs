using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    Light light;
    bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        light = gameObject.GetComponent<Light>();
        light.intensity = 0; //default dark
    }

    public void StartFadeIn() { 
        StartCoroutine("FadeIn"); 
    }
    IEnumerator FadeIn() 
    {
        float BlendVal = 0;
        float tParam = 0.0f;
        float speed = 0.5f;

        while (tParam < 1) 
        {
            tParam += Time.deltaTime * speed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            BlendVal = Mathf.Lerp(0f, 1.5f, tParam);
            light.intensity = BlendVal;
            yield return null;
        }
    }


    public void StartFadeOut() { StartCoroutine("FadeOut"); }
    IEnumerator FadeOut() 
    {
        float BlendVal = 1.5f;
        float tParam = 0.0f;
        float speed = 0.5f;

        while (tParam < 1) 
        {
            tParam += Time.deltaTime * speed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            BlendVal = Mathf.Lerp(1.5f, 0, tParam);
            light.intensity = BlendVal;
            yield return null;
        }
    }
}
