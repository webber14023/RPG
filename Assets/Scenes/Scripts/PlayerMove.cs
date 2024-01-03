using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    static PlayerMove intance;
    public bool canControl = true;
    public GameObject attackPoint;
    public Vector2 input;
    public float comboTimer;
    public int Combo;
    public GameObject DeathInterface;
    public GameObject HurtEffect;
    public Text moneyText;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer Sprite;
    private float inputX, inputY;
    private AbilityManager ability;
    CharacterStats stats;

    void Awake() {
        if(intance != null)
            Destroy(this);
        intance = this;

        Application.targetFrameRate = 60;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
        stats = GetComponent<CharacterStats>();
        ability = GetComponent<AbilityManager>();
    }

    void Update()
    {
        if(comboTimer > 0) {
            comboTimer -= Time.deltaTime;
            if(comboTimer <= 0) {
                Combo = 0;
            }
        }

        if(!ability.isCasting)
            Move();
        else if (!canControl)
            rb.velocity = Vector2.zero;
    }

    void Move() {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        input = new Vector2(inputX, inputY).normalized;
        rb.velocity = input * stats.speed;
        if(input != Vector2.zero) {
            animator.SetBool("isMoving", true);
        }
        else {
            animator.SetBool("isMoving", false);
        }

        if(inputX > 0) {
            Sprite.flipX = false;
        }
        else if(inputX < 0) {
            Sprite.flipX = true;
        }
    }
    public void ClearAllAttackEffect() {
        while(transform.childCount > 3)
            DestroyImmediate(transform.GetChild(3).gameObject);
    }

    public void PlayerHurt() {
        HurtEffect.GetComponent<Fade>().FadeEffect();
        Camera.main.GetComponent<CameraMove>().ShakeScreen();
    }

    public void Dead() {
        canControl = true;
        rb.velocity = Vector2.zero;
        ability.isCasting = true;
        GetComponent<BoxCollider2D>().enabled = false;
        DeathInterface.SetActive(true);
    }

    public void Respawn() {
        transform.GetComponent<CharacterStats>().ResetStats();
        GameObject.FindGameObjectWithTag("RoomGenerator").GetComponent<RoomGenerator>().SaveDungeonData();
        SceneManager.LoadScene("Vallage");
    }

    public static Vector2 GetMouseDerection()
    {
        Vector2 mouseDerection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - intance.transform.Find("AttackPoint").transform.position).normalized;
        return mouseDerection;
    }
    public static void UpdatePlayerUI() {
        intance.moneyText.text = "Money : " + intance.stats.money.ToString();
    }
}
