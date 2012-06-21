using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interface : MonoBehaviour {
	
	Transform explosionPrefab;
	public bool interact;
	public Texture normal;
	public Texture pickable;
	public Texture interactable;
	public string goalText;
	bool gotbook, burnedbook;
	public RaycastHit hit;
	public string inventoryitem;
	Transform inventoryItemTransform;
	Transform clone;
	
	void Start(){
		explosionPrefab = GameObject.Find("GotItem").transform;
	}
	
	void Update()
	{
		
		//луч когда смотрим, и его обработка
		//Ray lookray
		
		
		//луч при клике и его обработка
		
		Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 center = new Vector3(Screen.width, Screen.height, 0) / 2;
		if (Input.GetMouseButtonDown(0) && (Vector3.Distance(Input.mousePosition, center) < 50)) {
			Debug.Log(center.ToString());
			if (Physics.Raycast(touchray, out hit) && hit.distance <= 3) {
				Debug.Log("X:" + hit.transform.gameObject.ToString());
				if (hit.transform.tag == "pickable"){
					Transform clone;
					inventoryitem = hit.transform.name;
					inventoryItemTransform = hit.transform;
					Destroy(hit.transform.collider);
					// Если берем книгу
					if (hit.transform.name == "Old Book"){
						gotbook = true;
					}
					// 
					goalText ="You got: "+ hit.transform.name.ToString();
					hit.transform.renderer.enabled = false;
					//Destroy(hit.transform.gameObject);
					Instantiate(explosionPrefab, hit.transform.position, transform.rotation);
				}
				
				//обработка места использования тула
				else if (hit.transform.tag == "interactable"){
					if (hit.transform.name == "Fire"){
						if (gotbook){
							goalText ="Duh, it will keep fire burning a little more.";
							burnedbook = true;
							inventoryitem = null;
							clone = Instantiate(inventoryItemTransform, hit.transform.position, transform.rotation) as Transform;
							clone.transform.renderer.enabled = true;
						}
						if (burnedbook){
							GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
       						sphere.transform.position = hit.transform.position;
							sphere.transform.localScale = new Vector3 (10,10,10);
							sphere.collider.isTrigger = true;
							Destroy(sphere.renderer);
						}

					Instantiate(explosionPrefab, hit.transform.position, transform.rotation);
					}
				}
				//обработка зоны подсказки
				else if (hit.transform.tag == "tiparea" && hit.transform.name == "firehint"){
					if (gotbook){
						goalText ="Fire is fading, let me keep it a little more.";
						Destroy(hit.transform.gameObject);
					}
				}
				if (hit.transform.name == "grid"){
						goalText ="Hmm, i havent any idea about this grid";
						}
				if (hit.transform.name == "bench"){
						goalText ="I think I should not just laying there";
						}
				if (!burnedbook && hit.transform.name == "wallbutton"){
						goalText ="It's frozen, cant push it";
						}
			}	
		}
	}

	
	void OnGUI() {
		
		//задаю ёмкость инвентаря, и предметы
		  
		var list = new LinkedList<string>();
		list.AddLast("ef");
		list.AddLast("efeefe");
		//list.Count - 
		
		float x = 1;
		if (Screen.width <= 480)
			x = 2;
		//Goal bar
		GUI.Box(new Rect(Screen.width/8*x,0,Screen.width - Screen.width/4*x,30), goalText);
		
		//Гуй меню
		GUI.Box (new Rect (0,0,Screen.width/8*x,Screen.height/3), "Menu");
		if (GUI.Button(new Rect(10, Screen.height/16*x, Screen.width/10*x, Screen.height/10), "Reload"))
			Application.LoadLevel("first");
		if (GUI.Button(new Rect(10, Screen.height/5, Screen.width/10*x, Screen.height/10), "Exit"))
			Application.Quit();
		//GUI.Label(new Rect(Screen.width/2, 10, 100, 80), Input.touchCount.ToString());
		
		//Инвентарь
		GUI.Box (new Rect (Screen.width-Screen.width/8*x,0,Screen.width/8*x,Screen.height/3), "Inventory");
		GUI.Box (new Rect (Screen.width-Screen.width/8*x + 10, Screen.height/16*x, Screen.width/8*x-20,Screen.height/16-10), inventoryitem);
			
		
	}
}
