using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    Animator anim;
    bool triggered = false;

    // Start is called before the first frame update
    void Start() { anim = GetComponent<Animator>(); }

    // Update is called once per frame
    void Update() { }

    // Called to move into the fog
    public void StartMoveIn()
    {
        if (!triggered)
            StartCoroutine("MoveIn");
        triggered = true;
    }
    IEnumerator MoveIn()
    {
        anim.SetInteger("Walk", 1);
        float curX = 57f;
        float tParam = 0.0f;
        float speed = 0.1f;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            curX = Mathf.Lerp(57f, 61f, tParam);
            gameObject.transform.position = new Vector3(curX, gameObject.transform.position.y, gameObject.transform.position.z);
            yield return null;
        }

        anim.SetInteger("Walk", 0);
    }

    public void StartTurn() { StartCoroutine("Turn"); }
    IEnumerator Turn()
    {
        anim.SetInteger("Walk", 1);
        float curRotateY = 92.459F;
        float tParam = 0.0f;
        float speed = 0.4f;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            curRotateY = Mathf.Lerp(92.459F, 180F, tParam);
            gameObject.transform.rotation = Quaternion.Euler(
                gameObject.transform.rotation.x,
                curRotateY,
                gameObject.transform.rotation.z
            );
            yield return null;
        }
        anim.SetInteger("Walk", 0);
    }
}
