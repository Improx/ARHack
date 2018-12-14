using UnityEngine;

[System.Serializable]
public class MessageData
{
	public string _id;
	public Location location;
	public float altitude;
	public string text;

	public override string ToString()
	{
		return $"{location.coordinates[0]},{location.coordinates[1]},{altitude}: {text}";
	}
}

[System.Serializable]
public class Location
{
	public float[] coordinates;
}