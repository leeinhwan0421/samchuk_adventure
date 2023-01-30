using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float range;

    private Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (coll == null)
            coll = GetComponent<Collider2D>();

        Vector3 from = new Vector3(transform.position.x, coll.bounds.min.y);
        Vector3 to = from + Vector3.up * range;

        Gizmos.DrawLine(from, to);
    }

    public void Open()
    {
        StartCoroutine(Move());

        coll.enabled = false;
    }

    private IEnumerator Move()
    {
        Vector3 from = transform.position;
        Vector3 to = from + Vector3.up * range;

        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime;

            transform.position = Vector3.Lerp(from, to, timer);

            if (timer >= 1.0f)
            {
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
