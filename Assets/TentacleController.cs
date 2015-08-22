using UnityEngine;
using System.Collections;


public class TentacleController : MonoBehaviour
{
	public static float MaxDegreesOffVert = 90;
	public static float MinDegreesOffVert = 20;
	public static float DegreeChangePerUpdate = 0.5f;

	// [4] | [1]
	// =========
	// [3] | [2]
	public bool LeftTentacle = false;

	private bool CurrentlyActive = false;

	// Use this for initialization
	void Start ()
	{

	}
	

	void Update ()
	{
		// Firing action - don't change angle
		if (!this.CurrentlyActive) {
			// QuadrantFour (Left tentacle)
			if (LeftTentacle) {
				if (Input.GetKey (KeyCode.A)) {
					this.rotateTentacle (true);
				} else if (Input.GetKey (KeyCode.D)) {
					this.rotateTentacle (false);
				}
			} else {
				if (Input.GetKey (KeyCode.LeftArrow)) {
					this.rotateTentacle (true);
				} else if (Input.GetKey (KeyCode.RightArrow)) {
					this.rotateTentacle (false);
				}
			}
		}

		// Firing mechanism 
		if (LeftTentacle && Input.GetKey (KeyCode.W)) {
			Debug.Log ("Firing Left");
			this.CurrentlyActive = true;
		} else if (!LeftTentacle && Input.GetKey (KeyCode.UpArrow)) {
			Debug.Log ("Firing Right");
			this.CurrentlyActive = true;
		}

		// Pulling back?
		if (LeftTentacle && Input.GetKey (KeyCode.S)) {
			Debug.Log ("Withdrawing Left");
			this.CurrentlyActive = false;
		}
		if (!LeftTentacle && Input.GetKey (KeyCode.DownArrow)) {
			Debug.Log ("Withdrawing Right");
			this.CurrentlyActive = false;
		}
	}

	/// <summary>
	/// Rotates the tentacle.
	/// </summary>
	/// <param name="rotateLeft">If set to <c>true</c> rotate left.</param>
	void rotateTentacle (bool rotateLeft)
	{
		float rotationValue = rotateLeft ? 1f : -1f;
		Vector3 rotationDirection = new Vector3 (0f, 0f, rotationValue);

		bool allowRotation = false;
		if (LeftTentacle) {
			if (rotateLeft) {
				allowRotation = transform.rotation.eulerAngles.z < MaxDegreesOffVert;
			} else {
				allowRotation = transform.rotation.eulerAngles.z > MinDegreesOffVert;
			}
		} else {
			if (rotateLeft) {
				allowRotation = transform.rotation.eulerAngles.z < 360 - MinDegreesOffVert;
			} else {
				allowRotation = transform.rotation.eulerAngles.z > 360 - MaxDegreesOffVert;
			}
		}

		if (allowRotation) {
			transform.Rotate (rotationDirection, TentacleController.DegreeChangePerUpdate);
		}
	}
}
