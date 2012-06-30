using UnityEngine;
using System.Collections;

public class EnableTrigger : MonoBehaviour 
{
	public GameObject[] whatToActivate;
	public bool dieOnComplete = true;
	public GameObject needItemToActivate;
	public string needActionToActivate;

	GameObject whereisLogic;
		
	void Start()
	{
		whereisLogic = GameObject.Find("MainLogicObject");
	}
	public void ActivateTrigger()
	{
		if (whereisLogic.GetComponent<MainLogic>().inventorylist.Find(GameObject => needItemToActivate) != null)
		{
			if (whatToActivate != null)
			{
				foreach (GameObject activateObj in whatToActivate)
				{
            		activateObj.active = true;
					whereisLogic.GetComponent<MainLogic>().inventorylist.Remove(needItemToActivate);
				}
			}
			if (dieOnComplete)
			{
				Destroy(this.gameObject); 	
			}
		}
	}
}
