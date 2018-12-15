using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingMessage : MonoBehaviour
{
	private TextMeshProUGUI _loadingText;

	public static LoadingMessage Instance;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		_loadingText = GetComponent<TextMeshProUGUI>();
	}

	public void SetLoadingText(string text = "Uploading...")
	{
		_loadingText.text = text;
	}

	public void ClearLoadingText()
	{
		_loadingText.text = string.Empty;
	}
}