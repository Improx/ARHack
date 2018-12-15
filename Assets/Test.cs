using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	private void Start()
	{
		print("start");
		Input.location.Start();
		MessageService.GetMessagesAroundAsync(Input.location.lastData, 5f);

		// MessageService.SaveMessage(
		// 	new MessageData
		// 	{
		// 		text = "Moi",
		// 			location = new Location
		// 			{
		// 				coordinates = new float[] { 1, 2 }
		// 			},
		// 			altitude = 300,
		// 			animation = new BoneTransform[][]
		// 			{
		// 				new BoneTransform[]
		// 				{
		// 					new BoneTransform
		// 					{
		// 						rotation = new float[] { 1f, 1f, 1f, 1f },
		// 							position = new float[] { 1f, 1f, 1f },
		// 					}
		// 				}
		// 			}
		// 	});
	}
}