using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interface : MonoBehaviour {
	
	Transform explosionPrefab;
	public bool interact;
	public Texture cursor;
	public string goalText;
	bool gotplank, fireburning;
	public RaycastHit hit;
	public RaycastHit lookouthit;
	public string inventoryitemName;
	public GameObject inventoryitem;
	Transform clone;
	
	void Start(){
		explosionPrefab = GameObject.Find("GotItem").transform;
		
		//animation.wrapMode = WrapMode.Once;
	}
	
	void Update()
	{
		
		//луч когда смотрим, и его обработка
		//Ray lookray
		Ray lookray = Camera.main.ScreenPointToRay(new Vector2(Screen.width, Screen.height)/2); // рисую луч из центра экрана
		if (Physics.Raycast(lookray, out lookouthit) && lookouthit.transform.tag == ("interactable") && lookouthit.distance <= 3){
			this.transform.guiTexture.color = new Color(0.2f,0.4f,0.4f,0.5f);
		}
		else 
			this.transform.guiTexture.color = new Color(0.2f,0.2f,0.2f,0.5f);
		
		//луч при клике и его обработка
		
		Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition); //рисую луч из точки прикосновения к экрану
		Vector3 center = new Vector3(Screen.width, Screen.height, 0) / 2; // задаём вектор - центр экрана, для вычисления ареашки обработки тыка
		if (Input.GetMouseButtonDown(0) && (Vector3.Distance(Input.mousePosition, center) < 100)) { // если ткнул пальцем И дистанция от центра не больше 100 пикселей, то
			if (Physics.Raycast(touchray, out hit) && hit.distance <= 3) { // рисуем физический луч, который выходит из точки прикосновения к экрану и перпендикулярно плоскости камеры, он даёт точку прикосновения с физическим объектом - hit, если дистанция меньше 3 
				Debug.Log("X:" + hit.transform.gameObject.ToString()); // и пишет имя объекта
				
				// обработка подбирания предмета
				if (hit.transform.tag == "pickable"){
					inventoryitemName = hit.transform.name;
					inventoryitem = hit.collider.gameObject;
					Debug.Log(hit.collider.transform.position.ToString());
					Debug.Log(hit.transform.gameObject.transform.position.ToString());
					// Если берем палку
					if (hit.transform.name == "Plank"){
						gotplank = true;
						}
					// 
					goalText ="You got: "+ hit.transform.name.ToString();
				}
				
				//обработка места использования тула
				else if (hit.transform.tag == "interactable"){
					if (hit.transform.name == "fireplace_clip"){
						hit.transform.GetComponent<AniStarter>().AniStart();
						hit.transform.tag = "Untagged";
					}
					//открываем дверь
					if (hit.transform.name == "door"){
						hit.transform.GetComponent<AniStarter>().AniStart();
						hit.transform.tag = "Untagged";
					}
					// открываем коробку
					if (hit.transform.name == "box_top"){  //тут добавить условие, что коробка стоит на столе
						GameObject.Find("box_tech_on_table").GetComponent<AniStarter>().AniStart();
						hit.transform.tag = "Untagged";
					}
					// включаем ноут
					if (hit.transform.name == "notebook"){
						hit.transform.GetComponent<ChangeTexture>().Change();
						GameObject.Find("notebook_body").transform.GetComponent<ChangeTexture>().Change();
					}
					
					// открываем щиток
					if (hit.transform.name == "shielddoor") {
							hit.transform.GetComponent<AniStarter>().AniStart();
							hit.transform.tag = "Untagged";
						}
					
					// отодвигаем мусор
					if (hit.transform.name == "movingtrashbox") {
						hit.transform.GetComponent<AniStarter>().AniStart();
						hit.transform.tag = "Untagged";
						}
					// тыкаем на печку
					if (hit.transform.name == "Stove"){
						if (gotplank){ // неверно! переписать чтобы проверялось наличие предмета в инвентаре.
							goalText ="Duh, it will keep fire burning a little more.";
							fireburning = true;
							inventoryitemName = null;
							inventoryitem.transform.position = hit.transform.position;
							Instantiate(explosionPrefab, hit.transform.position, transform.rotation);
							}
						}
					}
				}


				//обработка зоны подсказки
				else if (hit.transform.tag == "tiparea"){
				}
				
				//обработка активити
				else if (hit.transform.tag == "activity"){
					/* hit.transform.gameObject.active = false;
					GameObject activity;
					activity = GameObject.Find(hit.transform.gameObject.name+"1");
					activity.collider.enabled = true;
					activity.renderer.enabled = true; */ 
				
				}
				// обработка реплик персонажа на предметы
				if (hit.transform.name == "window"){
						goalText ="It's locked, damn!";
						}
				if (hit.transform.name == "Trash"){
						goalText ="It smells like chemicals";
						}
				if (hit.transform.name == "Table"){
						goalText ="Seems like someone worked hard here";
						}
				if (hit.transform.name == "boxwithtool"){
						goalText ="Maybe there is something useful in this box";
						}
				if (hit.transform.name == "emptybox"){
						goalText ="Nothing needed in this box";
						}
				if (hit.transform.name == "Stove"){
						goalText ="This place is very cold, i should make some fire in this stove";
						}
				/*if (!burnedbook && hit.transform.name == "wallbutton"){
						goalText ="It's frozen, cant push it";
						}*/
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
