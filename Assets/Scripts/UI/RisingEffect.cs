using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RisingEffect : MonoBehaviour
{
    private Text m_Text;
    private float timer = 1.0f;

    private void Awake()
    {
        m_Text = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        transform.Translate(200 * Time.fixedDeltaTime * Vector3.up);
        m_Text.color = Color.white * timer;

        timer -= Time.fixedDeltaTime;

        if (timer <= 0.0f)
            Destroy(gameObject);
    }
}
