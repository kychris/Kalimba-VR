using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{

    Animator anim;
    bool triggered = false;
    // Start is called before the first frame update
    void Start() { 
        anim = GetComponent<Animator>(); 
    }

    public void StartMoveIn() { StartCoroutine("MoveIn"); }
    IEnumerator MoveIn() 
    {
        anim.SetInteger("Walk", 1);
        float curX = 72f;
        float tParam = 0.0f;
        float speed = 0.1f;

        while (tParam < 1) 
        {
            tParam += Time.deltaTime * speed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            curX = Mathf.Lerp(72f, 65.5f, tParam);
            gameObject.transform.position = new Vector3(curX, gameObject.transform.position.y, 39);
            yield return null;
        }
        anim.SetInteger("Walk", 0);
    }

    public void StartTurn() { StartCoroutine("Turn"); }
    IEnumerator Turn() 
    {
        anim.SetInteger("Left", 1);
        float curRotateY = -81.37f;
        float tParam = 0.0f;
        float speed = 0.4f;

        while (tParam < 1) 
        {
            tParam += Time.deltaTime * speed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            curRotateY = Mathf.Lerp(-81.37f, -200, tParam);
            gameObject.transform.rotation = Quaternion.Euler(
                gameObject.transform.rotation.x, 
                curRotateY, 
                gameObject.transform.rotation.z
            );
            yield return null;

            if (tParam > 0.88) { anim.SetInteger("Left", 0); }
        }  
    }
}
