using UnityEngine;

public class DumpButton : MonoBehaviour
{

    public Chart Chart;
    public SpriteRenderer SpriteRenderer;
    
    private void OnMouseUpAsButton()
    {
        Chart.Dump();
        SpriteRenderer.color = Color.gray;
    }
}
