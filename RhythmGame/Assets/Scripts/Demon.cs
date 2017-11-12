using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : EnemyManager {

	public void Start() {
		base.Start ();
		base.keyCode = KeyCode.UpArrow;
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}

}
