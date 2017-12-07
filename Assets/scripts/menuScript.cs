using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

    public string Start_Game_Level;
    public GameObject aboutGroup;
    public GameObject menuGroup;

    private void Start()
    {
        aboutGroup.SetActive(false);
        menuGroup.SetActive(true);
    }

    public void Start_Game()
    {
        SceneManager.LoadScene(Start_Game_Level);
    }

    public void About()
    {
        menuGroup.SetActive(false);
        aboutGroup.SetActive(true);
    }

    public void Visit_Website(string URL)
    {
        Application.OpenURL(URL);
    }

    public void Take_Photo()
    {
        SceneManager.LoadScene("capture");
    }

    public void Menu()
    {
        menuGroup.SetActive(true);
        aboutGroup.SetActive(false);
        //animate the panel
        //make About content invisible
    }

}
