using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Player":
                collision.GetComponent<Player>().GetDamage(damage);
                GameManager.Instance.ShakeCamera(1.0f, 0.35f);

                float x = Random.Range(-2.0f, 2.0f);
                float y = Random.Range(5.0f, 7.0f);

                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
                break;
        }
    }
}
