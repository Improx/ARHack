using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGPS : MonoBehaviour {

	public float Latitude = 0;
	public float Longitude = 0;
	public float Altitude = 0;

	public static Vector3 FocusPosition;

	private void Awake()
	{
		FocusPosition = transform.position;
	}

	private void Update()
	{
		transform.position = RelativePosition();
		FocusPosition = RelativePosition();
		print(transform.position);
	}

	public Vector3 RelativePosition()
	{
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
