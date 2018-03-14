using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	[SerializeField] Transform target;
	public float moveSpeed = 5.5f;
	public float rotSpeed = 15.0f;
	public float jumpSpeed = 15.0f;
	public float terminalVelocity = -10.0f;
	public float minFall = -1.5f;
	public float push = 3f;

	private float _vertSpeed;
	private float gravity = -9.8f;

	Animator Anim;
	CharacterController charCont;

	// Initialization
	void Start () {
		Anim = GetComponent<Animator> ();
		charCont = GetComponent<CharacterController> ();
		_vertSpeed = minFall;
	}
	// Update is called once per frame
	void Update () {
		Vector3 movement = Vector3.zero;
		float horInput = Input.GetAxis ("Horizontal");
		float vertInput = Input.GetAxis ("Vertical");
		if (horInput != 0 || vertInput != 0) {
			// Перемещение
			movement.x = horInput * moveSpeed;
			movement.z = vertInput * moveSpeed;
			movement = Vector3.ClampMagnitude (movement, moveSpeed);
			Quaternion tmp = target.rotation;
			target.eulerAngles = new Vector3 (0, target.eulerAngles.y, 0);
			movement = target.TransformDirection (movement);
			target.rotation = tmp;
			Quaternion direction = Quaternion.LookRotation (movement);
			transform.rotation = Quaternion.Lerp (transform.rotation, direction, rotSpeed * Time.deltaTime);
		}
		Anim.SetFloat ("Speed", movement.sqrMagnitude);
			if (charCont.isGrounded) {
				if (Input.GetButtonDown ("Jump")) {
					_vertSpeed = jumpSpeed;
				} else {
				Anim.SetBool ("Jumping", false);
					_vertSpeed = minFall;
				}
			} else {
			_vertSpeed += gravity * 5 * Time.deltaTime;
				if (_vertSpeed < terminalVelocity)
					_vertSpeed = terminalVelocity;
			Anim.SetBool ("Jumping", true);
			}
			movement.y = _vertSpeed;
			movement *= Time.deltaTime;
			charCont.Move (movement);
			
	} void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody body = hit.collider.attachedRigidbody;
		if(body != null && !body.isKinematic)
		{
			body.velocity = hit.moveDirection * push;
		}
	}
}