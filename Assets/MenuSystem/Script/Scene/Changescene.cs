using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Changescene : MonoBehaviour
{
   
    public void change(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void exitapp()
    {
        Application.Quit();
    }

}
