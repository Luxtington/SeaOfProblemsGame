using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] AudioSource button_sound;

    [SerializeField] private GameObject pause_panel;
    [SerializeField] private GameObject rules_list;
    [SerializeField] private GameObject aim_list;

    private void Awake()
    {
        pause_panel.SetActive(false);
        rules_list.SetActive(false);
        aim_list.SetActive(false);
    }

    public void pause_on()
    {
        button_sound.Play();
        pause_panel.SetActive(true);
        Time.timeScale = 0;
    }

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
    
    public void show_aim()
    {
        button_sound.Play();
        aim_list.SetActive(true);
    }
    public void show_rules()
    {
        aim_list.SetActive(false);
        button_sound.Play();
        rules_list.SetActive(true);
    }

    public void close_rules()
    {
        button_sound.Play();
        rules_list.SetActive(false);
    }


    public void pause_off()
    {
        button_sound.Play();
        pause_panel.SetActive(false);
        Time.timeScale = 1;
    }
}
