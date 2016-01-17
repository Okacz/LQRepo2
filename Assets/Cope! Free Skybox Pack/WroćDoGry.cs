using UnityEngine;
using System.Collections;

public class WroćDoGry : MonoBehaviour
{


    public GameObject menu;
    public SimpleMovement controller;
   public void Wróć()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }
    public void Wyjdź()
   {
       print("QUIT");
       Application.Quit();
        
   }
    public void kontynuuj()
    {
        controller.heal(controller.maxHealth);
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            controller = gameControllerObject.GetComponent<SimpleMovement>();

        }
        controller.respawn();
        menu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        
        controller.isMenuUp = false;
    }
}
