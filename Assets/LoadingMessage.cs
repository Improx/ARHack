using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingMessage : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _loadingText;

	public static LoadingMessage Instance;

	private void Awake()
	{
		Instance = this;
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