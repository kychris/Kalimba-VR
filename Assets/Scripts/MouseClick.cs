using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MouseClick : MonoBehaviour
{
    public int keyNum;
    public PlayableDirector timeline;
    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Pause();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // this object was clicked - do something
    void OnMouseDown()
    {
        Debug.Log("hello" + keyNum);
        audioData.Play(0);
        if (keyNum == 1 || keyNum == 2)
        {
            timeline.Play();
        }
    }
}
