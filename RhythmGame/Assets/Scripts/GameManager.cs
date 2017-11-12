using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;

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

	private List <int> orderList = new List<int>();
	private DateTime lastCreateEnemy;
	private const int REPOP_TIME = 500;

	// Use this for initialization
	void Start () {

		//アロー作成
		orderList.Add(1);
		orderList.Add(2);
		orderList.Add(3);
		orderList.Add(2);
		orderList.Add(2);
		orderList.Add(3);
		orderList.Add(1);
		orderList.Add(1);
		orderList.Add(3);
		orderList.Add(1);

		// 現在時刻
		lastCreateEnemy = DateTime.UtcNow;

		// ノーツ表示
		InitTextNotes();
	}
	
	// Update is called once per frame
	void Update () {
		// リポップ予定時刻を過ぎていなければreturn.
		TimeSpan timeSpan = DateTime.UtcNow - lastCreateEnemy;
		if (timeSpan < TimeSpan.FromMilliseconds(REPOP_TIME)) {
			return;
		}
		lastCreateEnemy = DateTime.UtcNow;
		// 最大まで生成したら何もしない
		if (orderList.Count - 1 < count) {
			return;
		}

		// 敵を生成
		if (MAX_COUNT >= nowEnemyCount) {
			CreateEnemy (ref orderList);
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
