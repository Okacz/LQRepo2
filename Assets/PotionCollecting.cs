using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using System.Collections;

    public class PotionCollecting : MonoBehaviour
    {

        public SimpleMovement controller;
        float max = 0.5f;
        float min = 0;
        bool updown = true;
        void Start()
        {
            GameObject gameControllerObject = GameObject.FindWithTag("GameController");
            if (gameControllerObject != null)
            {
                controller = gameControllerObject.GetComponent<SimpleMovement>();

            }
            min = transform.position.y;
        }
        void Update()
        {
            if (updown == true)
            {
                transform.Translate(0, 0, 0.02f);
            }
                
            else
            {
                transform.Translate(0, 0, -0.02f);
            }
            if (transform.position.y>min+max)
            {
                updown = false;
            }
            if(transform.position.y<min)
            {
                updown = true;
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