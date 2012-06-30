using UnityEngine;
using System.Collections;

public class EnableTrigger : MonoBehaviour 
{
	public GameObject[] toActivate;
	public bool activated;
	// Use this for initialization
	void Start () 
	{
		if (activated)
		{
			collider.enabled = true;
			renderer.enabled = true;
		}
	}
	
	public void ActivateTrigger()
	{
		if (toActivate != null)
		{
			foreach (GameObject child in toActivate) 
            	child.active = true;
		}
	}
}
