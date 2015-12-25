using UnityEngine;
using System.Collections;



/// <summary>
/// This script simply allows forwarding collision events for the objects that collide with something. This
/// allows you to have a generic collision handler and attach a collision forwarder to your child objects.
/// In addition, you also get access to the game object that is colliding, along with the object being
/// collided into, which is helpful.
/// </summary>
public class ParticleCollision : MonoBehaviour
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

    void OnParticleCollision(GameObject other)
    {
        if (other.name == "MainLumberjack")
        {
            controller.Damage(10);
            
        }

    }

}

