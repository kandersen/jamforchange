using System.Collections;
using JFCUtil.Game;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameClass
{
    private bool _dumped;
    private int _value;
    private int _score;

    public Cat Cat;
    public AudioSource WhompSound;
    private Transform _cameraTransform;
    
    public void Awake()
    {
        _dumped = false;
        _cameraTransform = FindObjectOfType<Camera>().transform;
    }
	
    public int GetScoreOutOfOneHundred()
    {
        return _score;
    }

    public void AffectScore(int affect)
    {
        Debug.LogWarning("Affect Score Called -- Not implemented");
    }

    public void DumpedAt(int dumpedAt)
    {
        _dumped = true;
        _value = dumpedAt;
        Cat.Startle();
        ScreenShake();
        WhompSound.Play();
    }

    private void ScreenShake()
    {
        StartCoroutine(ShakeCamera());
    }

    private IEnumerator ShakeCamera()
    {
        var shakeDuration = 0.1f;
        const float shakeAmount = 1.0f;
        var originalPos = _cameraTransform.localPosition;
        while (shakeDuration > 0)
        {
            _cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime;
            yield return null;
        }

        _cameraTransform.localPosition = originalPos;
    }    

    public void FinishedAt(int final, int maxPossible, int minPossible)
    {
        if (!_dumped)
        {
            _value = final;
        }
        float span = maxPossible - minPossible;
        float diff = maxPossible - _value;
        _score = Mathf.CeilToInt( ((span - diff) / span) * 100f );
    }
}