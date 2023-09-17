using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;             //移動速度
    public float attackCooldown;    //攻擊冷卻
    public float attackTime;        //攻擊時間
    public float attackRange;       //攻擊距離
    private bool canAttack = true;
    public bool canControl = true;
    public GameObject AttackEffectPrefab;   //攻擊產生的特效
    public GameObject myBag;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer Sprite;
    private float inputX, inputY;
    private Vector2 mouseDerection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        mouseDerection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        OpenMyBag();
        if(canControl) {
            Move();
            //Attack();
        }
    }

    void Move() {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(inputX, inputY).normalized;
        rb.velocity = input * speed;
        if(input != Vector2.zero) {
            animator.SetBool("isMoving", true);
        }
        else {
            animator.SetBool("isMoving", false);
        }
        if(mouseDerection.x > 0) {
            Sprite.flipX = false;
        }
        else {
            Sprite.flipX = true;
        }
    }

    void OpenMyBag()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            myBag.SetActive(!myBag.activeSelf);
        }
    }

    /*void Attack() {
        if(Input.GetMouseButton(0) && canAttack) {
            animator.SetTrigger("Attacking");
            canControl = false;
            GameObject attackEffect = Instantiate(AttackEffectPrefab, (Vector2)transform.position + mouseDerection.normalized * attackRange, Quaternion.identity, transform);
            rb.velocity = Vector2.zero;//mouseDerection.normalized * attackRange * 2;
            attackEffect.gameObject.GetComponent<Animator>().SetTrigger("isAttacking");
            attackEffect.gameObject.GetComponent<CircleCollider2D>().enabled = true;
            Destroy(attackEffect.gameObject, 1.2f);
            StartCoroutine("AttackCD");
            StartCoroutine("endAttack", attackEffect);
        }
    }
    IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    IEnumerator endAttack(GameObject attackEffect)
    {
        yield return new WaitForSeconds(attackTime);
        attackEffect.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        canControl = true;
    }*/
}
