using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Movement Info")]
    [SerializeField] private Vector2 moveDir;

    [Header("Movement Value")]
    [SerializeField] private float moveRange;
    [SerializeField] private float moveSpeed;

    private Vector3 maxPos;
    private Vector3 minPos;

    private float timer;

    private int nextMove;

    private void Awake()
    {
        maxPos = transform.position;
        minPos = transform.position + (Vector3)moveDir.normalized * moveRange;
    }

    private void OnDrawGizmos()
    {
        if (moveDir == Vector2.zero) return;

        Vector3 from = transform.position;
        Vector3 to = transform.position + (Vector3)moveDir * moveRange;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(from, to);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        timer += Time.fixedDeltaTime;

        // 0 ÇÏ°­ 1 »ó½Â

        if (nextMove == 0)
        {
            transform.position = Vector3.Lerp(maxPos, minPos, timer * moveSpeed); 

            if (timer * moveSpeed >= 1)
            {
                nextMove = 1;
                StartCoroutine(Stop(1.0f));
                timer = 0.0f;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(minPos, maxPos, timer * moveSpeed);

            if (timer * moveSpeed >= 1)
            {
                nextMove = 0;
                StartCoroutine(Stop(1.0f));
                timer = 0.0f;
            }
        }
    }

    private IEnumerator Stop(float duration)
    {
        float timer = 0.0f;

        while(timer < duration)
        {
            this.timer = 0.0f;

            yield return new WaitForEndOfFrame();

            timer += Time.deltaTime;
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.collider.tag)
        {
            case "Player":
                collision.transform.parent = this.transform;
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Player":
                collision.transform.parent = null;
                break;
        }
    }
    */
}
