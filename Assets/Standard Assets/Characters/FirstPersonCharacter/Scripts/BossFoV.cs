using UnityEngine;
using System.Collections;

public class BossFoV : MonoBehaviour {

    public GoblinBossController bosscontroller;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="GameController")
        {
            bosscontroller.doIRotate = false;
            print("hit");
            
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "GameController")
        {
            bosscontroller.doIRotate = true;
            print("escape");

        }
    }

    
}
