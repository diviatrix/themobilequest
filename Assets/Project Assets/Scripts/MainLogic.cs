using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainLogic : MonoBehaviour {
	public Transform mobilecontroller;
	public Transform mobilecontrollercamera;
	public Transform pccontroller;
	public Transform pccontrollercamera;

	public bool interact;
	public Texture cursor;
	public string goalText;
	public RaycastHit hit;
	public RaycastHit lookouthit;
	
	Component itemScript;
	Texture newitemTex;
	
	public Color normalcolor = new Color(0.1f,0.1f,0.1f,0.5f);
	public Color activitycolor = new Color(0.1f,0.2f,0.2f,0.5f);
	public Color itemcolor = new Color(0.1f,0.2f,0.2f,0.5f);
	
	public LinkedList<string> inventorylist = new LinkedList<string>();
	//public ArrayList<string> inventoryitems = new ArrayList<string>();
	
	void Start(){
	// если играем на андроиде выключить пк контроллер и наоборот
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer){
			mobilecontroller.gameObject.active = false;
			mobilecontrollercamera.gameObject.active = false;
		}
		
		if (Application.platform == RuntimePlatform.Android){
			pccontroller.gameObject.active = false;
			pccontrollercamera.gameObject.active = false;
		}
	}
	
	void Update()
	{
		
		//луч когда смотрим, и его обработка
		//Ray lookray
		Ray lookray = Camera.main.ScreenPointToRay(new Vector2(Screen.width, Screen.height)/2); // рисую луч из центра экрана
		if (Physics.Raycast(lookray, out lookouthit) && lookouthit.transform.tag == ("interactable") && lookouthit.distance <= 3)
			this.transform.guiTexture.color = activitycolor;

		else if (Physics.Raycast(lookray, out lookouthit) && lookouthit.transform.GetComponent<PickableObj>() && lookouthit.distance <= 3)
			this.transform.guiTexture.color = itemcolor;
			
		else
			this.transform.guiTexture.color = normalcolor;
		
		//луч при клике и его обработка
		
		Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition); //рисую луч из точки прикосновения к экрану
		Vector3 center = new Vector3(Screen.width, Screen.height, 0) / 2; // задаём вектор - центр экрана, для вычисления ареашки обработки тыка
		if (Input.GetMouseButtonDown(0) && (Vector3.Distance(Input.mousePosition, center) < 100)) { // если ткнул пальцем И дистанция от центра не больше 100 пикселей, то
			if (Physics.Raycast(touchray, out hit) && hit.distance <= 3) { // рисуем физический луч, который выходит из точки прикосновения к экрану и перпендикулярно плоскости камеры, он даёт точку прикосновения с физическим объектом - hit, если дистанция меньше 3 
				Debug.Log("X:" + hit.transform.gameObject.ToString()); // и пишет имя объекта
				
				if (hit.transform.GetComponent<PickableObj>() != null){
					hit.transform.GetComponent<PickableObj>().GetItem();
					newitemTex = hit.transform.GetComponent<PickableObj>().invtex;
					inventorylist.AddLast (hit.transform.GetComponent<PickableObj>().objname);
					goalText = hit.transform.GetComponent<PickableObj>().pickgoal;
				}
				
				//обработка места использования тула
				if (hit.transform.tag == "interactable"){
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
				//	if (hit.transform.name == "box_top"){  //тут добавить условие, что коробка стоит на столе
				//		GameObject.Find("box_tech_on_table").GetComponent<AniStarter>().AniStart();
				//		hit.transform.tag = "Untagged";
				//	}
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
					// example - changed the way you kiss me.mp3
					if (hit.transform.name == "example") {
							hit.transform.GetComponent<example>().povar();
							hit.transform.tag = "Untagged";
						}
					
					// отодвигаем мусор
					if (hit.transform.name == "movingtrashbox") {
						hit.transform.GetComponent<AniStarter>().AniStart();
						hit.transform.tag = "Untagged";
						}
					// тыкаем на печку
					if (hit.transform.name == "Stove"){
						//	Instantiate(explosionPrefab, hit.transform.position, transform.rotation);
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
			// обработка interact 
			if (hit.transform.GetComponent<AniStarter>())
				hit.transform.GetComponent<AniStarter>().AniStart();
			
			// обработка реплик персонажа на предметы
			if (hit.transform.GetComponent<GoalText>()) {
				// не готов к активити
				if (hit.transform.GetComponent<GoalText>())
					goalText = hit.transform.GetComponent<GoalText>().gtextbefore;
				// готов к активити
				else if (hit.transform.GetComponent<GoalText>().activityready)
					goalText = hit.transform.GetComponent<GoalText>().gtextready;
				// после активити
				else if (hit.transform.GetComponent<GoalText>().activitydone)
					goalText = hit.transform.GetComponent<GoalText>().gtextdone;
			}
		}	
	}
	
	
	
	void OnGUI() {
		
		// скейл для gui
		float x = 1;
		if (Screen.width <= 480)
		x = 2;
		
						// --- И Н В Е Н Т А Р Ь --- //
		
		
		//int index;        // Порядковый номер в списке
		//int rowCount = 5; // Максимальное количество в столбце

		//int col = Math.floor(index / rowCount); // Столбец
		//int row = index % rowCount;             // Строка

		//Vector2 position = Vector2( col * 66, row * 66 ); // Позиция на экране
		
		
		//Читаю количество предметов в инвентаре, беру названия, ищу для них объекты, читаю из объектов текстуры
		// создаю для них боксы 64
		for( int i = 0; i < inventorylist.Count; i++ )
		{
			GUI.Box (new Rect (Screen.width-82/x, 18/x + 100 * i, 64/x, 64/x), newitemTex);
		}
		
		
			
		
		
		
		

		//Goal bar
		
		GUI.Box(new Rect(Screen.width/8*x,0,Screen.width - Screen.width/4*x,30), goalText);
		
		//Гуй меню
		GUI.Box (new Rect (0,0,Screen.width/8*x,Screen.height/3), "Menu");
		if (GUI.Button(new Rect(10, Screen.height/16*x, Screen.width/10*x, Screen.height/10), "Reload"))
			Application.LoadLevel(Application.loadedLevel);
		if (GUI.Button(new Rect(10, Screen.height/5, Screen.width/10*x, Screen.height/10), "Exit"))
			Application.Quit();
	}
}

/*

*/