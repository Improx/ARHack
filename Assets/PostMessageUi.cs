using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostMessageUi : MonoBehaviour
{
	public static PostMessageUi Instance;
	private CanvasGroup _canvasGroup;

	private void Awake()
	{
		Instance = this;
		_canvasGroup = GetComponent<CanvasGroup>();
	}

	public void Show()
	{
		_canvasGroup.alpha = 1f;
		_canvasGroup.blocksRaycasts = true;
		_canvasGroup.interactable = true;
	}

	public void Hide()
	{
		_canvasGroup.alpha = 0f;
		_canvasGroup.blocksRaycasts = false;
		_canvasGroup.interactable = false;
	}
}