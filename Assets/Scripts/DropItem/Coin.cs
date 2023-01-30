using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rigid.velocity.y > 0.0f)
            rigid.gravityScale = 1.0f;
        else
            rigid.gravityScale = 2.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Player":
                collision.transform.GetComponent<Player>().GetCoin();
                SoundInstance.Instance.InstantiateNormalSFX(7);
                GameManager.Instance.InstantiateCoinEffect(transform);
                Destroy(gameObject);
                break;
        }
    }
}
