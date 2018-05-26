using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour {

	[SerializeField] List<GameObject> playerUnits;
	[SerializeField] List<GameObject> enemyUnits;
	[SerializeField] GameObject square;
	GameObject[,] squares = new GameObject[12, 8];
	int playerTurn = 0, enemyturn = 0;
	bool player = true;

	public int rows = 8;
	public int colls = 12;
	public float moveSpeed = 5.5f;
	public float rotSpeed = 15.0f;
	public float push = 3f;

	public float offset = 0.02f; // Отступ между клетками
	public float deceleration = 20f;
	public float targetBuffer = 1.5f;
	public float curSpeed = 0f;
	Vector3 targetpos = Vector3.one;

	Animator Anim;
	CharacterController charCont;

	// Initialization
	void Start () {
		squares = new GameObject[colls, rows];
		Vector3 pos = new Vector3 (-(square.transform.localScale.x * colls + offset * (colls - 1)) / 2, 0.02f, -(square.transform.localScale.z * rows + offset * (rows - 1)) / 2);
		for (int i = 0; i < rows; i++) {
			for (int k = 0; k < colls; k++) {
				squares [k, i] = Instantiate (square, pos + new Vector3 (k *(square.transform.localScale.x + offset), 0, i * (square.transform.localScale.z + offset)),Quaternion.identity);
			}
		}
	}

	void ShowMove() {
		if (player) {
			Anim = playerUnits [playerTurn].GetComponent<Animator> ();
			charCont = playerUnits [playerTurn].GetComponent<CharacterController> ();
		} else {
			Anim = enemyUnits [enemyturn].GetComponent<Animator> ();
			charCont = enemyUnits [enemyturn].GetComponent<CharacterController> ();
		}
	}
	void Move(GameObject unit) {

	}
	// Update is called once per frame
	void Update () {
		ShowMove ();
		Vector3 movement = Vector3.zero;
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit mouseHit;
			if (Physics.Raycast (ray, out mouseHit)) {
				targetpos = mouseHit.point;
				curSpeed = moveSpeed;
			}
		}
		if (targetpos != Vector3.one && player) {
			targetpos = new Vector3 (targetpos.x, playerUnits[playerTurn].transform.position.y, targetpos.z);
			Quaternion targetRot = Quaternion.LookRotation(targetpos - playerUnits[playerTurn].transform.position);
			playerUnits[playerTurn].transform.rotation = Quaternion.Slerp (playerUnits[playerTurn].transform.rotation, targetRot,rotSpeed * Time.deltaTime);
			movement = curSpeed * Vector3.forward;
			movement = playerUnits[playerTurn].transform.TransformDirection (movement);
			if (Vector3.Distance (targetpos, playerUnits[playerTurn].transform.position) < targetBuffer) {
				curSpeed -= deceleration * Time.deltaTime;
				if (curSpeed <= 0) {
					targetpos = Vector3.one;
					playerTurn++;
					if (playerTurn > playerUnits.Count - 1)
						playerTurn = 0;
					player = !player;
				}
			}
		}
		else if (targetpos != Vector3.one && !player) {
			targetpos = new Vector3 (targetpos.x, enemyUnits[enemyturn].transform.position.y, targetpos.z);
			Quaternion targetRot = Quaternion.LookRotation(targetpos - enemyUnits[enemyturn].transform.position);
			enemyUnits[enemyturn].transform.rotation = Quaternion.Slerp (enemyUnits[enemyturn].transform.rotation, targetRot,rotSpeed * Time.deltaTime);
			movement = curSpeed * Vector3.forward;
			movement = enemyUnits[enemyturn].transform.TransformDirection (movement);
			if (Vector3.Distance (targetpos, enemyUnits[enemyturn].transform.position) < targetBuffer) {
				curSpeed -= deceleration * Time.deltaTime;
				if (curSpeed <= 0) {
					targetpos = Vector3.one;
					enemyturn++;
					if(enemyturn > enemyUnits.Count -1) {
						enemyturn = 0;
						player = !player;
					}
				}
			}
		}
		Anim.SetFloat ("Speed", movement.sqrMagnitude);
			movement *= Time.deltaTime;
			charCont.Move (movement);
}
}