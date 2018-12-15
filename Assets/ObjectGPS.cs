using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGPS : MonoBehaviour {

	public float Latitude = 0;
	public float Longitude = 0;
	public float Altitude = 0;

	private static int _currentObject_i = 0;

	public static Vector3 FocusPosition;

	[HideInInspector] public Vector3 RoomPosition = Vector3.zero;

	private void Awake()
	{
		FocusPosition = transform.position;
	}

	private void Update()
	{
		if (_currentObject_i >= MessageManager.VisibleMessages.Count) _currentObject_i = 0;

		RoomPosition = RelativePosition();
		//transform.rotation = Quaternion.identity;
		FocusPosition = RelativePosition();

		//print(transform.position);

		_currentObject_i++;
	}

	public Vector3 RelativePosition()
	{
		Vector3 startPos = new Vector3(0, -3, 5);
		Vector3 pos = startPos + Utils.SpiralDistribution(_currentObject_i, 5, 0.3f, 40);
		return pos;

		//Old:
		double b = Utils.EarthRadius + Altitude;
		double c = Utils.EarthRadius + GeoLocation.Altitude;

		double b2 = b * b;
		double c2 = c * c;
		double bc2 = 2 * b * c;

		// Longitudinal calculations
		double alpha = Longitude - GeoLocation.Longitude;
		// Conversion to radian
		alpha = alpha * Mathf.PI / 180;
		// Small-angle approximation
		double cos = 1 - alpha * alpha / 2; //Math.cos(alpha);
											// Use the law of cosines / Al Kashi theorem
		double x = System.Math.Sqrt(b2 + c2 - bc2 * cos);


		// Repeat for latitudinal calculations
		alpha = Latitude - GeoLocation.Latitude;
		alpha = alpha * Mathf.PI / 180;

		double cos_new = 1 - alpha * alpha / 2; //Math.cos(alpha);
		double z = System.Math.Sqrt(b2 + c2 - bc2 * cos_new);

		// Obtain vertical difference, too
		double y = Altitude - GeoLocation.Altitude;

		return new Vector3((float) x, (float) y, (float) z);
	}

	

}
