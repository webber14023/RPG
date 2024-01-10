using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

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
    public Text moneyTextInBag;
    public CharacterStats stats;
    public AudioClip[] walkSound;
    public AudioSource audioSource;
    public GameObject UpgradeEffect;
    public AudioClip UpgradeSound;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer Sprite;
    private float inputX, inputY;
    private AbilityManager ability;
    public bool InteractStats, autoPickItem;

    void Awake() {
        if(intance != null)
            Destroy(this);
        intance = this;

        Application.targetFrameRate = 60;
    }
    
    void Start()
    {
        stats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
        ability = GetComponent<AbilityManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(comboTimer > 0) {
            comboTimer -= Time.deltaTime;
            if(comboTimer <= 0) {
                Combo = 0;
            }
        }

        if(!ability.isCasting) {
            Move();
        }
        else if (!canControl) {
            animator.SetBool("isMoving", false);
            rb.velocity = Vector2.zero;
        }
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
        while(transform.childCount > 4)
            DestroyImmediate(transform.GetChild(4).gameObject);
    }

    public void PlayerHurt() {
        HurtEffect.GetComponent<Fade>().FadeEffect();
        Camera.main.GetComponent<CameraMove>().ShakeScreen();
    }

    public void Dead() {
        canControl = false;
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

    public void PlayWalkSound() {
        audioSource.PlayOneShot(walkSound[Random.Range(0, walkSound.Length)]);
    }

    public static Vector2 GetMouseDerection()
    {
        Vector2 mouseDerection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - intance.transform.Find("AttackPoint").transform.position).normalized;
        return mouseDerection;
    }

    public static bool PlayerInteractStats() {
        return intance.InteractStats;
    }

    public static void PlayerInteractSet(bool stats) {
        intance.InteractStats = stats;
    }

    public static bool PlayerAutoPickItem() {
        return intance.autoPickItem;
    }

    public static void UpdatePlayerUI() {
        Debug.Log(intance.stats.money);
        intance.moneyText.text = "金錢 : " + intance.stats.money.ToString();
        intance.moneyTextInBag.text = "金錢 : " + intance.stats.money.ToString();
    }
    public static void PlayerUpgrade() {
        intance.audioSource.PlayOneShot(intance.UpgradeSound);
        Instantiate(intance.UpgradeEffect, intance.transform.position, Quaternion.identity, intance.transform);
    }
    public static void ResetStatsAndUI() {
        EquipmentManager.UpdateEquipmentStats();
        intance.stats.UpdateStats();
        intance.stats.currentHealth = intance.stats.maxHealth;
        intance.stats.UpdateUI();
        UpdatePlayerUI();
    }

    public static CharacterStats GetPlayerStats() {
        return intance.stats;
    }

}
