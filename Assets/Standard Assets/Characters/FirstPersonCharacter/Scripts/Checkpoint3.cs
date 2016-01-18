using UnityEngine;
using System.Collections;

public class Checkpoint3 : MonoBehaviour {
	public SimpleMovement controller;
	public void OnTriggerEnter(Collider col)
	{
		//print("koliduje sie");
		if (col.tag == "GameController")
		{
			//print ("checkpointuje");
			GameObject playa = GameObject.Find ("MainLumberjack");
			SimpleMovement skrypt = playa.GetComponent<SimpleMovement> ();
			GameObject spawn = GameObject.Find ("Spawnpoint3");
			skrypt.Spawnpoint = spawn.transform.position;
			//print ("zacheckpointowalo");
			GameObject shrine = GameObject.Find ("skulptur (1)");

			shrine.transform.GetComponent("Halo").GetType().GetProperty("enabled").SetValue(shrine.transform.GetComponent("Halo"), true, null); 
		}
	}

}
