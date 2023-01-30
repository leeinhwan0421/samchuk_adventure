using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enchant : MonoBehaviour
{
    [SerializeField] private GameObject arrow;

    private void OnEnable()
    {
        GameManager.Instance.talkManager.isAction = true;
    }

    private void OnDisable()
    {
        GameManager.Instance.talkManager.isAction = false;
        GameManager.Instance.talkManager.talkPanel.SetActive(false);
    }

    // 힐 포션 , 화살 , 비밀 열쇠, 토치까지 만들어놓았음...

    private bool CheckAndUseMoney(int price)
    {
        if (price <= GameManager.Instance.player.Coin)
        {
            // 돈이 있으면 쓰는거임
            GameManager.Instance.player.UseCoin(price);
            SoundInstance.Instance.InstantiateNormalSFX(7);

            return true;
        }
        else
        {
            GameManager.Instance.uiManager.InstantiateTextObject("돈이 부족합니다.");

            return false;
        }
    }

    public void EnchantWeapon(string weaponName)
    {
        if (!CheckAndUseMoney(5))
            return;

        switch (weaponName)
        {
            case "Sword":
                GameManager.Instance.player.playerAttack.Sword.GetComponent<Damage>().damage += 2.5f;
                break;
            case "Spear":
                GameManager.Instance.player.playerAttack.Spear.GetComponent<Damage>().damage += 2;
                break;
            case "Bow":
                arrow.GetComponent<Damage>().damage++;
                break;
        }
    }
}
