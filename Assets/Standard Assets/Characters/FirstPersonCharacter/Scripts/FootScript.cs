using UnityEngine;
using System.Collections;

public class FootScript : MonoBehaviour {


    public GoblinBossController bosscontroller;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "NPC_Tools_Axe_004")
        {
            print("dostał!");

        }
    }
}
