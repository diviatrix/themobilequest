using UnityEngine;
using System.Collections;

public class PickableObj : InteractObj {
	public GameObject parentObj;
	public GameObject geteffect;
	public GameObject activateTrigger;
	public Texture invtex;
	public string objname;
	public string pickgoal;
	public AudioClip picksound;
	
	
	
	void Start(){
		
		if (pickgoal == null)
			pickgoal = "You got: " + objname;
		
		if (objname == null) 
			objname = this.name;
		
		if (geteffect == null)
			geteffect = GameObject.Find("GotItem");
		
		if (picksound == null)
			picksound = GameObject.Find("GotItem").audio.clip;
	}
	
	// по запросу убиваем кхуям (совсем, рус.)
	
	public Component[] childrenrenderers; // создаю массив со всеми рендерами детишек
	public Component[] childrencolliders; // создаю массив со всеми коллайдерами детишек
	
	public void GetItem()
	{
		
		if (activateTrigger != null) {
			activateTrigger.gameObject.active = true;
		}
		if (parentObj == null) { //если нет родительского объекта
			if (renderer != null){
				this.gameObject.renderer.enabled = false;	// отключаем рендерер 
				this.gameObject.collider.enabled = false;	// отключаем коллайдер
			}
			
			childrenrenderers = GetComponentsInChildren<Renderer>(); // ищем рендеры детишек 
			foreach (Renderer renderers in childrenrenderers) // каждый из них
            	renderers.renderer.enabled = false;	// отключаем
			
			childrencolliders = GetComponentsInChildren<Collider>(); // то же с коллайдерами
			foreach (Collider colliders in childrencolliders)
				colliders.collider.enabled = false;
			
		}
		else { //esli yest papka
			childrenrenderers = parentObj.GetComponentsInChildren<Renderer>();
			foreach (Renderer renderers in childrenrenderers)
            	renderers.renderer.enabled = false;
			
			childrencolliders = parentObj.GetComponentsInChildren<Collider>();
			foreach (Collider colliders in childrencolliders)
				colliders.collider.enabled = false;
		}
		
		Instantiate (geteffect, this.transform.position, this.transform.rotation);
		AudioSource.PlayClipAtPoint(picksound, this.transform.position);
	}
}