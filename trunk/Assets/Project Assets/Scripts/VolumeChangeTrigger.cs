using UnityEngine;
using System.Collections;

public class VolumeChangeTrigger : MonoBehaviour {
	
	public bool loudSound;
	public float vol1 = 0.0f;
	public float vol2 = 1;
	// Use this for initialization
	void Start () 
	{
		if (loudSound)
			audio.volume = vol2;

		else if(!loudSound)
			audio.volume = vol1;
	}
	
	public void VolChange()
	{
		if (loudSound)
		{
			audio.volume = vol1;
			loudSound = false;
		}
		else if (!loudSound)
		{
			audio.volume = vol2;
			loudSound = true;
		}
	}
}
