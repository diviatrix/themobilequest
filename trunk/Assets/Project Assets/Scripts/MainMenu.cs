

using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{

	void OnGUI()
	{
		
	//скейл для gui	
	float x = 1;
	if (Screen.width <= 480)
		x = 2;
	
		//Гуй меню
		GUI.Box (new Rect (0,0,Screen.width/8/x,Screen.height/3), "Menu");
		if (GUI.Button(new Rect(10, Screen.height/16/x, Screen.width/10/x, Screen.height/10/x), "Play"))
			Application.LoadLevel(1 );
		if (GUI.Button(new Rect(10, Screen.height/16/x+Screen.height/10/x*2, Screen.width/10/x, Screen.height/10/x), "Exit"))
			Application.Quit();
		
		GUI.Box (new Rect(Screen.width/10/x*2,0, Screen.width/10/x, Screen.height/10), "Graphics");
		if (GUI.Button(new Rect(Screen.width/10/x*2, Screen.height/16/x, Screen.width/10/x, Screen.height/10/x), "Better"))
			QualitySettings.IncreaseLevel();
		if (GUI.Button(new Rect(Screen.width/10/x*2, Screen.height/16/x+Screen.height/10/x*2, Screen.width/10/x, Screen.height/10/x), "Worther"))
			QualitySettings.DecreaseLevel();
	}
}
