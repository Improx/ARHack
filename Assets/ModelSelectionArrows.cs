using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModelSelectionArrows : MonoBehaviour
{
	[SerializeField]
	private List<Sprite> _thumbNails;
	[SerializeField]
	private Image _thumbnailImage;
	[SerializeField]
	private Button _prevButton;
	[SerializeField]
	private Button _nextButton;

	private CanvasGroup _canvasGroup;

	public UnityEvent OnModelIndexChanged;

	private void Start()
	{
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

	public void IncrementModelIndex()
	{
		// TODO: Replace with actual count
		int modelCount = 3;
		int newId = MessageRecorder.Instance.CurrentMessage.modelId + 1;

		MessageRecorder.Instance.CurrentMessage.modelId = newId >= modelCount ? 0 : newId;

		UpdateThumbnail();

		OnModelIndexChanged.Invoke();
	}

	public void DecrementModelIndex()
	{
		// TODO: Replace with actual count
		int modelCount = 3;
		int newId = MessageRecorder.Instance.CurrentMessage.modelId - 1;

		MessageRecorder.Instance.CurrentMessage.modelId = newId < 0 ? modelCount - 1 : newId;

		UpdateThumbnail();

		OnModelIndexChanged.Invoke();
	}

	private void UpdateThumbnail()
	{
		_thumbnailImage.sprite = _thumbNails[MessageRecorder.Instance.CurrentMessage.modelId];
	}
}