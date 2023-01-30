using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Skeleton_Archer : Enemy
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject muzzle;

    private void Shot()
    {
        GameObject arrow = Instantiate(this.arrow, muzzle.transform.position, Quaternion.identity);

        if (arrow.GetComponent<Bullet>() == null) return;
        if (GameManager.Instance.player == null) return;

        arrow.GetComponent<Bullet>().SetBullet(GameManager.Instance.player.transform.position - transform.position, 8.0f);
    }
}
