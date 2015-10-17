using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public Transform weaponHolder;
	public Gun startingGun;
	Gun equippedGun;

	public void EquipGun(Gun equip)
	{
		if(equippedGun != null)
		{
			GameObject.Destroy(equippedGun);
		}

		equippedGun = Instantiate(equip, weaponHolder.position, weaponHolder.rotation) as Gun;
		equippedGun.transform.SetParent(weaponHolder,true);
	}

	void Start () {
		if(startingGun != null)
		{
			EquipGun(startingGun);
		}
	}

}
