using UnityEngine;
using UnityEngine.UI;

public class SplashProgressBarGenerator : MonoBehaviour
{
    [Header("Segment Settings")]
    public int segmentCount = 10;
    public Vector2 segmentSize = new Vector2(40, 8);
    public float spacing = 4f;
    
    [Header("Visual Settings")]
    public Sprite segmentSprite;
    public Color activeColor = new Color(0.62f, 0.31f, 0.87f, 1f); // Purple glow
    public Color inactiveColor = new Color(0.15f, 0.15f, 0.15f, 0.5f);
    public float cornerRadius = 2f;
    
    [Header("Parent")]
    public RectTransform container;
    
    void Start()
    {
        GenerateSegments();
    }
    
    void GenerateSegments()
    {
        if (container == null)
        {
            container = GetComponent<RectTransform>();
        }
        
        // Clear existing children
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        
        // Calculate total width
        float totalWidth = (segmentSize.x * segmentCount) + (spacing * (segmentCount - 1));
        float startX = -(totalWidth / 2) + (segmentSize.x / 2);
        
        // Create segments
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject segment = new GameObject($"Segment_{i}", typeof(RectTransform), typeof(Image));
            segment.transform.SetParent(container, false);
            
            RectTransform rect = segment.GetComponent<RectTransform>();
            rect.sizeDelta = segmentSize;
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(startX + (i * (segmentSize.x + spacing)), 0);
            
            Image img = segment.GetComponent<Image>();
            img.sprite = segmentSprite;
            img.type = Image.Type.Sliced;
            img.color = inactiveColor;
            img.pixelsPerUnitMultiplier = cornerRadius;
        }
        
        Debug.Log($"Generated {segmentCount} progress bar segments");
    }
    
    // Call this to update progress (0.0 to 1.0)
    public void UpdateProgress(float progress)
    {
        int activeSegments = Mathf.FloorToInt(progress * segmentCount);
        
        for (int i = 0; i < container.childCount; i++)
        {
            Image img = container.GetChild(i).GetComponent<Image>();
            if (img != null)
            {
                img.color = (i < activeSegments) ? activeColor : inactiveColor;
            }
        }
    }
}
