using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

   

    public void LoadSceneKurwo()
    {
        Time.timeScale = 1;
        Application.LoadLevel("GoblinLevel1");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
