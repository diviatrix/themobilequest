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
	
	public Component[] childrenrenderers; // создаю массив со всеми рендерами детишек
	public Component[] childrencolliders; // создаю массив со всеми коллайдерами детишек
	
	public void GetItem(){
		if (parentObj == null) {
			this.gameObject.renderer.enabled = false;	
			this.gameObject.collider.enabled = false;
		}
		else {
			childrenrenderers = parentObj.GetComponentsInChildren<Renderer>();
			foreach (Renderer renderers in childrenrenderers)
            	renderers.renderer.enabled = false;
			
			childrencolliders = parentObj.GetComponentsInChildren<Collider>();
			foreach (Collider colliders in childrencolliders)
				colliders.collider.enabled = false;
		}
		Instantiate (geteffect, this.transform.position, this.transform.rotation);
	}
} 


 /*
  	public Component[] hingeJoints;
    void Example() {
        hingeJoints = GetComponentsInChildren<HingeJoint>();
        foreach (HingeJoint joint in hingeJoints) {
            joint.useSpring = false;
        }
    }
    */