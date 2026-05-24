using UnityEngine;

public class SplashLogoPulse : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float pulseSpeed = 2f;
    public float minScale = 0.95f;
    public float maxScale = 1.05f;
    
    [Header("Glow Settings")]
    public CanvasGroup glowOverlay;
    public float glowMinAlpha = 0.3f;
    public float glowMaxAlpha = 0.7f;
    
    [Header("Rotation Settings")]
    public float rotationSpeed = 5f;
    public bool rotateClockwise = true;
    
    private RectTransform rectTransform;
    private float pulseTimer = 0f;
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    
    void Update()
    {
        PulseAnimation();
        RotateAnimation();
        GlowAnimation();
    }
    
    void PulseAnimation()
    {
        if (rectTransform == null) return;
        
        pulseTimer += Time.deltaTime * pulseSpeed;
        float pulseValue = Mathf.Sin(pulseTimer);
        float normalizedPulse = (pulseValue + 1f) / 2f; // Convert -1..1 to 0..1
        
        float currentScale = Mathf.Lerp(minScale, maxScale, normalizedPulse);
        rectTransform.localScale = Vector3.one * currentScale;
    }
    
    void RotateAnimation()
    {
        if (rectTransform == null) return;
        
        float rotation = rotationSpeed * Time.deltaTime * (rotateClockwise ? -1 : 1);
        rectTransform.Rotate(0, 0, rotation);
    }
    
    void GlowAnimation()
    {
        if (glowOverlay == null) return;
        
        float glowTimer = (Time.time * pulseSpeed) + Mathf.PI; // Offset from pulse
        float glowValue = Mathf.Sin(glowTimer);
        float normalizedGlow = (glowValue + 1f) / 2f;
        
        glowOverlay.alpha = Mathf.Lerp(glowMinAlpha, glowMaxAlpha, normalizedGlow);
    }
}
