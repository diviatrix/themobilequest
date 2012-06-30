using UnityEngine;
using System.Collections;

public class EnableTrigger : MonoBehaviour 
{
	public GameObject[] whatToActivate;
	public bool dieOnComplete = true;
	public GameObject needToActivate;

	GameObject whereisLogic;
		
	void Start()
	{
		whereisLogic = GameObject.Find("MainLogicObject");
	}
	public void ActivateTrigger()
	{
//		if (whereisLogic.GetComponent<MainLogic>().inventorylist.Find(needToActivate))
		//{
			if (whatToActivate != null)
			{
				foreach (GameObject activateObj in whatToActivate)
				{
            		activateObj.active = true;
				}
			}
			if (dieOnComplete)
			{
				Destroy(this.gameObject); 	
			}
		//}
	}
}
