using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject) Player.Instance.get_damage();
    }
    /*[SerializeField] private int lifes = 3;   ������ ����������� �� Essence 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.get_damage();
            lifes--;
            Debug.Log("���������� ����� ����: " + lifes);
        }

        if (lifes < 1) die();
    }*/
}
