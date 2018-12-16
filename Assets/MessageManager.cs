using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{

	public Transform MessagePrefab;
	public float Range = 100f;

	[HideInInspector] public static List<Transform> VisibleMessages = new List<Transform>();

	private float _time = 0;
	private float _maxRefreshtime = 10;

	void Update()
	{
		_time -= Time.deltaTime;
		if (_time < 0)
		{
			RefreshMessages();
		}
	}

	public void RefreshMessages()
	{
		_time = _maxRefreshtime;

		MessageService.GetMessagesAroundAsync(GeoLocation.Latitude, GeoLocation.Longitude, Range, msgs =>
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
		WorldMessage textMessage = transf.GetComponent<WorldMessage>();
		textMessage.SetMessage(data);

		VisibleMessages.Add(transf);
	}

}