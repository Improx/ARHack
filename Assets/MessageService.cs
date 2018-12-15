using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MessageService
{
	public const string ServerUrl = "http://worldmessage.serveo.net/messages";

	public static async void SaveMessage(MessageData msg)
	{
		WWWForm form = new WWWForm();
		form.AddField("text", msg.text);
		form.AddField("latitude", msg.location.coordinates[0].ToString());
		form.AddField("longitude", msg.location.coordinates[1].ToString());
		form.AddField("altitude", msg.altitude.ToString());

		string j = "{\"frames\":[";
		foreach (FrameData frame in msg.animation)
		{
			j += "{\"bones\":[";
			for (int i = 0; i < frame.bones.Length; i++)
			{
				BoneTransform bone = frame.bones[i];
				string bonej = bone.ToJson();
				j += bonej;

				// Don't add comma at end
				if (i != frame.bones.Length - 1)
				{
					j += ",";
				}
			}
			j += "]}";
		}
		j += "]}";
		Debug.Log(j);
		form.AddField("animation", j);

		using(UnityWebRequest www = UnityWebRequest.Post(ServerUrl, form))
		{
			Debug.Log("saving");
			await www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
			}

			Debug.Log(www.downloadHandler.text);
		}
	}

	public static async void GetMessagesAroundAsync(
		LocationInfo coordinates,
		float radius,
		Action<MessageData[]> onDoneCallback = null)
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
				MessageServiceResponse data = JsonUtility.FromJson<MessageServiceResponse>(www.downloadHandler.text);
				Debug.Log("First result: " + data.result[0]);

				// Or retrieve results as binary data
				byte[] results = www.downloadHandler.data;

				onDoneCallback?.Invoke(data.result);
			}
		}
	}
}