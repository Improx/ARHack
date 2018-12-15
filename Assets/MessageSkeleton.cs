using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuaweiARInternal;
using HuaweiARUnitySDK;

public class MessageSkeleton : MonoBehaviour {

	public Transform Head_Top;
	public Transform Neck;
	public Transform Right_Shoulder;
	public Transform Right_Elbow;
	public Transform Right_Wrist;
	public Transform Left_Shoulder;
	public Transform Left_Elbow;
	public Transform Left_Wrist;
	public Transform Right_Hip;
	public Transform Right_Knee;
	public Transform Right_Ankle;
	public Transform Left_Hip;
	public Transform Left_Knee;
	public Transform Left_Ankle;
	public Transform Body_Center;

	[HideInInspector] public SkeletonAnimation Anim = null;
	private int _currentFrame = 0;

	private void Update()
	{
		if (Anim != null)
		{
			_currentFrame++;
			if (_currentFrame >= Anim.Frames.Count) _currentFrame = 0;
			
			Frame frame = Anim.Frames[_currentFrame];



			//Actually transform the bones
			

			Neck.transform.position = transform.position + frame.Positions[1];
			//Neck.transform.rotation = frame.WorldRotations[1];

			Right_Shoulder.transform.position = transform.position + frame.Positions[2];
			Right_Shoulder.transform.rotation = frame.WorldRotations[2];

			Right_Elbow.transform.position = transform.position + frame.Positions[3];
			Right_Elbow.transform.rotation = frame.WorldRotations[3];

			Left_Shoulder.transform.position = transform.position + frame.Positions[5];
			Left_Shoulder.transform.rotation = frame.WorldRotations[5];

			Left_Elbow.transform.position = transform.position + frame.Positions[6];
			Left_Elbow.transform.rotation = frame.WorldRotations[6];

			Right_Hip.transform.position = transform.position + frame.Positions[8];
			Right_Hip.transform.rotation = frame.WorldRotations[8];

			Right_Knee.transform.position = transform.position + frame.Positions[9];
			Right_Knee.transform.rotation = frame.WorldRotations[9];

			Left_Hip.transform.position = transform.position + frame.Positions[11];
			Left_Hip.transform.rotation = frame.WorldRotations[11];

			Left_Knee.transform.position = transform.position + frame.Positions[12];
			Left_Knee.transform.rotation = frame.WorldRotations[12];

			Body_Center.transform.position = (Right_Hip.transform.position + Left_Hip.transform.position) / 2 + 0.2f * Vector3.up;
			//Body_Center.transform.rotation = frame.WorldRotations[14];
			//Body is the only whose up is not really up. It is back
			Vector3 bodyForward = -(Neck.transform.position - Body_Center.transform.position).normalized;
			Vector3 bodyright = -(Right_Shoulder.transform.position - Left_Shoulder.transform.position).normalized;
			Vector3 bodyUp = Vector3.Cross(bodyright, bodyForward);
			Body_Center.transform.rotation = Quaternion.LookRotation(bodyForward, bodyUp);

			//Neck.transform.rotation = Quaternion.LookRotation(bodyForward, bodyUp);
		}

		//Set whole objects position
		ObjectGPS objectGPS = GetComponentInParent<ObjectGPS>();
		objectGPS.transform.position = objectGPS.RoomPosition;
		objectGPS.transform.rotation = Quaternion.LookRotation(-transform.position.normalized, Vector3.up);
	}
}
