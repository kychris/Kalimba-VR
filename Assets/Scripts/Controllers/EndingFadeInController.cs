using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingFadeInController : MonoBehaviour
{

    public GameObject endingWords;
    // Start is called before the first frame update
    void Start()
    {
        StartFadeIn();
    }

    public void StartFadeIn() { StartCoroutine("FadeIn"); }
    IEnumerator FadeIn() 
    {
        float BlendVal;
        float tParam = 0.0f;
        float speed = 0.2f;
        Color c = endingWords.GetComponent<MeshRenderer>().material.color;

        while (tParam < 1) 
        {
            tParam += Time.deltaTime * speed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            BlendVal = Mathf.Lerp(0.0f, 1.0f, tParam);
            c.a = BlendVal;
            endingWords.GetComponent<MeshRenderer>().material.color = c;
            yield return null;
        }
    }
}
