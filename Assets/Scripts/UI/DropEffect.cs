using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEffect : MonoBehaviour
{
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        transform.Translate(Vector3.down * Time.deltaTime * 108f);
    }
}