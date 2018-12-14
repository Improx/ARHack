using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoOrientation : MonoBehaviour {

	public static GeoOrientation Instance;
	public static Vector3 MagnetNormVec = new Vector3(1,0,0);
	public static Quaternion Rotation = Quaternion.identity;

	private Compass _compass;
	private Gyroscope _gyro;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}

	private void Start()
	{
		_gyro = Input.gyro;
		_gyro.enabled = true;

		_compass = Input.compass;
		_compass.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		MagnetNormVec = _compass.rawVector.normalized;

		//Determine Rotation
		//const float maxX = 0.5

		//Rotation = Quaternion.FromToRotation(Input.gyro.gravity.normalized, MagnetNormVec);
		Vector2 Magnet2D = new Vector2(GeoOrientation.MagnetNormVec.x, GeoOrientation.MagnetNormVec.z).normalized;
		Vector3 localUp = -_gyro.gravity.normalized;

		Vector3 planarMagnet = MagnetNormVec;
		planarMagnet -= localUp * Vector3.Dot(localUp, planarMagnet);
		planarMagnet = planarMagnet.normalized;

		//float angle_x = (Mathf.Atan2(Magnet2D.x, Magnet2D.y) - Mathf.PI ) * Mathf.Rad2Deg;


		float angle_x = Vector3.SignedAngle(Vector3.back, planarMagnet, localUp);
		float angle_y = Vector3.SignedAngle(Vector3.down, -localUp, Vector3.right);

		Rotation = Quaternion.AngleAxis(angle_x, Vector3.up) * Quaternion.AngleAxis(angle_y, Vector3.right);
	}

	public static Vector3 GetGravity()
	{
		return Instance._gyro.gravity.normalized;
	}



}
