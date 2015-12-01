using UnityEngine;
using System.Collections;

    public class GoblinHit : MonoBehaviour
    {

        public SimpleMovement controller;
        // Use this for initialization
        void Start()
        {
            GameObject gameControllerObject = GameObject.FindWithTag("MainLumberjack");
            if (gameControllerObject != null)
            {
                controller = gameControllerObject.GetComponent<SimpleMovement>();

            }
        }

        // Update is called once per frame
        void OnTriggerEnter(Collider other)
        {

            /*if (transform.parent.parent.gameObject.GetComponent<Animation>().IsPlaying("attack1"))
            {*/
            if (GetComponentInParent<Animation>().IsPlaying("attack1"))
            {
                if (other.tag == "GameController")
                {
                    controller.Damage(10);
                }
            }
        }
    }
