using UnityEngine;
using System.Collections;

public class PickableObj : InteractObj {
	public GameObject geteffect;
	public Texture invtex;
	public string objname;
	public string pickgoal;
	
	
	void Start(){
		if (pickgoal == null)
			pickgoal = "You got: " + objname;
		
		if (objname == null) 
			objname = this.name;
		
		if (geteffect == null)
			geteffect = GameObject.Find("standardgeteffect");
		
	}
	
	// по запросу убиваем кхуям (совсем)
	void DestroyItem(){
		Destroy(this.gameObject, 1);
	}
}
