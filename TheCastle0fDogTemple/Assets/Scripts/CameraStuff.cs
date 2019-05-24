using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraStuff : MonoBehaviour {

	public Transform anchorPoint;
	public float rotateSpeed;

	// Update is called once per frame
	void Update ()
	{
		transform.RotateAround(anchorPoint.position, Vector3.up, rotateSpeed * Time.deltaTime);
	}
}
