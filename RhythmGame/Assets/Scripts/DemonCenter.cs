using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCenter : EnemyManager {

	// Use this for initialization
	void Start () {

		base.Start ();
		base.keyCode = KeyCode.RightArrow;

		
		
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
