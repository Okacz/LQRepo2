using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public void OnTriggerEnter(Collider col)
	{
		print("koliduje sie");
		if (col.name == "NPC_Tools_Axe_004")
		{
			print ("checkpointuje");
			GameObject playa = GameObject.Find ("MainLumberjack");
			SimpleMovement skrypt = playa.GetComponent<SimpleMovement> ();
			skrypt.Spawnpoint = transform.position;
		}
	}
}
