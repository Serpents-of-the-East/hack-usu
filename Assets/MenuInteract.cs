using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuInteract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnQuit(InputValue value)
    {
        Application.Quit();
    }

    private void OnStart(InputValue value)
    {
        SceneManager.LoadScene(2);
    }


}
