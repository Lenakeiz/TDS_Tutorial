using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity {

	public enum State{Idle, Chasing, Attacking};
	State currState;

	Material skinMaterial;
	Color originalColor;

	NavMeshAgent pathFinder;
	Transform target;
	LivingEntity livingEntity;
	bool hastarget;

	float damage = 1.0f;
	float attackDistanceThreshold = 0.5f;
	float timeBtwAttacks = 1.0f;

	float nextAttackTime;

	float myCollisionRadius;
	float targetCollisionRadius;

	IEnumerator UpdatePath()
	{
		float refreshRate = 0.25f;
		while (hastarget)
		{
			if(currState == State.Chasing)
			{
				Vector3 directionToTarget = (target.position - transform.position).normalized;
				Vector3 targetPosition =  target.position - directionToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold * 0.5f); //new Vector3(target.position.x, 1, target.position.z);
				if(!dead)
					pathFinder.SetDestination(targetPosition);
			}
			yield return new WaitForSeconds(refreshRate);
		}
	}

	IEnumerator Attack(){

		currState = State.Attacking;
		pathFinder.enabled = false;
		skinMaterial.color = Color.red;

		Vector3 startPos = transform.position;
		Vector3 directionToTarget = (target.position - transform.position).normalized;
		Vector3 endPos =  target.position - directionToTarget * (myCollisionRadius); //new Vector3(target.position.x, 1, target.position.z);

		float percent = 0.0f;
		float attackSpeed = 3.0f;

		bool hasAppliedDamage = false;

		while(percent <= 1.0f)
		{
			percent += Time.deltaTime * attackSpeed;
			float interpolation = ((- percent * percent) + percent) * 4;
			transform.position = Vector3.Lerp(startPos, endPos, interpolation);
 			
			if(percent >= .5f && !hasAppliedDamage)
			{
				hasAppliedDamage = true;
				livingEntity.TakeDamage(damage);
			}

			yield return null;
		}
				
		pathFinder.enabled = true;
		skinMaterial.color = originalColor;
		currState = State.Chasing;

	}

	void OnTargetDeath()
	{
		hastarget = false;
		currState = State.Idle;
	}

	public override void Start () {
		base.Start();

		skinMaterial = GetComponent<Renderer>().material;
		originalColor = skinMaterial.color;

		currState = State.Chasing;
		pathFinder = gameObject.GetComponent<NavMeshAgent>();
		GameObject tar = GameObject.FindGameObjectWithTag("Player");

		if(tar != null)
		{
			target = tar.transform;
			hastarget = true;
			livingEntity = target.GetComponent<LivingEntity>();
			livingEntity.OnDeath += OnTargetDeath;
			
			myCollisionRadius = GetComponent<CapsuleCollider>().radius;
			targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
			
			StartCoroutine(UpdatePath());
		}
	}

	void Update () {

		if(hastarget){
			if(Time.time > nextAttackTime){
				float sqrDistanceTarget = (target.position - transform.position).sqrMagnitude;			
				if(sqrDistanceTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius,2)){
					nextAttackTime =  Time.time + timeBtwAttacks;
					StartCoroutine(Attack());
				}
			}
		}
	}
}
