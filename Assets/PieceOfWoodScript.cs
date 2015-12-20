using UnityEngine;
using System.Collections;

public class PieceOfWoodScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.GetComponent<Rigidbody>().AddTorque(300, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
