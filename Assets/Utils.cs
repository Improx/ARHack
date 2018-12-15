using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

	//TODO: Radius varies in reality. For more exact esults, find Earth's radius in Espoo
	public static float EarthRadius = 6371000f;

	public static float DistanceGPS(LocationInfo gps1, LocationInfo gps2)
	{
		var lat1 = gps1.latitude * Mathf.Deg2Rad;
		var lat2 = gps2.latitude * Mathf.Deg2Rad;
		var delta_lat = lat2 - lat1;

		var long_1 = gps1.longitude * Mathf.Deg2Rad;
		var long_2 = gps2.longitude * Mathf.Deg2Rad;
		var delta_long = long_2 - long_1;

		var a = Mathf.Sin(delta_lat / 2) * Mathf.Sin(delta_lat / 2) +
				Mathf.Cos(lat1) * Mathf.Cos(lat2) *
				Mathf.Sin(delta_long / 2) * Mathf.Sin(delta_long / 2);
		var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

		var distance = EarthRadius * c;
		return distance;
	}

	public static Vector3 PerpVectorRight(Vector3 vec)
	{
		Vector3 auxVector = new Vector3(vec.x, vec.y, -vec.z) + Vector3.right;

		Vector3 result = Vector3.Cross(vec, auxVector);

		return result.normalized;
	}

	public static Vector3 FlipY (this Vector3 vec)
	{
		return new Vector3(vec.x, -vec.y, vec.z);
	}

	public static Vector3 FlipZ(this Vector3 vec)
	{
		return new Vector3(vec.x, vec.y, -vec.z);
	}

	public static Vector3 SpiralDistribution(int sample, float startRadius = 1, float radiusGrowth = 0.2f, float angleGrowth = 30f)
	{
		float angle = sample * angleGrowth;
		float radius = startRadius + sample * radiusGrowth;
		return radius * new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));
	}

}
