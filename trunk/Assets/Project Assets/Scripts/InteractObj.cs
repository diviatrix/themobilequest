using UnityEngine;
using System.Collections;

public class InteractObj : MonoBehaviour {

	protected virtual void OnClick()
	{		
	}
	
	// Update is called once per frame
	
	void Update() 
	{
		//if (Input.GetAxis("Vertical")== 0 && Input.GetAxis("Horisontal") == 0){
		//Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && (hit.transform.gameObject == gameObject) && (hit.distance <= 2)) {
			OnClick();
			}
		//}
	}
}
