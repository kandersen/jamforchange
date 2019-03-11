using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chart : MonoBehaviour
{
    private const int Points = 70;
    private const int OriginX = 1;
    private const int OriginY = 20;     
    private const int Ceiling = 19;
    private const int Floor = -19;
    private const int Height = 2 * Ceiling + 1;


    
    public SpriteRenderer Renderer;
    public Sprite BaseSprite;
    private Texture2D _texture;

    private Coroutine _animation;

    public GameManager Manager;
    
    private int[] _values;
    private int _currentFrame;
    private bool _hasDumped;
    
    public void Awake()
    {
        _hasDumped = false;
        RandomizeCurve();
        InitiateBlankGraph();
        _animation = StartCoroutine(DrawChart());
    }

    private void RandomizeCurve()
    {
        _values = new int[Points];
        _values[0] = 0; //Random.Range(Floor, Ceiling + 1);
        for (var i = 1; i < _values.Length; i++)
        {
            _values[i] = _values[i - 1] + Random.Range(-1, 2);            
            if (_values[i] > Ceiling)
            {
                _values[i] = Ceiling;
            }
            
            if (_values[i] < Floor)
            {
                _values[i] = Floor;
            }
        }
    }

    private void InitiateBlankGraph()
    {
        var newTexture2D = Instantiate(BaseSprite.texture);
        Renderer.sprite = Sprite.Create(newTexture2D, BaseSprite.rect, new Vector2(0.5f, 0.5f), BaseSprite.pixelsPerUnit, 1, SpriteMeshType.Tight);
    }

    private IEnumerator DrawChart()
    {
        const float totalDuration = 3f;
        var frameTime = totalDuration / _values.Length; 

        _currentFrame = 0;

        while (_currentFrame < _values.Length)
        {            
            Renderer.sprite.texture.SetPixel(OriginX + _currentFrame, OriginY + _values[_currentFrame], _values[_currentFrame] > 0 ? new Color(130f / 255f, 161f / 255f, 124f / 255f) : new Color(153f / 255f, 46f / 255f, 83f / 255f));
            Renderer.sprite.texture.Apply();
            yield return new WaitForSeconds(frameTime);                
            _currentFrame++;
        }

        Manager.FinishedAt(_values[_currentFrame - 1], _values.Max(), _values.Min());
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Reset();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Dump();
        }
    }

    private bool IsDone()
    {
        return _currentFrame == Points;
    }
    
    public void Dump()
    {
        if (_hasDumped || IsDone()) return;        
        _hasDumped = true;
        var heldAt = _values[_currentFrame];
//        for (var i = 0; i < Points; i++)
//        {
//            Renderer.sprite.texture.SetPixel(OriginX + i, OriginY + heldAt, Color.white);
//        }
        for (var i = 0; i < 3; i++)
        {
            Renderer.sprite.texture.SetPixel(OriginX + _currentFrame, OriginY - 1 + i, new Color(92 / 255f, 102 / 255f, 132 / 255f));
        }
        Renderer.sprite.texture.Apply();
        Manager.DumpedAt(heldAt);
    }

    private void Reset()
    {
        StopCoroutine(_animation);
        InitiateBlankGraph();
        RandomizeCurve();
        _hasDumped = false;
        _animation = StartCoroutine(DrawChart());
    }
}