﻿using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	public SimpleMovement controller;
	public void OnTriggerEnter(Collider col)
	{
		//print("koliduje sie");
		if (col.tag == "GameController")
		{
			//print ("checkpointuje");
			GameObject playa = GameObject.Find ("MainLumberjack");
			SimpleMovement skrypt = playa.GetComponent<SimpleMovement> ();
			GameObject spawn = GameObject.Find ("Spawnpoint1");
			skrypt.Spawnpoint = spawn.transform.position;
			//print ("zacheckpointowalo");
			GameObject shrine = GameObject.Find ("sculpture_02");

            shrine.transform.GetComponent("Halo").GetType().GetProperty("enabled").SetValue(shrine.transform.GetComponent("Halo"), true, null); 
		}
	}

}
