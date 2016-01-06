using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	public SimpleMovement controller;
	public void OnTriggerEnter(Collider col)
	{
		print("koliduje sie");
		if (col.tag == "GameController")
		{
			print ("checkpointuje");
			GameObject playa = GameObject.Find ("MainLumberjack");
			SimpleMovement skrypt = playa.GetComponent<SimpleMovement> ();
			skrypt.Spawnpoint = transform.position;
			print ("zacheckpointowalo");
		}
	}

}
