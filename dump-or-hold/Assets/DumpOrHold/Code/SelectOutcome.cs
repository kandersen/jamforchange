using UnityEngine;
using UnityEngine.UI;

public class SelectOutcome : MonoBehaviour
{

	public Sprite WinScreen;
	public Sprite LoseScreen;
	public Image Image;


	private void Awake()
	{
		var score = FindObjectOfType<GameManager>().GetScoreOutOfOneHundred();
		Image.sprite = score >= 50 ? WinScreen : LoseScreen;
	}
}
