using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(gameObject.tag)
        {
            case "PlayerAttackObject":
                switch (collision.tag)
                {
                    case "Enemy":
                        collision.GetComponent<Enemy>().GetDamage(damage);
                        GameManager.Instance.InstantiateHitEffect(collision.transform);
                        GameManager.Instance.ShakeCamera(0.45f, 0.35f);
                        DestroyObject();
                        break;
                    case "Breakable":
                        collision.GetComponent<BreakableObject>().Hit();
                        GameManager.Instance.InstantiateHitEffect(collision.transform);
                        GameManager.Instance.ShakeCamera(0.45f, 0.35f);
                        break;
                    case "Lever":
                        collision.GetComponent<Lever>().Open();
                        GameManager.Instance.ShakeCamera(0.45f, 0.35f);
                        break;
                }
                break;
            case "EnemyAttackObject":
                switch(collision.tag)
                {
                    case "Player":
                        collision.GetComponent<Player>().GetDamage(damage);
                        DestroyObject();
                        break;
                }
                break;
        }
    }

    private void DestroyObject()
    {
        if (GetComponent<Bullet>() != null)
        {
            if (GetComponent<Bullet>() != null)
                SoundInstance.Instance.InstantiateNormalSFX(Random.Range(0, 2));

            Destroy(gameObject);
        }
    }
}
