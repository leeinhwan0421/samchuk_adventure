using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] private PlayerAttack.State state;

    [SerializeField] private GameObject effect;

    public void UseItem()
    {
        GameManager.Instance.player.playerAttack.SetState(state);

        Instantiate(effect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
