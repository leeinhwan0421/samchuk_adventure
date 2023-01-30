using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    private int hitCount = 0;
    private const int maxHitCount = 3;

    private SpriteRenderer sr;
    private Animator anim;

    [SerializeField] private int EA;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void Hit()
    {
        hitCount++;

        SoundInstance.Instance.InstantiateNormalSFX(Random.Range(2, 5));

        StartCoroutine(HitEffect());

        if (maxHitCount <= hitCount)
        {
            anim.SetTrigger("Destroy");
        }
    }

    private void DropItem()
    {
        GameManager.Instance.DropCoin(transform, EA);
    }

    private void DestroyThisObject()
    {
        Destroy(gameObject);
    }

    private IEnumerator HitEffect()
    {
        sr.color = Color.red;

        yield return new WaitForSeconds(0.08f);

        sr.color = Color.white;
    }
}
