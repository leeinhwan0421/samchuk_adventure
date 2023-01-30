using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerAttack : MonoBehaviour
{
    public enum State
    {
        NONE,
        SWORD,
        SPEAR,
        BOW
    }

    public bool isAttacking = false;

    [Header("AnimatorController")]
    [SerializeField] private State state = State.NONE;
    [SerializeField] private List<RuntimeAnimatorController> controllerList = new List<RuntimeAnimatorController>();

    [Header("AttackCollision")]
    [SerializeField] private Collider2D sword;
    [SerializeField] private Collider2D spear;

    public Collider2D Sword { get { return sword; } }
    public Collider2D Spear { get { return spear; } }

    [Header("ArrowPosition")]
    [SerializeField] private Transform arrowPos;

    public int arrow = 0;
    public const int maxArrow = 40;

    private Animator anim;
    private PlayerMove playerMove;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    private void FixedUpdate()
    {
        ChangeAnimatorRuntimeController();
    }

    private void Update()
    {
        CheckMoveState();
        GetInput();
    }

    private void ChangeAnimatorRuntimeController()
    {
        if (anim.runtimeAnimatorController == controllerList[(int)state]) return;

        anim.runtimeAnimatorController = controllerList[(int)state];
        sword.enabled = false;
        isAttacking = false;

        switch (state)
        { 
            case State.SPEAR:
                sword.enabled = false;
                break;
            case State.SWORD:
                spear.enabled = false;
                break;
            default:
                sword.enabled = false;
                spear.enabled = false;
                break;
        }

    }

    public void GetArrow()
    {
        arrow++;

        if (arrow >= maxArrow)
        {
            arrow = maxArrow;
        }

        GameManager.Instance.uiManager.SetArrowText(arrow.ToString("00"));
    }

    private void CheckMoveState()
    {
        if(playerMove._State == PlayerMove.State.FALL || 
           playerMove._State == PlayerMove.State.RISE ||
           playerMove._State == PlayerMove.State.DIE)
        {
            isAttacking = false;
        }    
    }

    private void GetInput()
    {
        if (state == State.NONE || playerMove._State == PlayerMove.State.DIE) return;
        if (playerMove._State == PlayerMove.State.FALL || playerMove._State == PlayerMove.State.RISE) return;
        if (anim.runtimeAnimatorController != controllerList[(int)state]) return;
        if (!Input.GetKeyDown(KeyCode.J)) return;
        if (isAttacking == true) return;
        if (GameManager.Instance.talkManager.isAction) return;

        switch (state)
        {
            case State.SWORD:
                int key_sword = Random.Range(1, 4);
                SoundInstance.Instance.InstantiatePlayerSFX(Random.Range(0,2));
                anim.SetTrigger("AttackState" + key_sword.ToString());
                break;
            case State.SPEAR:
                int key_spear = Random.Range(1, 3);
                SoundInstance.Instance.InstantiatePlayerSFX(Random.Range(2, 4));
                anim.SetTrigger("AttackState" + key_spear.ToString());
                break;
            case State.BOW:
                if (arrow <= 0)
                {
                    GameManager.Instance.uiManager.InstantiateTextObject("화살이 부족합니다.");
                    return;
                }
                anim.SetTrigger("Attack");
                break;
        }

        isAttacking = true;
    }

    public void SetState(PlayerAttack.State state)
    {
        this.state = state;
    }

    private void SetAttackState(int isAttacking) => this.isAttacking = isAttacking == 0 ? false : true;

    private void SetSwordCollider2D(int isActive) => sword.enabled = isActive == 0 ? false : true;

    private void SetSpearCollider2D(int isActive) => spear.enabled = isActive == 0 ? false : true;

    private void ShotArrow(GameObject arrow)
    {
        this.arrow--;

        GameManager.Instance.uiManager.SetArrowText(this.arrow.ToString("00"));

        SoundInstance.Instance.InstantiatePlayerSFX(4);

        GameObject obj = Instantiate(arrow, arrowPos.position, Quaternion.identity);
        obj.GetComponent<Arrow>().SetBullet(arrowPos.position - transform.position, 10.0f);
    }
}
