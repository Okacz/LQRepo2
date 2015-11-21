using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using System.Collections;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class ShopInteract : MonoBehaviour
    {

        void OnMouseDown()
        {
            GameObject jack = GameObject.Find("Lumberjack1");
           
            if(jack.GetComponent<Animation>().IsPlaying("Lumbering"))
            {
                jack.GetComponent<Animation>().Play("Idle");
            }
            else
            {
                jack.GetComponent<Animation>().Play("Lumbering");
            }
        }
    }
}