using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonEvent : MonoBehaviour
{
    public void Replay()
    {
        SceneManager.LoadScene("Level");
    }
    public void QuitApp()
    {
        Application.Quit();
    }
}
