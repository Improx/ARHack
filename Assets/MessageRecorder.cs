using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageRecorder : MonoBehaviour
{
	[SerializeField]
	private TMP_InputField _inputField;

	public static MessageRecorder Instance;

	public MessageData CurrentMessage;
	private MessageManager _messageManager;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		_messageManager = FindObjectOfType<MessageManager>();
	}

	// Called from record button
	public void InitializeNewMessage()
	{
		CurrentMessage = new MessageData();
		CurrentMessage.location = new Location
		{
			coordinates = new float[] { GeoLocation.Latitude, GeoLocation.Longitude }
		};
		CurrentMessage.altitude = GeoLocation.Altitude;
	}

	public void SubmitText()
	{
		CurrentMessage.text = _inputField.text;
	}

	public void Send()
	{
		SubmitText();

		_inputField.text = string.Empty;

		print("Sending message");
		MessageService.SaveMessage(CurrentMessage, () =>
		{
			_messageManager.RefreshMessages();
		});
		print("Message sent");
	}
}