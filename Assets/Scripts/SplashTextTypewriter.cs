using UnityEngine;
using TMPro;
using System.Collections;

public class SplashTextTypewriter : MonoBehaviour
{
    [Header("Text Settings")]
    public TextMeshProUGUI targetText;
    public string fullText = "INITIALIZING AR EXPERIENCE...";
    
    [Header("Typewriter Settings")]
    public float typeSpeed = 0.05f;
    public float startDelay = 0f;
    public bool playOnStart = true;
    
    [Header("Cursor Settings")]
    public bool showCursor = true;
    public string cursorChar = "_";
    public float cursorBlinkRate = 0.5f;
    
    private string currentText = "";
    private bool isTyping = false;
    
    public bool IsTyping => isTyping;
    
    void Start()
    {
        if (targetText == null) targetText = GetComponent<TextMeshProUGUI>();
        
        if (playOnStart)
        {
            StartCoroutine(StartTyping());
        }
    }
    
    IEnumerator StartTyping()
    {
        yield return new WaitForSeconds(startDelay);
        yield return StartCoroutine(TypeText());
    }
    
    IEnumerator TypeText()
    {
        isTyping = true;
        currentText = "";
        
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            
            if (showCursor)
            {
                targetText.text = currentText + cursorChar;
            }
            else
            {
                targetText.text = currentText;
            }
            
            yield return new WaitForSeconds(typeSpeed);
        }
        
        isTyping = false;
        
        // Start cursor blink after typing complete
        if (showCursor)
        {
            StartCoroutine(BlinkCursor());
        }
    }
    
    IEnumerator BlinkCursor()
    {
        bool cursorVisible = true;
        
        while (true)
        {
            if (cursorVisible)
            {
                targetText.text = fullText + cursorChar;
            }
            else
            {
                targetText.text = fullText + " ";
            }
            
            cursorVisible = !cursorVisible;
            yield return new WaitForSeconds(cursorBlinkRate);
        }
    }
    
    public void StartTypingNewText(string newText)
    {
        fullText = newText;
        StopAllCoroutines();
        StartCoroutine(TypeText());
    }
}
