using UnityEngine;
using System.Collections;

public class WroćDoGry : MonoBehaviour
{

    public GoblinBossController gbc;
    public GameObject menu;
    public CameraMovement3 camerascript;
    public SimpleMovement controller;
   public void Wróć()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        
    }
    public void WróćDoMenu()
   {
       Application.LoadLevel("Main Menu");
   }
    public void Wyjdź()
   {
       print("QUIT");
       Application.Quit();
        
   }
    public void kontynuuj()
    {
        gbc.healToMax();
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
        controller.potiony = 0;
        controller.isMenuUp = false;
        camerascript.playLevelMusic();
    }
}
