using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private AudioSource button_sound;

    public Button[] levels;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levels.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levels[i].interactable = false;
            }
        }
    }
    public void select(int numberInBuild)
    {
        SceneManager.LoadScene(numberInBuild);
        Destroy(GameObject.Find("Audio Source")); //во время нахождения в меню музыка из уровня молчит
    }

    public void to_menu()
    {
        SceneManager.LoadScene(0);
    }

    public void go_to_menu()
    {
        button_sound.Play();
        Invoke("to_menu", 0.3f);
    }
}
