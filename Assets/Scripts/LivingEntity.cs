using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageble {

	public float startingHealth;
	protected float health;
	protected bool dead;

	public event System.Action OnDeath;

	public void TakeHit(float damage, RaycastHit hit){
		//detect point where hitted
		TakeDamage(damage);
	}

	public void TakeDamage(float damage){
		health -= damage;
		
		if(health <= 0 && !dead)
		{
			Die();
		}
	}

	public virtual void Start(){
		health = startingHealth;
		dead = false;
	}

	public void Die(){
		dead = true;
		if(OnDeath != null)
		{
			OnDeath();
		}
		GameObject.Destroy(gameObject);
	}

}
