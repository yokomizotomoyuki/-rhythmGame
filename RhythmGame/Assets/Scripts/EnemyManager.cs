using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManager : MonoBehaviour {

	public float basePositionX = 5.0f;
	public float basePositionY = -0.2f;
	private float speed = 0.05f;

	//現在速度
	public float nowX;
	private bool isInTapArea = false;
	public KeyCode keyCode;

	private GameObject gameManager;

	// Use this for initialization
	protected void Start () {
		transform.position = new Vector2(basePositionX, basePositionY);
		nowX = basePositionX;
		gameManager = GameObject.Find ("GameManager");
	}

	// Update is called once per frame
	protected void Update (){
		this.nowX -= speed;

		transform.position = new Vector2(this.nowX, basePositionY);
		if (Input.GetKeyDown (keyCode)) {
			tapDestroy ();
		}
	}

	public void tapDestroy(){
		if (this.isInTapArea) {
			Destroy (this.gameObject);

		}
	}

	//オブジェクトが衝突したとき
	void OnTriggerEnter2D(Collider2D collider2D) {
		if (collider2D.gameObject.tag == "Player") {
			Destroy (this.gameObject);
			gameManager.GetComponent<GameManager> ().DecrimentEnemyCount ();
		}
		this.isInTapArea = true;
	}


	//オブジェクトが離れた時
	void OnTriggerExit2D(Collider2D collider2D) {
		this.isInTapArea = false;
	}
}
