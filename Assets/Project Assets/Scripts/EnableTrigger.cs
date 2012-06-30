using UnityEngine;
using System.Collections;

public class EnableTrigger : MonoBehaviour 
{
	public bool active;
	// Use this for initialization
	void Start () 
	{
		if (active)
		{
			this.collider.enabled = true;
		}
	}
	
	void ActivateTrigger()
	{
		
	}

}
