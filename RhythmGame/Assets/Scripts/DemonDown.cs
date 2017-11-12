using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDown : EnemyManager {

	// Use this for initialization
	void Start () {

		base.Start ();
		base.keyCode = KeyCode.DownArrow;
	}
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
