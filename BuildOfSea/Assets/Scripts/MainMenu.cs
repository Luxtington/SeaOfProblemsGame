using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject rule_list;
    [SerializeField] GameObject aim_list;
    [SerializeField] AudioSource button_sound;

    private void Awake()
    {
        rule_list.SetActive(false);
        aim_list.SetActive(false);
    }

    public void to_aim()
    {
        button_sound.Play();
        aim_list.SetActive(true);
    }
    public void to_rules()
    {
        aim_list.SetActive(false);
        button_sound.Play();
        rule_list.SetActive(true);
    }

    public void to_menu()
    {
        button_sound.Play();
        rule_list.SetActive(false);
    }

    public void open_level_list()
    {
        //button_sound.Play();
        SceneManager.LoadScene(1);
    }
    public void lvl_sound()
    {
        button_sound.Play();
        Invoke("open_level_list", 0.3f);
    }

    public void leave_game()
    {
        Application.Quit();
    }

    public void exit()
    {
        button_sound.Play();
        Invoke("leave_game", 0.5f);
    }
}
