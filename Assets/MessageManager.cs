using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour {

	public Transform MessagePrefab;

	[HideInInspector] public static List<Transform> VisibleMessages = new List<Transform>();

	private float _time = 0;
	private float _maxRefreshtime = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		_time -= Time.deltaTime;
		if (_time < 0)
		{
			RefreshMessages();
			_time = _maxRefreshtime;
		}
	}

	public void RefreshMessages()
	{
		MessageService.GetMessagesAroundAsync(GeoLocation.Latitude, GeoLocation.Longitude, 50f, msgs =>
		{
			foreach (var transf in VisibleMessages)
			{
				Destroy(transf.gameObject);
			}
			VisibleMessages.Clear();

			foreach (var message in msgs)
			{
				SpawnMessage(message);
			}
		});
		print("Number of messages fetched: " + VisibleMessages.Count);
	}

	private void SpawnMessage(MessageData data)
	{
		Transform transf = Instantiate(MessagePrefab);
		WorldTextMessage textMessage = transf.GetComponent<WorldTextMessage>();
		//Text
		textMessage.SetText(data.text);
		//Position
		ObjectGPS object_gps = transf.GetComponent<ObjectGPS>();
		object_gps.Latitude = data.location.coordinates[0];
		object_gps.Longitude = data.location.coordinates[1];
		object_gps.Altitude = data.altitude;
		//Animation

		SkeletonAnimation anim = new SkeletonAnimation();
		for (int i = 0; i < data.animation.Length; i++)
		{
			anim.AddFrame();
			for (int j = 0; j < RecordAnimation.NumBones; j++)
			{
				anim.LastFrame.Positions[j] = data.animation[i].bones[j].Position;
				anim.LastFrame.WorldRotations[j] = data.animation[i].bones[j].Rotation;
			}
		}

		MessageSkeleton skeleton = transf.GetComponentInChildren<MessageSkeleton>();
		skeleton.Anim = anim;

		VisibleMessages.Add(transf);
	}


}
