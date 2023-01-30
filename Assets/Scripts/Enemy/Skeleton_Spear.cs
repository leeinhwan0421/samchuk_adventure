using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Skeleton_Spear : Enemy
{
    [SerializeField] private Collider2D attackCollider;

    private void SetAttackCollider2D(int isActive) => attackCollider.enabled = isActive == 0 ? false : true;
}
