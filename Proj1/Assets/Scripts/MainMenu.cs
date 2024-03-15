using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(QUIT_GAME)) {
            Application.Quit();
        }
    }

    public void LoadGame() {
        SceneManager.LoadScene(0);
    }
}
