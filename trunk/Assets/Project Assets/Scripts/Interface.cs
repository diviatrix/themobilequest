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
	bool gotplank, fireburning;
	public RaycastHit hit;
	public string inventoryitemName;
	public GameObject inventoryitem;
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
			if (Physics.Raycast(touchray, out hit) && hit.distance <= 3) {
				Debug.Log("X:" + hit.transform.gameObject.ToString());
				if (hit.transform.tag == "pickable"){
					inventoryitemName = hit.transform.name;
					inventoryitem = hit.transform.gameObject;
					hit.transform.gameObject.active = false;
					// Если берем палку
					if (hit.transform.name == "Plank"){
						gotplank = true;
					}
					// 
					goalText ="You got: "+ hit.transform.name.ToString();
				}
				
				//обработка места использования тула
				else if (hit.transform.tag == "interactable"){
					if (hit.transform.name == "Stove"){
						if (gotplank){
							goalText ="Duh, it will keep fire burning a little more.";
							fireburning = true;
							inventoryitemName = null;
							inventoryitem.gameObject.active = true;
						//	inventoryitem.gameObject.transform.position = hit.transform.position;
							Instantiate(explosionPrefab, hit.transform.position, transform.rotation);
							}
						}
				}
				//обработка зоны подсказки
				else if (hit.transform.tag == "tiparea" && hit.transform.name == "firehint"){
				}

				if (hit.transform.name == "safe"){
						goalText ="Hmm, i havent any idea about this";
						}
				if (hit.transform.name == "trash"){
						goalText ="It smells like chemicals";
						}
				if (hit.transform.name == "Table"){
						goalText ="Seems like someone worked hard here";
						}
				/*if (!burnedbook && hit.transform.name == "wallbutton"){
						goalText ="It's frozen, cant push it";
						}*/
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
			Application.LoadLevel(Application.loadedLevel);
		if (GUI.Button(new Rect(10, Screen.height/5, Screen.width/10*x, Screen.height/10), "Exit"))
			Application.Quit();
		//GUI.Label(new Rect(Screen.width/2, 10, 100, 80), Input.touchCount.ToString());
		
		//Инвентарь
		GUI.Box (new Rect (Screen.width-Screen.width/8*x,0,Screen.width/8*x,Screen.height/3), "Inventory");
		GUI.Box (new Rect (Screen.width-Screen.width/8*x + 10, Screen.height/16*x, Screen.width/8*x-20,Screen.height/16-10), inventoryitemName);
			
		
	}
}
