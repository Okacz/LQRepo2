using UnityEngine;
using System.Collections;


    public interface ICollisionHandler
    {
        void HandleCollision(GameObject obj, Collision c);
    }

    /// <summary>
    /// This script simply allows forwarding collision events for the objects that collide with something. This
    /// allows you to have a generic collision handler and attach a collision forwarder to your child objects.
    /// In addition, you also get access to the game object that is colliding, along with the object being
    /// collided into, which is helpful.
    /// </summary>
    public class FireCollisionForwardScript : MonoBehaviour
    {
        public ICollisionHandler CollisionHandler;

        SimpleMovement controller;
        public void Start()
        {
            GameObject gameControllerObject = GameObject.FindWithTag("GameController");
            if (gameControllerObject != null)
            {
                controller = gameControllerObject.GetComponent<SimpleMovement>();

            }
        }
        public void OnCollisionEnter(Collision col)
        {
            if (col.collider.name == "MainLumberjack")
            {
                print("kolizja z " + col.collider.name);
                controller.NoDamage(10);

            }
            CollisionHandler.HandleCollision(gameObject, col);
            
        }
        
    }

