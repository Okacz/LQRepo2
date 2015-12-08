using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using System.Collections;

    public class PotionCollecting : MonoBehaviour
    {

        public SimpleMovement controller;


        void Start()
        {
            GameObject gameControllerObject = GameObject.FindWithTag("GameController");
            if (gameControllerObject != null)
            {
                controller = gameControllerObject.GetComponent<SimpleMovement>();

            }
        }
        void OnTriggerEnter(Collider other)
        {
            print("ojej");
            if (other.tag == "GameController")
            {
                controller.getPotion(1);
                Destroy(this);
                Destroy(this.gameObject);
            }
        }
    
}