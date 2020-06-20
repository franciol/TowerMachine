using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class menuScript : MonoBehaviour
{
    public Button btnStart,btnQuit;
    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(LoadScene);
        btnQuit.onClick.AddListener(quitt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadScene()
    {
        SceneManager.LoadScene("DummyScene");
    }
    void quitt()
    {
        Application.Quit();
    }
}
