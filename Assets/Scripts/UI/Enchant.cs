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

    // �� ���� , ȭ�� , ��� ����, ��ġ���� ����������...

    private bool CheckAndUseMoney(int price)
    {
        if (price <= GameManager.Instance.player.Coin)
        {
            // ���� ������ ���°���
            GameManager.Instance.player.UseCoin(price);
            SoundInstance.Instance.InstantiateNormalSFX(7);

            return true;
        }
        else
        {
            GameManager.Instance.uiManager.InstantiateTextObject("���� �����մϴ�.");

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
