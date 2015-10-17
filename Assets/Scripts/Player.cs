using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]

public class Player : LivingEntity {

	public float moveSpeed = 5.0f;
	private PlayerController controller;
	private GunController gunController;

	Camera cam;

	public override void Start () {
		base.Start();
		controller = GetComponent<PlayerController>();
		gunController = GetComponent<GunController>();
		cam = Camera.main;
	}

	void Update () {
		Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		Vector3 moveVelocity = moveInput.normalized * moveSpeed;

		controller.Move(moveVelocity);

		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		Plane ground = new Plane(Vector3.up,Vector3.zero);

		float rayDistance;

		if(ground.Raycast(ray, out rayDistance))
		{
			Vector3 rayPoint = ray.GetPoint(rayDistance);
			Debug.DrawLine(ray.origin, rayPoint, Color.red);
			controller.LookAt(rayPoint);
		}

		if(Input.GetMouseButton(0))
		{
			gunController.Shoot();
		}

	}
}
