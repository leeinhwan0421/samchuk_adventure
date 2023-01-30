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

        GameManager.Instance.uiManager.InstantiateTextObject("어딘가 문이 열리는 소리가 났다.");

        door.GetComponent<Door>().Open();
    }
}
