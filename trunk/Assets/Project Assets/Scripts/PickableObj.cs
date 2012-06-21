using UnityEngine;
using System.Collections;

public class PickableObj : InteractObj {
	Transform explosionPrefab;
	public Texture picktexture;
	
	void Start(){
		explosionPrefab = GameObject.Find("GotItem").transform;
	}
	
	void OnGUI(){
	if (GUI.Button(new Rect(-64, -64, 64, 64), picktexture)){
			Destroy(gameObject);
			Instantiate(explosionPrefab, transform.position, transform.rotation);
			Debug.Log("drfsdf");
		}
	}
	
	protected override void OnClick()
	{
		base.OnClick();
	}
}
