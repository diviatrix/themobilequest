using UnityEngine;
using System.Collections;

public class Curlock : MonoBehaviour {
	
	public bool curlock;

	void Start () {
	curlock = true;
	}
	
	// Update is called once per frame
	void Update () {
	if (curlock)
			Screen.lockCursor = true;
	if (!curlock)
			Screen.lockCursor = false;
	}	
}
