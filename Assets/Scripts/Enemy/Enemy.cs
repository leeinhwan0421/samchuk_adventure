using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public enum State
    {
        IDLE,
        SEARCH,
        TRACE,
        ATTACK,
        DIE,
    }

    protected State state = State.IDLE;
    float nextMove = 0;

    [Header("Status")]
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected float traceRange;
    [SerializeField] protected float attackRange;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D coll;

    public bool isFly = false;
    public bool isFlyTrace = false;

    private void SetAnimationByState()
    {
        if (anim == null) return;

        anim.SetInteger("CurState", (int)state);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();

        if (anim == null || rb == null || sr == null) return;

        Think();
    }

    private void Update()
    {
        FSM();

        SetAnimationByState();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow * 0.25f;

        Gizmos.DrawSphere(transform.position, traceRange);

        Gizmos.color = Color.green * 0.25f;

        Gizmos.DrawSphere(transform.position, attackRange);
    }

    private void Think()
    {
        //-1:왼쪽이동 ,1:오른쪽 이동 ,0:멈춤
        nextMove = (float)Random.Range(-1, 2);

        float time = Random.Range(1.5f, 3f);

        // 재귀함수사용
        Invoke(nameof(Think), time);
    }

    private void FSM()
    {
        if (health <= 0) return;
        if (GameManager.Instance.player == null) return;

        // 박쥐 영역
        if (isFly)
        {
            if (Vector2.Distance(GameManager.Instance.player.transform.position, transform.position) < traceRange) isFlyTrace = true;

            if (isFlyTrace == true)
            {
                float x = (GameManager.Instance.player.transform.position - transform.position).x;
                SpriteFlip(x);

                Movement(speed * Time.deltaTime * (GameManager.Instance.player.transform.position - transform.position).normalized);

                state = State.TRACE;
            }
        }
        // 뚜벅이 영역 // 하드코딩 FSM 
        else
        {
            if (Vector2.Distance(GameManager.Instance.player.transform.position, transform.position) < attackRange)
            {
                state = State.ATTACK;
            }
            else if (Vector2.Distance(GameManager.Instance.player.transform.position, transform.position) < traceRange)
            {
                if (state == State.ATTACK) return;

                state = State.TRACE;

                float x = (GameManager.Instance.player.transform.position - transform.position).x;
                SpriteFlip(x);

                Movement(speed * Time.deltaTime * new Vector2(x, 0.0f).normalized);
            }
            else
            {
                if (nextMove == 0)
                {
                    state = State.IDLE;
                }
                else
                {
                    state = State.SEARCH;

                    Movement(speed * Time.deltaTime * new Vector2(nextMove, 0.0f).normalized);
                    SpriteFlip(nextMove);
                }
            }
        }
    }

    private void Movement(Vector3 direction)
    {
        if (state == State.ATTACK) return;

        // 박쥐 영역
        if (isFly == true)
        {
            transform.Translate(speed * Time.deltaTime * direction.normalized);
            return;
        }

        // 일반 영역
        Vector2 frontVec = new Vector2(rb.position.x + nextMove * 0.4f, coll.bounds.min.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); //초록색

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 0.1f, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null)
        {
            rayHit = Physics2D.Raycast(frontVec, Vector3.down, 0.1f, LayerMask.GetMask("Ground"));
            if (rayHit.collider == null)
            {
                return;
            }
        }

        transform.Translate(speed * Time.deltaTime * direction.normalized);
    }

    private void SpriteFlip(float x)
    {
        if (x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private IEnumerator HitEffect()
    {
        sr.color = Color.red;

        yield return new WaitForSeconds(0.08f);

        sr.color = Color.white;
    }

    private void ChangeStateToInt(int state)
    {
        this.state = (State)state;

        float x = (GameManager.Instance.player.transform.position - transform.position).x;
        SpriteFlip(x);
    }

    public void GetDamage(float damage)
    {
        if (health == 0) return;

        SoundInstance.Instance.InstantiateEnemySFX(Random.Range(0, 2));

        health -= damage;
        StartCoroutine(HitEffect());

        if (health <= 0)
        {
            if (anim == null) return;

            if (transform.Find("AttackRange") != null) transform.Find("AttackRange").gameObject.SetActive(false);

            state = State.DIE;
            anim.SetTrigger("Die");
        }
    }

    private void Die()
    {
        health = 0;
        Destroy(gameObject, 0.15f);

        GameManager.Instance.DropCoin(transform, Random.Range(0, 3));
    }
}
