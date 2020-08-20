using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickMoveCamera : MonoBehaviour
{
	public Joystick moveJoyStick;
	public Joystick panJoyStick;
	private Rigidbody rigidBody;

	private float rotY = 0.0f;
	private float rotX = 0.0f;
	void Start()
    {
		rigidBody = gameObject.GetComponent<Rigidbody>();
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
	}

    void Update()
    {
		Vector3 Vforward;
		Vector3 Vright;
		Vforward = transform.forward * moveJoyStick.Vertical * 1.5f;
		Vright = transform.right * moveJoyStick.Horizontal * 1.5f;
		rigidBody.velocity += Vector3.ClampMagnitude(Vforward, 4f);
		rigidBody.velocity += Vector3.ClampMagnitude(Vright, 4f);
		rotY += panJoyStick.Horizontal * 0.3f;
		rotX += -panJoyStick.Vertical * 0.3f;

		Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
		transform.rotation = localRotation;
	}
}
