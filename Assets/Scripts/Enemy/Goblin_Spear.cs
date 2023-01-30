using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Goblin_Spear : Enemy
{
    [SerializeField] private GameObject spear;
    [SerializeField] private GameObject muzzle;

    private void Shot()
    {
        GameObject spear = Instantiate(this.spear, muzzle.transform.position, Quaternion.identity);

        if (spear.GetComponent<Bullet>() == null) return;
        if (GameManager.Instance.player == null) return;

        spear.GetComponent<Bullet>().SetBullet(GameManager.Instance.player.transform.position - transform.position, 8.0f);
    }
}
