using UnityEngine;

public interface IDamageble
{
	void TakeHit(float dam, RaycastHit hit);
	void TakeDamage(float dam);
}