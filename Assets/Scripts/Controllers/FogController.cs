using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricFogAndMist;

public class FogController : MonoBehaviour
{
    VolumetricFog fog;
    bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        fog = gameObject.GetComponent<VolumetricFog>();
    }

    public void StartFadeOut()
    {
        if (!triggered)
            StartCoroutine("FadeOut");
    }
    IEnumerator FadeOut()
    {
        float BlendVal = 1.25f;
        float tParam = 0.0f;
        float speed = 0.05f;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            BlendVal = Mathf.Lerp(1.25f, 0f, tParam);
            fog.density = BlendVal;
            yield return null;
        }
        triggered = true;
    }
}
