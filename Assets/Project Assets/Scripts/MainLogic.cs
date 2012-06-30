using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainLogic : MonoBehaviour {
	
	// задаём контроллер для отключения на нужной платформе.
	public Transform mobilecontroller;
	public Transform mobilecontrollercamera;
	public Transform pccontroller;
	public Transform pccontrollercamera;

	public Texture cursor;
	public string goalText;
	public RaycastHit hit;
	public RaycastHit lookouthit;
	
	Component itemScript;
	Texture newitemTex;
	
	public Color normalcolor = new Color(0.1f,0.1f,0.1f,0.5f);
	public Color activitycolor = new Color(0.1f,0.2f,0.2f,0.5f);
	public Color itemcolor = new Color(0.1f,0.2f,0.2f,0.5f);
	
	public List<GameObject> inventorylist = new List<GameObject>();
	//public ArrayList<string> inventoryitems = new ArrayList<string>();
	
	void Start()
	{
	// если играем на андроиде выключить пк контроллер и наоборот
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsWebPlayer)
		{
			Destroy(mobilecontroller.gameObject);
			mobilecontrollercamera.gameObject.active = false;
		}
		
		if (Application.platform == RuntimePlatform.Android)
		{
			Destroy(pccontroller.gameObject);
		} 
	}
	
	void Update()
	{
//		if (inventorylist.Find ()){
//			Debug.Log("111");
//		}
		
		
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
			if (Physics.Raycast(touchray, out hit) && hit.distance <= 3) 
			{ // рисуем физический луч, который выходит из точки прикосновения к экрану и перпендикулярно плоскости камеры, он даёт точку прикосновения с физическим объектом - hit, если дистанция меньше 3 
				Debug.Log("X:" + hit.transform.gameObject.ToString()); // и пишет имя объекта
				
			if (hit.transform.GetComponent<PickableObj>() != null)
  				{
					var item = hit.transform.GetComponent<PickableObj>().gameObject;
     				item.GetComponent<PickableObj>().GetItem();
					inventorylist.Add( item );
    				goalText = item.GetComponent<PickableObj>().pickgoal;
   				}
						// обработка interact 
			if (hit.transform.GetComponent<AniStarter>())
				hit.transform.GetComponent<AniStarter>().AniStart();
			
			// обработка смены текстуры
			if (hit.transform.GetComponent<ChangeTexture>())
				hit.transform.GetComponent<ChangeTexture>().Change();
			
			// обработка триггера смены громкости
			if (hit.transform.GetComponent<VolumeChangeTrigger>())
				hit.transform.GetComponent<VolumeChangeTrigger>().VolChange();
			
			// Обработка триггера включения объекта
			if (hit.transform.GetComponent<EnableTrigger>())
				hit.transform.GetComponent<EnableTrigger>().ActivateTrigger();
							
			// обработка реплик персонажа на предметы
			if (hit.transform.GetComponent<GoalText>()) 
			{
				// не готов к активити
				if (hit.transform.GetComponent<GoalText>().activitynotready)
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
}
	
	void OnGUI() 
	{
		
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
	//	for( int i = 0; i < inventorylist.Count; i++ )
	//	{
	//		GUI.Box (new Rect (Screen.width-82/x, 18/x + 100 * i, 64/x, 64/x), newitemTex);
	//	}
		
		// рисую инвентарь.
			for( int i = 0; i < inventorylist.Count; i++ ) // для каждого объекта в массиве
  				{
					Texture invtexture = inventorylist[i].GetComponent<PickableObj>().invtex; // берем текстуру
   					GUI.Box (new Rect (Screen.width-82/x, 18/x + 80/x * i, 64/x, 64/x), invtexture); // и создаём бокс с этой текстурой 
  				}					

		// проверка на тыканием пальцем в текстуру круга по центру
		if (Input.touchCount == 1)
		{
			if (guiTexture.HitTest(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y,0)))
			{
				GUI.Box( new Rect(Screen.width/2, Screen.height/2, 32, 32), "box");
			}
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


