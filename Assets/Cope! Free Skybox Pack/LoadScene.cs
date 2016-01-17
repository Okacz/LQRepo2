using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

   

    public void LoadSceneKurwo()
    {
        Application.LoadLevel("GoblinLevel1");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
