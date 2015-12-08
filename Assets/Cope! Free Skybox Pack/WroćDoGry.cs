using UnityEngine;
using System.Collections;

public class WroćDoGry : MonoBehaviour
{


    public GameObject menu;
   public void Wróć()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Wyjdź()
   {
       print("QUIT");
       Application.Quit();
        
   }
}
