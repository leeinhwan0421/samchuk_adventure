using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class SecretGround : MonoBehaviour
{
    private Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private IEnumerator SetVisible(float intensity)
    {
        float first = tilemap.color.a;

        if (first > intensity)
        {
            while (true)
            {
                first -= Time.deltaTime;

                yield return new WaitForEndOfFrame();

                tilemap.color = Color.white * first;

                if (first <= intensity)
                {
                    break;
                }
            }
        }

        else if (first < intensity)
        {
            while (true)
            {
                first += Time.deltaTime;

                yield return new WaitForEndOfFrame();

                tilemap.color = Color.white * first;

                if (first >= intensity)
                {
                    break;
                }
            }
        }

        tilemap.color = Color.white * first;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                StartCoroutine(SetVisible(0.5f));
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                StartCoroutine(SetVisible(1.0f));
                break;
        }
    }
}
