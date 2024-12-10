using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : Essence
{
    [SerializeField] private AudioSource jump_sound;
    [SerializeField] private AudioSource damage_sound;
    [SerializeField] private AudioSource bad_hit_sound;
    [SerializeField] private AudioSource success_hit_sound;

    [SerializeField] private float speed = 3f;
    [SerializeField] private int health;
    [SerializeField] private float jump_force = 15f;

    private bool isGround = false;

    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;

    [SerializeField] private GameObject loser_panel;
    [SerializeField] private GameObject winner_panel;

    public bool isHitting = false;
    public bool isRecharged = true;

    public Transform hitPos;
    public float hitRange;
    public LayerMask enemy;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    public static Player Instance { get; set; }  // теперь можем обращаться ко всех паблик полям без создания объектов

    private void Awake()
    {
        lifes = 5;
        health = lifes;
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isRecharged = true;

        loser_panel.SetActive(false);
        winner_panel.SetActive(false);
    }

    private States state
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }

    }

    private void Update()
    {
        if (isGround && !isHitting && health > 0) state = States.idle;

        if (Input.GetButton("Horizontal") && !isHitting && health > 0) run();

        check_ground();

        if (Input.GetButton("Jump") && isGround && !isHitting && health > 0) jump();

        if (Input.GetButtonDown("Fire1") && health > 0) hit();

        if (health > lifes) health = lifes;
        
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i<health) hearts[i].sprite = aliveHeart;
            else hearts[i].sprite = deadHeart;

            if (i<lifes) hearts[i].enabled = true;
            else hearts[i].enabled = false;
        }
    }
    private void run()
    {
        if (isGround) state = States.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void jump()
    {
        rb.AddForce(transform.up * jump_force, ForceMode2D.Impulse);
        jump_sound.Play();
    }

    private void check_ground()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGround = collider.Length > 1;

        if (!isGround && health > 0) state = States.jump;
    }


    public override void get_damage()
    {
        health -= 1;
        damage_sound.Play();
        if (health == 0)
        {
            foreach (var item in hearts)
            {
                item.sprite = deadHeart;
            }
            die();
        }
    }

    public override void die()
    {
        state = States.death;
        Invoke("set_loser_panel", 1.1f);
    }

    private void set_loser_panel()
    {
        loser_panel.SetActive(true);
        Time.timeScale = 0;
    }
    public void set_winner_panel()
    {
        winner_panel.SetActive(true);
        //Time.timeScale = 0;
    }


    public void hit()
    {
        if (isGround && isRecharged)
        {
            state = States.hit;
            isHitting = true;
            isRecharged = false;

            StartCoroutine(hit_animation());
            StartCoroutine(hit_cool_down());
        }
    }
    private void on_hit()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPos.position, hitRange, enemy);

        if (colliders.Length == 0) bad_hit_sound.Play();
        else success_hit_sound.Play();

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Essence>().get_damage();
            StartCoroutine(enemy_on_hit(colliders[i]));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPos.position, hitRange);
    }

    private IEnumerator hit_animation()
    {
        yield return new WaitForSeconds(0.4f);
        isHitting = false;
    }

    private IEnumerator hit_cool_down()
    {
        yield return new WaitForSeconds(0.5f);
        isRecharged = true;
    }

    private IEnumerator enemy_on_hit(Collider2D enemy)
    {
        SpriteRenderer enemy_color = enemy.GetComponentInChildren<SpriteRenderer>();
        enemy_color.color = new Color(0.8039f, 0.3764f, 0.2823f);
        yield return new WaitForSeconds(0.2f);
        if (enemy_color) enemy_color.color = new Color(1, 1, 1);
    }
}
public enum States
{
    idle,
    run,
    jump,
    hit,
    death
}
