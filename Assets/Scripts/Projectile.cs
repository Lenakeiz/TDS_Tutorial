using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	float speed = 10.0f;
	float damage = 1.0f;
	public LayerMask collisionMask;

	float lifeTime = 3.0f;
	float width = .1f;

	void Start(){
		Destroy(gameObject, lifeTime);

		Collider[] initialCollision = Physics.OverlapSphere(transform.position, .1f, collisionMask);

		if(initialCollision.Length > 0){
			OnHitObject(initialCollision[0]);
		}
	}

	public void SetSpeed(float newspeed)
	{
		speed = newspeed;
	}

	void CheckCollision(float distanceMove)
	{
		Ray ray = new Ray(transform.position, transform.forward);
		//Debug.DrawLine(transform.position, transform.forward * distanceMove, Color.magenta);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, distanceMove + width, collisionMask))
		{
			OnHitObject(hit);
		}
	}

	void OnHitObject(RaycastHit hit)
	{
		IDamageble damageObject = hit.collider.gameObject.GetComponent<IDamageble>();
		if(damageObject != null)
		{
			//Debug.Log("Hitted :" + hit.collider.gameObject.name);
			damageObject.TakeHit(damage, hit);
		}
		//Debug.Log(hit.collider.gameObject.name);
		GameObject.Destroy(gameObject);
	}

	void OnHitObject(Collider hit)
	{
		IDamageble damageObject = hit.GetComponent<IDamageble>();
		if(damageObject != null)
		{
			//Debug.Log("Hitted :" + hit.collider.gameObject.name);
			damageObject.TakeDamage(damage);
		}
		//Debug.Log(hit.collider.gameObject.name);
		GameObject.Destroy(gameObject);
	}

	void Update () {

		float distanceMove =  Time.deltaTime * speed;
		CheckCollision(distanceMove);
		gameObject.transform.Translate(Vector3.forward * distanceMove);

	}
}
