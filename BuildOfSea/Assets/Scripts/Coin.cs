using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioSource coin_sound; 

    private void no_coin()
    {
        Destroy(this.gameObject);
    }
    private void stop_time()
    {
        Time.timeScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            coin_sound.Play();
            Player.Instance.set_winner_panel();
            Invoke("no_coin", 0.5f);
            Invoke("stop_time", 0.5f);
        }
    }
}
                                    