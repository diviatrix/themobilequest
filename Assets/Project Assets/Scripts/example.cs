using UnityEngine;
using System.Collections;

public class example : MonoBehaviour {
    public Color lerpedColor = Color.white;
	public bool letsgo = false;
	
    public void povar() {
       letsgo = true;
    }
	void Update () {
		if (letsgo){
		 lerpedColor = Color.Lerp(Color.white, Color.black, Time.time);
		this.transform.renderer.material.color = lerpedColor;
		}
	}
}