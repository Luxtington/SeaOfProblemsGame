using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterGame : MonoBehaviour
{
    [SerializeField] AudioSource button_sound;
    public void restart_lvl()
    {
        //button_sound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Time.timeScale = 1;
    }

    public void restart_sound()
    {
        button_sound.Play();
        Invoke("restart_lvl", 0.1f);
        Time.timeScale = 1;
    }
    /*public void to_menu()
    {
        button_sound.Play();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }*/
    public void to_menu()
    {
        SceneManager.LoadScene(0);
    }

    public void go_to_menu()
    {
        button_sound.Play();
        Invoke("to_menu", 0.1f);
        Time.timeScale = 1;
    }
}
