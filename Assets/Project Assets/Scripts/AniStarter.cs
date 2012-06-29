using UnityEngine;
using System.Collections;

public class AniStarter : MonoBehaviour {
	public GameObject target;
	public bool reanimatable = false;
	public bool animated = false;
	public AudioClip picksound;
	
	
	void Start () 
	{
		if (picksound == null)
			picksound = GameObject.Find("GotItem").audio.clip;
	}
	
	public void AniStart () 
	{
		if (reanimatable){
	    	if (target != null)
				target.GetComponent<AniStarter>().AniStartTarget();
			else 
			{
				animation.Play();
				AudioSource.PlayClipAtPoint(picksound, this.transform.position);
			}
		}
		else if (!reanimatable && !animated){
			if (target != null){
				target.GetComponent<AniStarter>().AniStartTarget();
				animated = true;
			}
			else {
				animation.Play();
				animated = true;
				AudioSource.PlayClipAtPoint(picksound, this.transform.position);
			}
		}			
	}
	public void AniStartTarget () {
		this.animation.Play();
		AudioSource.PlayClipAtPoint(picksound, this.transform.position);
	}
}
