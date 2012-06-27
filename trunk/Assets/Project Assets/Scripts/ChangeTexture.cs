using UnityEngine;
using System.Collections;

public class ChangeTexture : MonoBehaviour {
	public Texture onTex;
	public Texture offTex;
	public bool on;
	
	void Start() {
		if (onTex == null || offTex == null)
			Debug.LogWarning("One or both textures missing at " + this.name);
		on = false;
	}
	
	void Update(){
		
	}
	
	public void Change() {
		if (!on){
			transform.renderer.material.mainTexture = onTex;
			on = true;
		}
		else if (on) {
			transform.renderer.material.mainTexture = offTex;
			on = false;
		}
	}
}
