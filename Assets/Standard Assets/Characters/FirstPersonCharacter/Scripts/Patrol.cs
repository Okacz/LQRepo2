using UnityEngine;
using System.Collections;


public class Patrol : MonoBehaviour {

	//public Transform[] points;
	private int destPoint = 0;
	private NavMeshAgent agent;
	private Vector3 startPosition;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		startPosition = this.transform.position;
		// Disabling auto-braking allows for continuous movement
		// between points (ie, the agent doesn't slow down as it
		// approaches a destination point).
		agent.autoBraking = false;
		GotoNextPoint();
	}


	void GotoNextPoint() {

		Vector3 destination = startPosition + new Vector3(Random.Range (-10, 10), 
				0, Random.Range (-10, 10));

		// Set the agent to go to the currently selected destination.
		agent.destination = destination;
		GetComponent<Animation>().Play("run");

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		//destPoint = (destPoint + 1) % points.Length;
	}


	void Update () {
		// Choose the next destination point when the agent gets
		// close to the current one.
		//trzeba dodać if, czy goblin żyje
		/*if ()
		{*/
			if (agent.remainingDistance < 0.5f)
			GotoNextPoint();
		//}
	}
}