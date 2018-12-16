using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldMessage : MonoBehaviour
{

	[SerializeField]
	private TextMeshProUGUI _text;
	[SerializeField]
	private TextMeshProUGUI _scoreText;
	[SerializeField]
	private string _scoreWord = "love";
	private MessageData _data;

	public List<SkinnedMeshRenderer> SkinnedMeshRenderers;

	public SkinnedMeshRenderer manClothes;

	public static int NumModels = 5;

	public void SetMessage(MessageData data)
	{
		_data = data;

		//Text
		_text.text = data.text;
		_scoreText.text = (_data.points).ToString() + _scoreWord;

		//Position
		ObjectGPS object_gps = GetComponent<ObjectGPS>();
		object_gps.Latitude = data.location.coordinates[0];
		object_gps.Longitude = data.location.coordinates[1];
		object_gps.Altitude = data.altitude;

		//Set model
		SetModel(data.modelId);

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
		MessageSkeleton skeleton = GetComponentInChildren<MessageSkeleton>();
		skeleton.Anim = anim;
	}

	public void Upvote()
	{
		MessageService.UpvoteMessage(_data._id);
		_data.points++;
		_scoreText.text = (_data.points).ToString() + _scoreWord;
		print("upvote");
	}

	public void Downvote()
	{
		MessageService.DownvoteMessage(_data._id);
		_data.points--;
		_scoreText.text = (_data.points).ToString() + _scoreWord;
	}

	private void SetModel(int modelIndex = 0)
	{
		manClothes.enabled = modelIndex == 0;
		for (int i = 0; i < NumModels; i++)
		{
			SkinnedMeshRenderers[i].enabled = false;
			if (i == modelIndex) SkinnedMeshRenderers[i].enabled = true;
		}
	}
}