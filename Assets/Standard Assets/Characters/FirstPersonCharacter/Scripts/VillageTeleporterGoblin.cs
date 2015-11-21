using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using System.Collections;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class VillageTeleporterGoblin : MonoBehaviour
    {

        public SimpleMovement controller;


        void Start()
        {
            GameObject gameControllerObject = GameObject.FindWithTag("MainLumberjack");
            if (gameControllerObject != null)
            {
                controller = gameControllerObject.GetComponent<SimpleMovement>();

            }
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "GameController")
            {
                Application.LoadLevel("GoblinLevel1");
            }
        }
    }
}