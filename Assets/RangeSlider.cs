using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RangeSlider : MonoBehaviour
{

	[SerializeField]
	private float _minRange = 10f;
	[SerializeField]
	private float _maxRange = 1000f;

	[SerializeField]
	private TextMeshProUGUI _rangeText;

	private Slider _slider;
	private MessageManager _messageManager;

	private void Start()
	{
		_slider = GetComponent<Slider>();
		_messageManager = FindObjectOfType<MessageManager>();
		_slider.onValueChanged.AddListener(newValue => UpdateRange(newValue));

		UpdateRange(_slider.value);
	}

	private void UpdateRange(float newRange)
	{
		float range = Mathf.Lerp(_minRange, _maxRange, newRange);
		_messageManager.Range = range;
		_rangeText.text = Mathf.RoundToInt(range) + "m";
	}
}