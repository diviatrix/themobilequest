using UnityEngine;
using System.Collections;

public class AniStarter : MonoBehaviour {
	public GameObject target;
	public bool reanimatable = false;
	public bool animated = false;
	
	public void AniStart () {
		if (reanimatable){
	    	if (target != null)
				target.GetComponent<AniStarter>().AniStartTarget();
			else 
				animation.Play();
		}
		else if (!reanimatable && !animated){
			if (target != null){
				target.GetComponent<AniStarter>().AniStartTarget();
				animated = true;
			}
			else {
				animation.Play();
				animated = true;
			}
		}			
	}
	public void AniStartTarget () {
		this.animation.Play();
		Debug.Log("poo");
	}
}
