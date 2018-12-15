using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{

	public Transform MessagePrefab;

	[HideInInspector] public static List<Transform> VisibleMessages = new List<Transform>();

	private float _time = 0;
	private float _maxRefreshtime = 10;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
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
		WorldMessage textMessage = transf.GetComponent<WorldMessage>();
		textMessage.SetMessage(data);

		VisibleMessages.Add(transf);
	}

}