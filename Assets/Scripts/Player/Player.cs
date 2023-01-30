using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player : MonoBehaviour
{
    [Header("other Scripts Value")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;

    public PlayerMove playerMove;
    public PlayerAttack playerAttack;

    [Header("Status")]
    public const float maxHealth = 100.0f;
    [SerializeField] private float health = maxHealth;
    public float Health { get { return health; } }
    [SerializeField] private int coin;
    public int Coin { get { return coin; } }

    [Header("Invincible")]
    private const float invincibleTime = 1.15f;
    private float invincibleTimer = 0.0f;

    private GameObject scanObject;
    private GameObject itemObject;
    private GameObject chestObject;

    private SpriteRenderer sr;

    [Header("Shop Value")]
    public bool isHaveSecretKey = false;
    public bool isHaveTouch = false;

    [Header("Cheat")]
    public bool isInvincible = false;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();

        sr = GetComponent<SpriteRenderer>();

        playerMove.SetMoveSpeed(moveSpeed);
        playerMove.SetJumpPower(jumpPower);
    }

    private void Update()
    {
        Talking();
        Getting();
        Opening();

        invincibleTimer -= Time.deltaTime;
    }

    public void SetInvincibleTime(float sec)
    {
        invincibleTimer = sec;

        StartCoroutine(InvincibleEffect());
    }

    private void Opening()
    {
        if (chestObject == null) return;

        if (Input.GetKeyDown(KeyCode.E))
            chestObject.GetComponent<Chest>().Open();
    }

    private void Getting()
    {
        if (itemObject == null) return;

        if (Input.GetKeyDown(KeyCode.E))
            itemObject.GetComponent<WeaponItem>().UseItem();
    }

    private void Talking()
    {
        if (scanObject == null) return;

        if (Input.GetKeyDown(KeyCode.E))
            GameManager.Instance.talkManager.Action(scanObject);
    }

    public void GetCoin()
    {
        coin++;

        if (coin >= 999)
        {
            coin = 999;
        }

        GameManager.Instance.uiManager.SetCoinText(coin.ToString("000"));
    }

    public void UseCoin(int price)
    {
        coin -= price;

        if (coin <= 0)
        {
            coin = 0;
        }

        GameManager.Instance.uiManager.SetCoinText(coin.ToString("000"));
    }

    public void GetDamage(float value)
    {
        if (isInvincible) return;
        if (health == 0) return;
        if (invincibleTimer > 0) return;

        health -= value;

        if (health <= 0)
        {
            playerMove._State = PlayerMove.State.DIE;
            GetComponent<Animator>().SetTrigger("Die");
            Invoke(nameof(Die), 1.25f);
            health = 0;
        }

        StartCoroutine(HitEffect());

        SoundInstance.Instance.InstantiatePlayerSFX(Random.Range(5, 7));

        GameManager.Instance.ShakeCamera(0.75f, 0.35f);
        GameManager.Instance.uiManager.SetHealthImage(health, maxHealth);
    }

    public void GetHealth(float value)
    {
        health += value;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        GameManager.Instance.uiManager.SetHealthImage(health, maxHealth);
    }

    private IEnumerator HitEffect()
    {
        SetInvincibleTime(invincibleTime);

        sr.color = Color.red;

        yield return new WaitForSeconds(0.08f);

        sr.color = Color.white;
    }

    private IEnumerator InvincibleEffect()
    {
        while(true)
        {
            sr.color = Color.white * 0.9f;

            yield return new WaitForSeconds(0.08f);

            if (invincibleTimer <= 0.0f)
            {
                sr.color = Color.white;
                yield break;
            }
        }
    }

    private void Die()
    {
        GameManager.Instance.uiManager.die.SetActive(true);
        SoundInstance.Instance.InstantiateNormalSFX(8);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Npc":
                scanObject = collision.gameObject;
                break;
            case "Item":
                itemObject = collision.gameObject;
                break;
            case "Chest":
                chestObject = collision.gameObject;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Npc":
                scanObject = null;
                break;
            case "Item":
                itemObject = null;
                break;
            case "Chest":
                chestObject = null;
                break;
        }
    }
}