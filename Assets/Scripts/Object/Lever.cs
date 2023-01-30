using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private Animator anim;
    private Collider2D coll;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    public void Open()
    {
        anim.SetTrigger("Open");

        coll.enabled = false;

        SoundInstance.Instance.InstantiateNormalSFX(5);

        GameManager.Instance.uiManager.InstantiateTextObject("��� ���� ������ �Ҹ��� ����.");

        door.GetComponent<Door>().Open();
    }
}
