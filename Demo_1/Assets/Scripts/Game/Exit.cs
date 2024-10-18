using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    private Button exit;
    void Start()
    {
        exit = GameObject.Find("exit").GetComponent<Button>();
        exit.onClick.AddListener(QuitScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void QuitScene(){

        SceneManager.UnloadSceneAsync(1);
    }
}
