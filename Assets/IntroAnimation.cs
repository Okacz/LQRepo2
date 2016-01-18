using UnityEngine;
using System.Collections;

public class IntroAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Animation>().wrapMode = WrapMode.Loop;

        GetComponent<Animation>().Play("Idle");
        Time.timeScale = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Animation>().wrapMode = WrapMode.Loop;

        GetComponent<Animation>().Play("Idle");
	}
}
