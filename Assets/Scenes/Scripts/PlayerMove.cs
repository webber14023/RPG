using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    static PlayerMove intance;
    public float speed;             //移動速度
    public bool canControl = true;
    public GameObject attackPoint;
    public Vector2 input;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer Sprite;
    private float inputX, inputY;
    private Vector2 mouseDerection;


    void Awake() {
        if(intance != null)
            Destroy(this);
        intance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        mouseDerection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        
        if(canControl) {
            Move();
        }
    }

    void Move() {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        input = new Vector2(inputX, inputY).normalized;
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

    public static Vector2 GetMouseDerection()
    {
        Vector2 mouseDerection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - intance.transform.Find("AttackPoint").transform.position).normalized;
        return mouseDerection;
    }
}
