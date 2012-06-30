using UnityEngine;
using System.Collections;

public class ChangeTexture : MonoBehaviour {
	public Texture onTex;
	public Texture offTex;
	public bool on;
	public bool activateKids;
	public GameObject parentObj;
	
	void Start() {
		if (onTex == null || offTex == null)
			Debug.LogWarning("One or both textures missing at " + this.name);
		on = false;
	}
	
	void Update(){
		
	}
	
	public void Change() {
		
		Component[] childrenrenderers;
		
		if (!on){
			transform.renderer.material.mainTexture = onTex;
			on = true;
			if (activateKids)
			{
			childrenrenderers = GetComponentsInChildren<Renderer>(); // ищем рендеры детишек 
			foreach (Renderer renderers in childrenrenderers) // каждый из них
            	renderers.renderer.material.mainTexture = onTex;	// отключаем
			}
			if (parentObj != null)
			{
				parentObj.renderer.material.mainTexture = onTex;
			}
		}
		else if (on) {
			transform.renderer.material.mainTexture = offTex;
			on = false;
			if (activateKids)
			{
			childrenrenderers = GetComponentsInChildren<Renderer>(); // ищем рендеры детишек 
			foreach (Renderer renderers in childrenrenderers) // каждый из них
            	renderers.renderer.material.mainTexture = offTex;	// отключаем
			}
			if (parentObj != null)
			{
				parentObj.renderer.material.mainTexture = offTex;
			}
		}
	}
}
