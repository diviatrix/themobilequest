using UnityEngine;
using System.Collections;

public class Curlock : MonoBehaviour {
	
	public bool curlock;

	void Start () {
	curlock = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && curlock)
			curlock = false;
		else if (Input.GetKeyDown(KeyCode.Escape) && !curlock)
			curlock = true;
		
		
		if (curlock)
			Screen.lockCursor = true;
		else if (!curlock)
			Screen.lockCursor = false;
	}	
}
