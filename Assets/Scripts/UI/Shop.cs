using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class Shop : MonoBehaviour
{
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

    public void BuyPotion(int price)
    {
        if (GameManager.Instance.player.Health >= Player.maxHealth)
        {
            GameManager.Instance.uiManager.InstantiateTextObject("최대 체력 이상으로 회복 할 수 없습니다.");
            return;
        }

        if (!CheckAndUseMoney(price)) return;

        GameManager.Instance.player.GetHealth(30.0f);
    }

    public void BuyArrow(int price)
    {
        if (GameManager.Instance.player.playerAttack.arrow >= PlayerAttack.maxArrow)
        {
            GameManager.Instance.uiManager.InstantiateTextObject("이미 화살이 최대 보유량입니다.");
            return;
        }
        if (!CheckAndUseMoney(price)) return;

        GameManager.Instance.player.playerAttack.GetArrow();
    }

    public void BuySecretKey(int price)
    {
        if (GameManager.Instance.player.isHaveSecretKey == true)
        {
            GameManager.Instance.uiManager.InstantiateTextObject("이미 비밀 열쇠를 가지고 있습니다.");
            return;
        }

        if (!CheckAndUseMoney(price)) return;

        GameManager.Instance.player.isHaveSecretKey = true;
    }

    public void BuyTouch(int price)
    {
        if (GameManager.Instance.player.isHaveTouch == true)
        {
            GameManager.Instance.uiManager.InstantiateTextObject("이미 토치를 가지고 있습니다.");
            return;
        }
        if (!CheckAndUseMoney(price)) return;

        GameManager.Instance.player.GetComponentInChildren<Light2D>().intensity = 0.75f;
        GameManager.Instance.player.GetComponentInChildren<Light2D>().pointLightOuterRadius = 4f;

        GameManager.Instance.player.isHaveTouch = true;
    }
}
