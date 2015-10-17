using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed = 10.0f;

	public void SetSpeed(float newspeed)
	{
		speed = newspeed;
	}

	void Update () {
		gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}
}
