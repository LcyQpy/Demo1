using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public Button level_1_btn;
    public Button level_2_btn;
    public Button level_3_btn;
    public Button close_btn;

    // Start is called before the first frame update
    void Start()
    {
        level_1_btn.onClick.AddListener(LoadScene1);
        level_2_btn.onClick.AddListener(LoadScene2);
        level_3_btn.onClick.AddListener(LoadScene3);
        close_btn.onClick.AddListener(QuitAPP);
    }

    public void LoadScene1(){
        SceneManager.LoadScene(1);
    }
    public void LoadScene2(){
        SceneManager.LoadScene(2);
    }
    public void LoadScene3(){
        SceneManager.LoadScene(3);
    }
    public void BackScene()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitAPP() {
        Application.Quit();
    }
}
