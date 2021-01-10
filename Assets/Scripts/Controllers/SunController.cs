using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    Light light;
    bool triggered = false;
    // Start is called before the first frame update
    void Start() { 
        light = gameObject.GetComponent<Light>(); 
        light.intensity = 1f;

        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x, 
            0, 
            gameObject.transform.position.z
        );
        gameObject.transform.rotation = Quaternion.Euler(
            0, 
            -9.133f, 
            gameObject.transform.rotation.z
        );
    }

    public void StartFadeIn() { 
        StartCoroutine("FadeIn"); 
    }
    IEnumerator FadeIn() 
    {
        float BlendVal = 0;
        float tParam = 0.0f;
        float speed = 0.2f;

        while (tParam < 1) 
        {
            tParam += Time.deltaTime * speed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            BlendVal = Mathf.Lerp(0.1f, 1.0f, tParam);
            light.intensity = BlendVal;
            yield return null;
        }
    }

    public void StartMinorRise() { 
        if (!triggered)
            StartCoroutine("MinorRise"); 
    }
    IEnumerator MinorRise() 
    {
        float curRotateX = 0.0f;
        float tParam = 0.0f;
        float speed = 0.2f;

        while (tParam < 1) 
        {
            tParam += Time.deltaTime * speed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            curRotateX = Mathf.Lerp(0f, 8f, tParam);
            gameObject.transform.rotation = Quaternion.Euler(
                curRotateX, 
                -9.133f, 
                gameObject.transform.rotation.z
            );
            yield return null;
        }
        triggered = true;
    }

    public void StartFullRise() { StartCoroutine("FullRiseHeight"); StartCoroutine("FullRiseRotate"); }
    IEnumerator FullRiseHeight() 
    {
        float curHeight;
        float tParam = 0.0f;
        float speed = 0.05f;

        while (tParam < 1) 
        {
            tParam += Time.deltaTime * speed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            curHeight = Mathf.Lerp(4f, 40f, tParam);
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x, 
                curHeight, 
                gameObject.transform.position.z
            );
            yield return null;
        }
    }
    IEnumerator FullRiseRotate() 
    {
        float curRotateX = 0f;
        float tParam = 0.0f;
        float speed = 0.05f;

        while (tParam < 1) 
        {
            tParam += Time.deltaTime * speed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            curRotateX = Mathf.Lerp(8f, 15f, tParam);
            gameObject.transform.rotation = Quaternion.Euler(
                curRotateX, 
                -9.133f, 
                gameObject.transform.rotation.z
            );
            yield return null;
        }
    }

    

}
