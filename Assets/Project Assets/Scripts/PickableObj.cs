using UnityEngine;
using System.Collections;

public class PickableObj : InteractObj {
	public GameObject parentObj;
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
	
	// по запросу убиваем кхуям (совсем, рус.)
	public void GetItem(){
		Component[] childrenrenderers;
		if (parentObj == null) {
			this.gameObject.renderer.enabled = false;	
		}
		else {
			foreach (Renderer renderer in childrenrenderers) {
            	renderer = GetComponentsInChildren<parentObj>();
        	}			
		}		
	}
} 
