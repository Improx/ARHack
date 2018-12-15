using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSkeleton : MonoBehaviour {

	public Transform LeftElbow;

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
			LeftElbow.transform.position = transform.position + frame.Positions[6];
			LeftElbow.transform.rotation = frame.WorldRotations[6];
		}
	}
}
