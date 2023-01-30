using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerMove : MonoBehaviour
{
    private float moveSpeed;
    private float moveSpeedValue { 
        get 
        {   if (isRolling)
                return 1.55f;
            else
                return 1.0f;
        } 
    }

    private float jumpPower;
    private float jumpPowerValue = 1.0f;

    private const float checkGroundRange = 0.1f;
    private bool isGrounded = true;

    public enum State
    {
        IDLE,
        RUN,
        ROLL,
        RISE,
        FALL,
        DIE
    }

    private State state = State.IDLE;
    public State _State { get { return state; } set { state = value; } }

    private Vector2 direction = Vector2.zero;
    public Vector2 _Direction { get { return direction; } }

    private SpriteRenderer sr;
    private Rigidbody2D rigid;
    private Collider2D coll;
    private Animator anim;
    private PlayerAttack playerAttack;

    private int playerLayer;
    private int platformLayer;

    private const float roolTime = 1.0f;
    private float roolTimer;

    private bool isRolling = false;

    public void SetMoveSpeed(float moveSpeed) => this.moveSpeed = moveSpeed;
    public void SetJumpPower(float jumpPower) => this.jumpPower = jumpPower;
    public void SetRollState() => this.isRolling = false;
    private void SetAnimationByState() => anim.SetInteger("CurState", (int)state);

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        playerAttack = GetComponent<PlayerAttack>();

        playerLayer = LayerMask.NameToLayer("Player");
        platformLayer = LayerMask.NameToLayer("Platform");
    }

    private void Update()
    {
        if (state == State.DIE) return;

        Jump();

        GravityAndLayerCheck();
    }

    private void FixedUpdate()
    {
        if (state == State.DIE) return;

        roolTimer -= Time.deltaTime;

        Movement();
        CheckGrounded();

        SetAnimationByState();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (coll == null) return;

        Vector3 from = new Vector2(transform.position.x, coll.bounds.min.y);
        Vector3 to = new Vector2(transform.position.x, coll.bounds.min.y - checkGroundRange);

        Gizmos.DrawLine(from, to);
    }

    private void GravityAndLayerCheck()
    {
        // 속도
        if (rigid.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
        }

        // 키입력
        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(IgnorePlatformLayerWithDuration(0.5f));
        }
    }

    private IEnumerator IgnorePlatformLayerWithDuration(float duration)
    {
        float timer = 0;

        while(true)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);

            timer += Time.deltaTime;

            yield return new WaitForEndOfFrame();

            if (timer >= duration) break;
        }
    }

    private void Movement()
    {
        if (GameManager.Instance.talkManager.isAction)
        {
            state = State.IDLE;
            return;
        }

        if (playerAttack.isAttacking) return;

        direction.x = Input.GetAxisRaw("Horizontal");

        if (direction.x == 0)
        {
            state = State.IDLE;
            return;
        }

        if (direction.x > 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        state = State.RUN;

        if (Input.GetKey(KeyCode.K) && roolTimer <= 0.0f && isGrounded)
        {
            roolTimer = roolTime;
            GameManager.Instance.player.SetInvincibleTime(0.55f);
            Invoke(nameof(SetRollState), 0.55f);
            isRolling = true;

            state = State.ROLL;
        }

        transform.Translate(moveSpeed * moveSpeedValue * Time.deltaTime * direction);
    }

    private void CheckGrounded()
    {
        Vector3 from = new Vector2(transform.position.x, coll.bounds.min.y);

        // Ground Check == null -> Platform Check
        if (!CheckRay(Physics2D.Raycast(from, Vector3.down, checkGroundRange, LayerMask.GetMask("Ground"))))
            CheckRay(Physics2D.Raycast(from, Vector3.down, checkGroundRange, LayerMask.GetMask("Platform")));


        if (isGrounded == false)
        {
            if (rigid.velocity.y > 0)
            {
                state = State.RISE;
                rigid.gravityScale = 1.55f;
            }
            else
            {
                state = State.FALL;
                rigid.gravityScale = 2.65f;
            }
        }
    }

    private bool CheckRay(RaycastHit2D rayhit)
    {
        if (rayhit.collider == null)
        {
            isGrounded = false;
            return false;
        }
        else
        {
            isGrounded = true;
            return true;
        }
    }

    private void Jump()
    {
        if (GameManager.Instance.talkManager.isAction)
        {
            state = State.IDLE;
            return;
        }

        if (playerAttack.isAttacking) return;

        if (!isGrounded) return;
        if (Input.GetKey(KeyCode.S)) return;
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        rigid.velocity = new Vector2(0.0f, jumpPower * jumpPowerValue);
    }
}