using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RecordAnimationButton : MonoBehaviour
{
	[SerializeField]
	private Sprite _recordingSprite;
	[SerializeField]
	private Sprite _stopSprite;
	private Image _iconImage;
	private CanvasGroup _canvasGroup;
	private bool _recording = false;

	public UnityEvent OnStartRecording;
	public UnityEvent OnStopRecording;

	private void Start()
	{
		_iconImage = GetComponent<Image>();
		_canvasGroup = GetComponent<CanvasGroup>();
	}

	public void Show()
	{
		_canvasGroup.alpha = 1f;
		_canvasGroup.interactable = true;
		_canvasGroup.blocksRaycasts = true;
	}

	public void Hide()
	{
		_canvasGroup.alpha = 0f;
		_canvasGroup.interactable = false;
		_canvasGroup.blocksRaycasts = false;
	}

	public void ToggleRecordingState()
	{
		_recording = !_recording;

		if (_recording)
		{
			SetSpriteStop();
			OnStopRecording.Invoke();
		}
		else
		{
			SetSpriteRecord();
			OnStartRecording.Invoke();
		}
	}

	private void SetSpriteRecord()
	{
		_iconImage.sprite = _recordingSprite;
	}

	private void SetSpriteStop()
	{
		_iconImage.sprite = _stopSprite;
	}
}