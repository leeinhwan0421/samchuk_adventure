using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private bool isNeedKey = false;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        if (isNeedKey)
        {
            if (GameManager.Instance.player.isHaveSecretKey)
            {
                anim.SetTrigger("Open");
                SoundInstance.Instance.InstantiateNormalSFX(6);
            }
            else
            {
                GameManager.Instance.uiManager.InstantiateTextObject("비밀 열쇠가 필요합니다.");
                return;
            }
        }

        anim.SetTrigger("Open");
        SoundInstance.Instance.InstantiateNormalSFX(6);
    }

    private void EnableContent() => content.SetActive(true);
}
