using UnityEngine;
using System.Collections;

public class ClickableObj : InteractObj {

	protected override void OnClick()
	{
		base.OnClick();
		Debug.Log("pew");

	}
}
