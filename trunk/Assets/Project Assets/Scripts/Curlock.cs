using UnityEngine;
using System.Collections;

public class Curlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKeyDown(KeyCode.Escape))
		Screen.lockCursor = false;
	}
}
