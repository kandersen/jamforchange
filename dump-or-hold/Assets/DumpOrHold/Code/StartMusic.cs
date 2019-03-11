using UnityEngine;

public class StartMusic : MonoBehaviour
{

	public AudioSource MusicPlayerPrefab;
	
	void Awake ()
	{
		var player = Instantiate(MusicPlayerPrefab);
		player.time = (60f / 128f) * 2f;
		player.Play();
		
	}
	
}
