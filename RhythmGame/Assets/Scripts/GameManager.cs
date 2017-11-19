using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;
public class GameManager : MonoBehaviour {

	//オブジェクト参照
	public GameObject demonUpPrefab;
	public GameObject demonCenterPrefab;
	public GameObject demonDownPrefab;
	public GameObject textNotes;
	private const int MAX_COUNT=10;
	private int count = 0;
	public int nowEnemyCount = 0;
	public Boolean startFlg = false;
	public Boolean aBottonFlg = true;

	private List <int> orderList = new List<int>();
	private DateTime lastCreateEnemy;
	private const int REPOP_TIME = 400;

	// Use this for initialization
	void Start () {

		if (startFlg  && aBottonFlg) {
			aBottonFlg = false;
			//アロー作成
			orderList.Clear();
			count = 0;
			nowEnemyCount = 0;

			for (int i = 0; i <= 10; i++) {
				orderList.Add (UnityEngine.Random.Range (1, 4));
				//orderList.Add (UnityEngine.Random.Range (1, 2));
			}

			// 現在時刻
			lastCreateEnemy = DateTime.UtcNow;

			// ノーツ表示
			InitTextNotes ();
		}
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.A)) {
			startFlg = true;
			Start ();
		}

		if (startFlg) {
			// リポップ予定時刻を過ぎていなければreturn.
			TimeSpan timeSpan = DateTime.UtcNow - lastCreateEnemy;
			if (timeSpan < TimeSpan.FromMilliseconds (REPOP_TIME)) {
				return;
			}
			lastCreateEnemy = DateTime.UtcNow;
			// 最大まで生成したら何もしない
			if (orderList.Count - 1 < count) {
				startFlg = false;
				aBottonFlg = true;
				return;
			}

			// 敵を生成
			if (MAX_COUNT >= nowEnemyCount) {
				CreateEnemy (ref orderList);
			}	
		}
	}

	public void CreateEnemy(ref List<int> orderList) {

		int enemyPosition = orderList[count];
		switch(enemyPosition){
		case 1:
			CreateEnemyObject (demonUpPrefab);
			break;
		case 2:
			CreateEnemyObject (demonCenterPrefab);
			break;
		case 3:
			CreateEnemyObject (demonDownPrefab);
			break;
		default:
			break;
		}

	}

	private GameObject CreateEnemyObject(GameObject demon)
	{
		if (MAX_COUNT < nowEnemyCount) {
			return null;
		}
		this.count++;
		this.nowEnemyCount++;
		return (GameObject)Instantiate (demon);
	}

	public void DecrimentEnemyCount()
	{
		this.nowEnemyCount--;
	}

	private void InitTextNotes() {
		string notes = "";
		foreach (int order in orderList) {
			switch(order){
			case 1:
				notes += "上";
				break;
			case 2:
				notes += "右";
				break;
			case 3:
				notes += "下";
				break;
			default:
				break;
			}
		} 

		textNotes.GetComponent<Text> ().text = notes;
	}
}
