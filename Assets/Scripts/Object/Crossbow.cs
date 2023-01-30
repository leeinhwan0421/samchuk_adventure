using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject muzzle;

    private void Shot()
    {
        GameObject arrow = Instantiate(this.arrow, muzzle.transform.position, Quaternion.identity);

        float x = -transform.localScale.x;

        arrow.GetComponent<Bullet>().SetBullet(new Vector2(x, 0.0f), 8f);
    }
}
