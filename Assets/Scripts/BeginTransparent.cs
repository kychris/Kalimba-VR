using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginTransparent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color c = this.GetComponent<MeshRenderer>().material.color;
        c.a = 0;
        this.GetComponent<MeshRenderer>().material.color = c;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
