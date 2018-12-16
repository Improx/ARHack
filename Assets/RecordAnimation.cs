using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using HuaweiARInternal;
using HuaweiARUnitySDK;
using UnityEngine;

public class RecordAnimation : MonoBehaviour
{

	public static SkeletonAnimation Anim;
	public static bool Recording = false;

	public static int NumBones = 15;

	private static List<ARBody> VisibleBodies = new List<ARBody>();

	private void Update()
	{
		if (Recording)
		{
			Anim.Frames.Add(RecordFrame());
		}
	}

	public Frame RecordFrame()
	{
		VisibleBodies.Clear();
		ARFrame.GetTrackables<ARBody>(VisibleBodies, ARTrackableQueryFilter.ALL);

		Frame frame = new Frame();
		//If see skeleton, add bones to frame
		if (VisibleBodies.Count > 0)
		{
			ARBody body = VisibleBodies[0];
			//Here, Skeletons = points
			var skeletons = new Dictionary<ARBody.SkeletonPointName, ARBody.SkeletonPointEntry>();
			body.GetSkeletons(skeletons);

			foreach (var point in skeletons)
			{
				if (!point.Value.Is3DValid) return frame;
			}

			//Getting bone positions
			Vector3 neckPos = skeletons[ARBody.SkeletonPointName.Neck].Coordinate3D.FlipY().FlipZ();
			Vector3 headPos = skeletons[ARBody.SkeletonPointName.Head_Top].Coordinate3D.FlipY().FlipZ();
			Vector3 bodyCenterPos = skeletons[ARBody.SkeletonPointName.Body_Center].Coordinate3D.FlipY().FlipZ();

			Vector3 leftShoulderPos = skeletons[ARBody.SkeletonPointName.Left_Shoulder].Coordinate3D.FlipY().FlipZ();
			Vector3 leftElbowPos = skeletons[ARBody.SkeletonPointName.Left_Elbow].Coordinate3D.FlipY().FlipZ();
			Vector3 leftWristPos = skeletons[ARBody.SkeletonPointName.Left_Wrist].Coordinate3D.FlipY().FlipZ();

			Vector3 rightShoulderPos = skeletons[ARBody.SkeletonPointName.Right_Shoulder].Coordinate3D.FlipY().FlipZ();
			Vector3 rightElbowPos = skeletons[ARBody.SkeletonPointName.Right_Elbow].Coordinate3D.FlipY().FlipZ();
			Vector3 rightWristPos = skeletons[ARBody.SkeletonPointName.Right_Wrist].Coordinate3D.FlipY().FlipZ();

			Vector3 leftHipPos = skeletons[ARBody.SkeletonPointName.Left_Hip].Coordinate3D.FlipY().FlipZ();
			Vector3 leftKneePos = skeletons[ARBody.SkeletonPointName.Left_Knee].Coordinate3D.FlipY().FlipZ();
			Vector3 leftAnklePos = skeletons[ARBody.SkeletonPointName.Left_Ankle].Coordinate3D.FlipY().FlipZ();

			Vector3 rightHipPos = skeletons[ARBody.SkeletonPointName.Right_Hip].Coordinate3D.FlipY().FlipZ();
			Vector3 rightKneePos = skeletons[ARBody.SkeletonPointName.Right_Knee].Coordinate3D.FlipY().FlipZ();
			Vector3 rightAnklePos = skeletons[ARBody.SkeletonPointName.Right_Ankle].Coordinate3D.FlipY().FlipZ();

			//Actually set bone positions and rotationg in current frame
			SetBoneToFrame(frame, 14, bodyCenterPos, neckPos);
			SetBoneToFrame(frame, 1, neckPos, headPos);
			SetBoneToFrame(frame, 2, rightShoulderPos, rightElbowPos);
			SetBoneToFrame(frame, 3, rightElbowPos, rightWristPos);
			SetBoneToFrame(frame, 5, leftShoulderPos, leftElbowPos);
			SetBoneToFrame(frame, 6, leftElbowPos, leftWristPos);
			SetBoneToFrame(frame, 8, rightHipPos, rightKneePos);
			SetBoneToFrame(frame, 9, rightKneePos, rightAnklePos);
			SetBoneToFrame(frame, 11, leftHipPos, leftKneePos);
			SetBoneToFrame(frame, 12, leftKneePos, leftAnklePos);
		}
		return frame;
	}

	private void SetBoneToFrame(Frame frame, int index, Vector3 thisBonePos, Vector3 childBonePos)
	{
		Vector3 boneUp = (childBonePos - thisBonePos).normalized;
		if (boneUp.magnitude < 0.05f) boneUp = Vector3.up;
		Vector3 forward = Utils.PerpVectorRight(boneUp);
		Quaternion worldRot = Quaternion.LookRotation(forward, boneUp);

		frame.Positions[index] = thisBonePos;
		frame.WorldRotations[index] = worldRot;
	}

	public void StartRecording()
	{
		Anim = new SkeletonAnimation();
		Recording = true;
		print("STARTED RECORDING");
	}

	public void StopRecording()
	{
		Recording = false;
		print("STOPPED RECORDING");
		ApplyLastAnimation();
	}

	public void ApplyLastAnimation()
	{
		//LoadingMessage.Instance.SetLoadingText("Uploading...");

		FrameData[] frames = new FrameData[Anim.Frames.Count];
		print("Frames Num: " + Anim.Frames.Count);
		for (int i = 0; i < Anim.Frames.Count; i++)
		{
			BoneTransform[] frame = new BoneTransform[NumBones];
			for (int j = 0; j < NumBones; j++)
			{
				BoneTransform bone = new BoneTransform();
				Vector3 pos = Anim.Frames[i].Positions[j];
				Quaternion rot = Anim.Frames[i].WorldRotations[j];
				bone.position = new float[] { pos.x, pos.y, pos.z };
				bone.rotation = new float[] { rot.x, rot.y, rot.z, rot.w };
				frame[j] = bone;
			}
			frames[i] = new FrameData { bones = frame };
		}

		MessageRecorder.Instance.CurrentMessage.animation = frames;
		//LoadingMessage.Instance.ClearLoadingText();
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
			Positions[i] = Vector3.zero;
			WorldRotations[i] = Quaternion.identity;
		}
	}
}