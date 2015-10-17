using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	Rigidbody body;
	Vector3 velocity;

	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}

	public void LookAt(Vector3 lookAtPoint)
	{
		Vector3 correctedPoint = new Vector3(lookAtPoint.x, transform.position.y, lookAtPoint.z);
		transform.LookAt(correctedPoint);
	}

	void FixedUpdate()
	{
		body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
	}

	void Start () {
		body = GetComponent<Rigidbody>();
	}
}
