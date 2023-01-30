using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Boar : Enemy
{
    [SerializeField] private Collider2D attackCollider;

    [Header("Speed Value")]
    [SerializeField] private float normalSpeed;
    [SerializeField] private float runSpeed;

    private void FixedUpdate()
    {
        if (state == State.TRACE)
            speed = runSpeed;
        else
            speed = normalSpeed;
    }

    private void SetAttackCollider2D(int isActive) => attackCollider.enabled = isActive == 0 ? false : true;
}
