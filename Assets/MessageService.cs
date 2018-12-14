using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MessageService
{
	public const string ServerUrl = "http://www.my-server.com";

	public static async void GetMessagesAroundAsync(LocationInfo coordinates, float radius)
	{
		using(UnityWebRequest www = UnityWebRequest.Get(ServerUrl))
		{
			Debug.Log("call");
			await www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
			}
			else
			{
				// Show results as text
				Debug.Log(www.downloadHandler.text);

				// TODO: Check this
				JsonUtility.FromJson<MessageData[]>(www.downloadHandler.text);

				// Or retrieve results as binary data
				byte[] results = www.downloadHandler.data;
			}
		}
	}
}