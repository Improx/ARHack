using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuaweiARUnitySDK;
using Common;
using HuaweiARInternal;

public class RecordAnimation : MonoBehaviour {

	public static SkeletonAnimation Anim;
	public static bool Recording = false;

	public static int NumBones = 15;

	private static List<ARBody> VisibleBodies = new List<ARBody>();

	private void Update()
	{
		if (Recording) Record();
	}

	private void Record()
	{
		VisibleBodies.Clear();
		ARFrame.GetTrackables<ARBody>(VisibleBodies, ARTrackableQueryFilter.ALL);
		//If see skeleton:
		if (VisibleBodies.Count > 0)
		{
			Anim.AddFrame();
			ARBody body = VisibleBodies[0];
			//Here, Skeletons = points
			Dictionary<ARBody.SkeletonPointName, ARBody.SkeletonPointEntry> skeletons = new Dictionary<ARBody.SkeletonPointName, ARBody.SkeletonPointEntry>();
			body.GetSkeletons(skeletons);

			//TODO: other bones
			Vector3 leftElbowPos = skeletons[ARBody.SkeletonPointName.Left_Elbow].Coordinate3D;
			Vector3 leftWristPos = skeletons[ARBody.SkeletonPointName.Left_Wrist].Coordinate3D;

			Vector3 leftElbowUp = (leftWristPos - leftElbowPos).normalized;
			Vector3 forward = Utils.PerpVectorRight(leftElbowUp);
			Quaternion leftElbowWorldRot = Quaternion.LookRotation(forward, leftElbowUp);

			Anim.LastFrame.Positions[6] = leftElbowPos;
			Anim.LastFrame.WorldRotations[6] = leftElbowWorldRot;
		}

	}

	public static void StartRecording()
	{
		Anim = new SkeletonAnimation();
		Recording = true;
	}


	public static void StopRecording()
	{
		Recording = false;
		SendLastAnimation();
	}

	public static void SendLastAnimation()
	{
		for (int i = 0; i < Anim.Frames.Count; i++)
		{

		}
	}
}

public class SkeletonAnimation
{
	public List<Frame> Frames = new List<Frame>();
	public Frame LastFrame { get { return Frames[Frames.Count - 1]; } }

	public void AddFrame() { Frames.Add(new Frame()); }

}

public class Frame
{
	public Vector3[] Positions = new Vector3[RecordAnimation.NumBones]; //In reference frame of captured room
	public Quaternion[] WorldRotations = new Quaternion[RecordAnimation.NumBones];

	public Frame()
	{
		for (int i = 0; i < RecordAnimation.NumBones; i++)
		{
			WorldRotations[i] = Quaternion.identity;
		}
	}
}
