using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : Essence
{
    private float speed = 1.5f;

    private Vector3 dir;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        lifes = 2;
        dir = transform.right;
    }

    private void Update()
    {
        move_it();
    }

    private void move_it()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 1.1f, 0.1f);
        if (colliders.Length > 0) dir *= -1f;
        transform.Translate(dir * speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime * speed);
        sprite.flipX = dir.x > 0.0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject) Player.Instance.get_damage();
    }
}
