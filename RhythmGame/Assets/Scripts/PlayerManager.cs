using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	private Rigidbody2D rbody; //プレーヤー制御用rigidbody2D
	private float basePositionX = -2.0f;
	private float basePositionY = -0.2f;
	private float changePositionY = 0.7f;
	private Animator animator;
	private bool gurd = false;

	public Sprite[] knightPic = new Sprite[2];

	// Use this for initialization
	void Start () {
		transform.position = new Vector2(basePositionX, basePositionY);
		animator = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update (){
		animator.SetBool ("isGurd",gurd);



		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			// 上キーを押した時
			transform.position = new Vector2(basePositionX, basePositionY);

		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			// 右キーを押した時
			transform.position = new Vector2(basePositionX,basePositionY - changePositionY); 

		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			// 下キーを押した時
			transform.position = new Vector2(basePositionX,basePositionY - (changePositionY * 2)); 

		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			// スペースキーを押した時
			Destroy(this.gameObject);
		}
    }

	void OnTriggerEnter2D(Collider2D col){
		
	}

	void FixedUpdate (){
		//  エンターキーが押されたら攻撃フラグを立てる
		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.DownArrow)) {
			gurd = true;	
	//		StartCoroutine ("WaitForGurd");
			return;
		}
		if (gurd) {
			gurd = false;
			return;

		}
	}

//	IEnumerable WaitForGurd (){
//		yield return new WaitForSeconds(0.5f);
//		gurd = false;
//	}

}