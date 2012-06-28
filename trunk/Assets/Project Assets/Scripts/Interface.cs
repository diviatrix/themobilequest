using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interface : MonoBehaviour {
	public Transform mobilecontroller;
	public Transform mobilecontrollercamera;
	public Transform pccontroller;
	public Transform pccontrollercamera;
	Transform explosionPrefab;
	public bool interact;
	public Texture cursor;
	public string goalText;
	bool gotplank, fireburning;
	public RaycastHit hit;
	public RaycastHit lookouthit;
	Transform clone;
	public Color normalcolor = new Color(0.1f,0.1f,0.1f,0.5f);
	public Color lightcolor = new Color(0.1f,0.2f,0.2f,0.5f);
	public LinkedList<string> list = new LinkedList<string>();
	
	void Start(){
		explosionPrefab = GameObject.Find("GotItem").transform;
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
		if (Physics.Raycast(lookray, out lookouthit) && lookouthit.transform.tag == ("interactable") && lookouthit.distance <= 3){
			Color lerpcolor = Color.Lerp(normalcolor,lightcolor,Time.time);
			this.transform.guiTexture.color = lerpcolor;
		}
		else 
			this.transform.guiTexture.color = normalcolor;
		
		//луч при клике и его обработка
		
		Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition); //рисую луч из точки прикосновения к экрану
		Vector3 center = new Vector3(Screen.width, Screen.height, 0) / 2; // задаём вектор - центр экрана, для вычисления ареашки обработки тыка
		if (Input.GetMouseButtonDown(0) && (Vector3.Distance(Input.mousePosition, center) < 100)) { // если ткнул пальцем И дистанция от центра не больше 100 пикселей, то
			if (Physics.Raycast(touchray, out hit) && hit.distance <= 3) { // рисуем физический луч, который выходит из точки прикосновения к экрану и перпендикулярно плоскости камеры, он даёт точку прикосновения с физическим объектом - hit, если дистанция меньше 3 
				Debug.Log("X:" + hit.transform.gameObject.ToString()); // и пишет имя объекта
				
				// обработка подбирания предмета
				if (hit.transform.tag == "pickable"){
					Debug.Log(hit.collider.transform.position.ToString());
					Debug.Log(hit.transform.gameObject.transform.position.ToString());
					goalText ="You got: "+ hit.transform.name.ToString();
					
					if (hit.transform.name == "box_tech"){
						list.AddLast("box_tech");
						GameObject.Find("box_tech_main").active = false;
					}
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
						if (gotplank){ // АХИНЕЯ! переписать нормально						
							goalText ="Duh, it will keep fire burning a little more.";
							fireburning = true;
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
	
	
	// текстуры для gui
	Texture invTexture1;
	public Texture boxTexture;
	public Texture mcTexture;
	
	void OnGUI() {
		
		float x = 1;
		if (Screen.width <= 480)
		x = 2;
		//Читаю количество предметов в инвентаре
		int itemscount = list.Count;
			
		//Рисую Инвентарь
		//GUI.DrawTexture(new Rect (Screen.width-80, 16, 64, 64), boxTexture);
		
		for( int i = 0; i < list.Count; i++ )
		{
			GUI.Box (new Rect (Screen.width-82/x, 18/x + 100 * i, 66/x, 66/x), boxTexture);
		}
		
		
			
		
		
		
		

		//Goal bar
		
		GUI.Box(new Rect(Screen.width/8*x,0,Screen.width - Screen.width/4*x,30), goalText);
		
		//Гуй меню
		GUI.Box (new Rect (0,0,Screen.width/8*x,Screen.height/3), "Menu");
		if (GUI.Button(new Rect(10, Screen.height/16*x, Screen.width/10*x, Screen.height/10), "Reload"))
			Application.LoadLevel(Application.loadedLevel);
		if (GUI.Button(new Rect(10, Screen.height/5, Screen.width/10*x, Screen.height/10), "Exit"))
			Application.Quit();
		//GUI.Label(new Rect(Screen.width/2, 10, 100, 80), Input.touchCount.ToString());
		
	}
}

/*
 * int index;        // Порядковый номер в списке
int rowCount = 5; // Максимальное количество в столбце

int col = Math.floor(index / rowCount); // Столбец
int row = index % rowCount;             // Строка

Vector2 position = Vector2( col * 66, row * 66 ); // Позиция на экране
*/