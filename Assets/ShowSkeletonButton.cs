using System.Collections;
using System.Collections.Generic;
using BodyARSample;
using UnityEngine;
using UnityEngine.UI;

public class ShowSkeletonButton : MonoBehaviour
{

	[SerializeField]
	private Image _crossImage;
	private BodyARController _bodyDrawer;

	private void Start()
	{
		_bodyDrawer = FindObjectOfType<BodyARController>();
	}

	public void ToggleSkeleton()
	{
		_bodyDrawer.ToggleDrawBody();
		_crossImage.enabled = !_crossImage.enabled;
	}
}