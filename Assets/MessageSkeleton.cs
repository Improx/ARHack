using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSkeleton : MonoBehaviour {

	public Transform Neck;
	public Transform Right_Shoulder;
	public Transform Right_Elbow;
	public Transform Left_Shoulder;
	public Transform Left_Elbow;
	public Transform Right_Hip;
	public Transform Right_Knee;
	public Transform Left_Hip;
	public Transform Left_Knee;
	public Transform Body_Center;

	[HideInInspector] public SkeletonAnimation Anim;
	private int _currentFrame = 0;

	private void Update()
	{
		if (Anim != null)
		{
			_currentFrame++;
			if (_currentFrame >= Anim.Frames.Count) _currentFrame = 0;
			//TODO: Other bones
			Frame frame = Anim.Frames[_currentFrame];
			Left_Elbow.transform.position = transform.position + frame.Positions[6];
			Left_Elbow.transform.rotation = frame.WorldRotations[6];
		}
	}
}
