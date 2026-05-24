using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class SplashScreenLoader : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;
    public TextMeshProUGUI taglineText;
    public TextMeshProUGUI loadingText;
    public Slider progressBar;
    public RectTransform progressBarFill;
    public Image[] progressSegments;
    
    [Header("Logo Animation")]
    public RectTransform logoTransform;
    public CanvasGroup logoCanvasGroup;
    
    [Header("Settings")]
    public float minDisplayTime = 3f;
    public string nextSceneName = "Game";
    
    [Header("Colors")]
    public Color activeSegmentColor = new Color(0.62f, 0.31f, 0.87f, 1f); // Purple glow
    public Color inactiveSegmentColor = new Color(0.2f, 0.2f, 0.2f, 0.5f);
    
    private float startTime;
    private bool isLoading = false;
    
    public bool IsLoading => isLoading;
    
    void Start()
    {
        startTime = Time.time;
        
        // Initialize UI states
        if (titleText) titleText.alpha = 0;
        if (subtitleText) subtitleText.alpha = 0;
        if (taglineText) taglineText.alpha = 0;
        if (loadingText) loadingText.text = "INITIALIZING AR EXPERIENCE...";
        
        if (progressBar) progressBar.value = 0;
        if (logoCanvasGroup) logoCanvasGroup.alpha = 0;
        
        // Initialize progress segments
        if (progressSegments != null)
        {
            foreach (var segment in progressSegments)
            {
                if (segment) segment.color = inactiveSegmentColor;
            }
        }
        
        // Start the animation sequence
        StartCoroutine(PlaySplashSequence());
    }
    
    IEnumerator PlaySplashSequence()
    {
        // Phase 1: Logo appears with scale animation
        if (logoTransform && logoCanvasGroup)
        {
            yield return StartCoroutine(AnimateLogoAppear());
        }
        
        yield return new WaitForSeconds(0.2f);
        
        // Phase 2: Title fades in
        if (titleText)
        {
            yield return StartCoroutine(FadeTextIn(titleText, 0.5f));
        }
        
        yield return new WaitForSeconds(0.15f);
        
        // Phase 3: Subtitle fades in
        if (subtitleText)
        {
            yield return StartCoroutine(FadeTextIn(subtitleText, 0.4f));
        }
        
        yield return new WaitForSeconds(0.1f);
        
        // Phase 4: Tagline fades in
        if (taglineText)
        {
            yield return StartCoroutine(FadeTextIn(taglineText, 0.3f));
        }
        
        yield return new WaitForSeconds(0.3f);
        
        // Phase 5: Progress bar animates
        isLoading = true;
        yield return StartCoroutine(AnimateProgressBar());
    }
    
    IEnumerator AnimateLogoAppear()
    {
        float duration = 0.6f;
        float elapsed = 0;
        Vector3 targetScale = Vector3.one;
        logoTransform.localScale = Vector3.zero;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float smoothT = Mathf.SmoothStep(0, 1, t);
            
            logoTransform.localScale = Vector3.Lerp(Vector3.zero, targetScale, smoothT);
            logoCanvasGroup.alpha = smoothT;
            
            yield return null;
        }
        
        logoTransform.localScale = targetScale;
        logoCanvasGroup.alpha = 1;
    }
    
    IEnumerator FadeTextIn(TextMeshProUGUI text, float duration)
    {
        float elapsed = 0;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            text.alpha = Mathf.Lerp(0, 1, elapsed / duration);
            yield return null;
        }
        
        text.alpha = 1;
    }
    
    IEnumerator AnimateProgressBar()
    {
        float elapsed = 0;
        float loadDuration = 2f;
        int totalSegments = progressSegments?.Length ?? 10;
        
        while (elapsed < loadDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / loadDuration);
            
            // Update slider
            if (progressBar) progressBar.value = progress;
            
            // Update segments
            if (progressSegments != null)
            {
                int activeSegments = Mathf.FloorToInt(progress * totalSegments);
                for (int i = 0; i < progressSegments.Length; i++)
                {
                    if (progressSegments[i])
                    {
                        progressSegments[i].color = (i < activeSegments) ? activeSegmentColor : inactiveSegmentColor;
                    }
                }
            }
            
            yield return null;
        }
        
        // Ensure all segments are active
        if (progressSegments != null)
        {
            foreach (var segment in progressSegments)
            {
                if (segment) segment.color = activeSegmentColor;
            }
        }
        
        if (progressBar) progressBar.value = 1;
        
        // Change loading text
        if (loadingText) loadingText.text = "READY!";
        
        yield return new WaitForSeconds(0.5f);
        
        // Load next scene
        LoadNextScene();
    }
    
    void LoadNextScene()
    {
        // Ensure minimum display time
        float elapsedTime = Time.time - startTime;
        if (elapsedTime < minDisplayTime)
        {
            StartCoroutine(DelayedLoad(minDisplayTime - elapsedTime));
            return;
        }
        
        SceneManager.LoadScene(nextSceneName);
    }
    
    IEnumerator DelayedLoad(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }
    
    // Call this from a button to skip
    public void SkipSplash()
    {
        if (!isLoading) return; // Prevent skipping if not loading yet
        StopAllCoroutines();
        LoadNextScene();
    }
}
