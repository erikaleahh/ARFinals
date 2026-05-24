using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SciFiProgressBar : MonoBehaviour
{
    [Header("Segment Settings")]
    public int segmentCount = 10;
    public float segmentWidth = 30f;
    public float segmentHeight = 8f;
    public float spacing = 2f;
    
    [Header("Colors")]
    public Color activeColor = new Color(0.62f, 0.31f, 0.87f, 1f); // Purple glow
    public Color inactiveColor = new Color(0.1f, 0.1f, 0.1f, 0.8f);
    public Color glowColor = new Color(0.9f, 0.4f, 1f, 0.5f);
    
    [Header("Animation")]
    public bool animateFill = true;
    public float fillDuration = 2f;
    public float shimmerSpeed = 2f;
    
    [Header("Optional")]
    public Image borderFrame; // Optional border image
    public float startDelay = 1f;
    
    private Image[] segments;
    private RectTransform container;
    private float progress = 0f;
    private bool isFilling = false;
    
    void Start()
    {
        container = GetComponent<RectTransform>();
        CreateSegments();
        
        if (animateFill)
        {
            StartCoroutine(AnimateProgress());
        }
    }
    
    void CreateSegments()
    {
        // Clear existing
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Segment"))
                Destroy(child.gameObject);
        }
        
        segments = new Image[segmentCount];
        
        // Calculate dimensions
        float totalWidth = (segmentWidth * segmentCount) + (spacing * (segmentCount - 1));
        float startX = -(totalWidth / 2f) + (segmentWidth / 2f);
        
        // Create each segment
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject seg = new GameObject($"Segment_{i}", typeof(RectTransform), typeof(Image));
            seg.transform.SetParent(transform, false);
            
            RectTransform rect = seg.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(segmentWidth, segmentHeight);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(startX + (i * (segmentWidth + spacing)), 0);
            
            Image img = seg.GetComponent<Image>();
            
            // Create rounded sprite if none assigned
            if (img.sprite == null)
            {
                img.sprite = CreateRoundedSprite();
            }
            
            img.type = Image.Type.Sliced;
            img.fillCenter = true;
            img.color = inactiveColor;
            
            // Add glow effect (optional child image)
            CreateGlowEffect(seg, rect);
            
            segments[i] = img;
        }
        
        Debug.Log($"Created {segmentCount} sci-fi progress bar segments");
    }
    
    void CreateGlowEffect(GameObject parent, RectTransform rect)
    {
        GameObject glow = new GameObject("Glow", typeof(RectTransform), typeof(Image));
        glow.transform.SetParent(parent.transform, false);
        
        RectTransform glowRect = glow.GetComponent<RectTransform>();
        glowRect.sizeDelta = rect.sizeDelta + new Vector2(4, 4);
        glowRect.anchorMin = new Vector2(0.5f, 0.5f);
        glowRect.anchorMax = new Vector2(0.5f, 0.5f);
        glowRect.pivot = new Vector2(0.5f, 0.5f);
        glowRect.anchoredPosition = Vector2.zero;
        
        Image glowImg = glow.GetComponent<Image>();
        glowImg.sprite = parent.GetComponent<Image>().sprite;
        glowImg.type = Image.Type.Sliced;
        glowImg.color = new Color(glowColor.r, glowColor.g, glowColor.b, 0f);
        glowImg.raycastTarget = false;
        
        // Store reference for animation
        glow.name = "GlowEffect";
    }
    
    IEnumerator AnimateProgress()
    {
        yield return new WaitForSeconds(startDelay);
        
        isFilling = true;
        float elapsed = 0f;
        
        while (elapsed < fillDuration)
        {
            elapsed += Time.deltaTime;
            progress = Mathf.Clamp01(elapsed / fillDuration);
            
            UpdateVisuals();
            
            yield return null;
        }
        
        progress = 1f;
        UpdateVisuals();
        isFilling = false;
        
        // Pulse effect when complete
        StartCoroutine(PulseComplete());
    }
    
    void UpdateVisuals()
    {
        int activeCount = Mathf.FloorToInt(progress * segmentCount);
        
        for (int i = 0; i < segmentCount; i++)
        {
            if (segments[i] == null) continue;
            
            bool isActive = i < activeCount;
            
            // Main segment color
            segments[i].color = isActive ? activeColor : inactiveColor;
            
            // Glow effect
            Transform glow = segments[i].transform.Find("GlowEffect");
            if (glow != null)
            {
                Image glowImg = glow.GetComponent<Image>();
                float targetAlpha = isActive ? glowColor.a : 0f;
                
                // Shimmer effect on the leading edge
                if (isActive && i == activeCount - 1 && isFilling)
                {
                    float shimmer = Mathf.PingPong(Time.time * shimmerSpeed, 1f);
                    targetAlpha = Mathf.Lerp(0.3f, 1f, shimmer);
                    
                    // Make the leading segment brighter
                    Color brightColor = Color.Lerp(activeColor, Color.white, 0.3f);
                    segments[i].color = brightColor;
                }
                
                glowImg.color = new Color(glowColor.r, glowColor.g, glowColor.b, targetAlpha);
            }
        }
    }
    
    IEnumerator PulseComplete()
    {
        while (true)
        {
            float pulse = Mathf.PingPong(Time.time * 2f, 0.3f);
            
            for (int i = 0; i < segmentCount; i++)
            {
                if (segments[i] == null) continue;
                
                Color pulseColor = Color.Lerp(activeColor, Color.white, pulse * 0.3f);
                segments[i].color = pulseColor;
                
                Transform glow = segments[i].transform.Find("GlowEffect");
                if (glow != null)
                {
                    Image glowImg = glow.GetComponent<Image>();
                    glowImg.color = new Color(glowColor.r, glowColor.g, glowColor.b, glowColor.a + pulse);
                }
            }
            
            yield return null;
        }
    }
    
    // Call this to set progress manually (0.0 to 1.0)
    public void SetProgress(float value)
    {
        progress = Mathf.Clamp01(value);
        UpdateVisuals();
    }
    
    // Public getter for current progress
    public float GetProgress() => progress;
    
    // Create a simple white sprite for the segments
    Sprite CreateRoundedSprite()
    {
        // This will use Unity's default sprite if available
        // You should create a proper rounded rectangle sprite in your Resources
        return null;
    }
}
