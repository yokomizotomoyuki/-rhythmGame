using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

	public float basePositionX = 5.0f;
	public float basePositionY = -0.2f;
	private float speed = 0.05f;
	public AudioClip gurdHitSE;
	public AudioClip damageSE;
	private AudioSource audioSource;
	public Sprite[] spriteImage = new Sprite[1];

	//現在速度
	public float nowX;
	private bool isInTapArea = false;
	private bool isDestroy = false;
	public KeyCode keyCode;

	private GameObject gameManager;

	// フェードアウトしながら消える
	private float fadeTime  = 1.0f;
	private SpriteRenderer spRenderer;
	private float currentRemainTime;


	// Use this for initialization
	void Start () {
		audioSource = this.gameObject.GetComponent<AudioSource> ();
		transform.position = new Vector2(basePositionX, basePositionY);
		nowX = basePositionX;
		spRenderer = GetComponent<SpriteRenderer>();
		currentRemainTime = fadeTime;
		gameManager = GameObject.Find ("GameManager");
	}

	// Update is called once per frame
	void Update (){
		if (this.isDestroy) {
			currentRemainTime -= Time.deltaTime;
			if ( currentRemainTime <= 0f ) {
				// 残り時間が無くなったら自分自身を消滅
				GameObject.Destroy(gameObject);
				return;
			}

			// フェードアウト
			float alpha = currentRemainTime / fadeTime;
			var color = spRenderer.color;
			color.a = alpha;
			spRenderer.color = color;
			return;
		}
		this.nowX -= speed;

		transform.position = new Vector2(this.nowX, basePositionY);
		if (Input.GetKeyDown (keyCode)) {
			tapDestroy ();
		}
	}

	//タップエリアで消滅した時
	public void tapDestroy(){
		if (this.isInTapArea) {
			audioSource.PlayOneShot(gurdHitSE);
			print (GetComponent<Image> ());
			GetComponent<Image> ().sprite = spriteImage [0];
			this.isDestroy = true;
			//Destroy (this.gameObject, gurdHitSE.length);
		}
	}

	//オブジェクトが衝突したとき
	void OnTriggerEnter2D(Collider2D collider2D) {
		print (collider2D.gameObject.tag);
		if (collider2D.gameObject.tag == "Player") {
			audioSource.PlayOneShot(damageSE);
			this.isDestroy = true;
			//Destroy (this.gameObject,damageSE.length);
			gameManager.GetComponent<GameManager> ().DecrimentEnemyCount ();
			return;
		}

		if (collider2D.gameObject.tag == "Finish") {
			this.isInTapArea = true;
			return;
		}
	}


	//オブジェクトが離れた時
	void OnTriggerExit2D(Collider2D collider2D) {
		this.isInTapArea = false;
	}
}
