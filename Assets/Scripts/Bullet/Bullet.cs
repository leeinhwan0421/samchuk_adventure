using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    private float moveSpeed = 0.0f;

    private Vector2 direction = Vector2.zero;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        DestroyCheck();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void DestroyCheck()
    {
        Vector3 trans =  Camera.main.WorldToViewportPoint(transform.position);

        if (trans.y >= 1.10f || trans.y <= -0.10f ||
            trans.x > 1.00f || trans.x < 0.00f)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        // Scene View Camera에서도 완전히 벗어나야 삭제가 됨.
        // 즉 빌드할 때는 상관 없음 ^^;
        Destroy(this.gameObject);
    }

    private void Movement()
    {
        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime * moveSpeed);
    }

    public void SetBullet(Vector2 direction, float moveSpeed)
    {
        this.direction = direction.normalized;
        this.moveSpeed = moveSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 1);
        transform.rotation = rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Ground":
                if (!GetComponent<Damage>())
                    return;

                GetComponent<Damage>().enabled = false;
                GetComponent<Collider2D>().enabled = false;

                direction = Vector2.zero;
                moveSpeed = 0.0f;

                sr.sortingOrder = -4;
                break;
        }
    }
}
