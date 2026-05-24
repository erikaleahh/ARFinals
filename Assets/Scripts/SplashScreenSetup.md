# Splash Screen Setup Guide

## Scripts Created
1. **SplashScreenLoader.cs** - Main controller with progress bar animation and scene loading
2. **SplashLogoPulse.cs** - Adds pulse, rotation, and glow effects to the cell logo
3. **SplashTextTypewriter.cs** - Typewriter effect for loading text

## Unity Setup Steps

### 1. Attach Scripts to GameObjects

**Create an Empty GameObject** named "SplashController" and attach **SplashScreenLoader.cs**

Assign these references in the Inspector:
- Title Text (TextMeshProUGUI) - "ANIMAL CELL"
- Subtitle Text (TextMeshProUGUI) - "THE BUILDING BLOCK OF LIFE"
- Tagline Text (TextMeshProUGUI) - "SMALL IN SIZE, BIG IN FUNCTION"
- Loading Text (TextMeshProUGUI) - "INITIALIZING AR EXPERIENCE..."
- Progress Bar (Slider)
- Logo Transform (RectTransform of your cell image)
- Logo CanvasGroup (add CanvasGroup component to logo)

**On your Logo GameObject**, attach **SplashLogoPulse.cs**

**On your Loading Text GameObject**, attach **SplashTextTypewriter.cs**

### 2. Create UI Elements

**Title Text (TextMeshPro)**
- Font: Bold, Size: 72
- Color: #9D4EDD (Purple glow)
- Add "Outline" or "Glow" effect in Font Material
- Position: Top center

**Subtitle Text (TextMeshPro)**
- Font: Regular, Size: 28
- Color: #FFFFFF
- Spacing: 20 between characters (tracking)
- Position: Below title

**Tagline Text (TextMeshPro)**
- Font: Light, Size: 18
- Color: #CCCCCC
- Position: Below subtitle

**Loading Text (TextMeshPro)**
- Font: Monospace, Size: 16
- Color: #00F5FF (Cyan)
- Position: Above progress bar

### 3. Create Sci-Fi Progress Bar

**Method: Segmented Bar**

1. Create empty GameObject "ProgressBar"
2. Add Horizontal Layout Group component
3. Create 8-10 child Image objects named "Segment_1", "Segment_2", etc.
4. Set each segment's Source Image to a rounded rectangle sprite
5. Size: Width 30, Height 10 per segment
6. Spacing: 5 pixels between segments

**Color Settings:**
- Active: #9D4EDD (Purple)
- Inactive: #333333 (Dark gray)

**Alternative: Slider Progress Bar**

1. Create Slider (UI > Slider)
2. Set Direction: Left to Right
3. Remove Handle Slide Area
4. Fill Area: Create a sci-fi styled fill image with glow

### 4. Add HUD Frame

1. Create Image covering full screen (1080x1920)
2. Use 9-sliced sprite for tech border
3. Add corner accents (small sci-fi icons)
4. Add top/bottom decorative lines

### 5. Add Particle Effects (Optional)

1. Create Particle System GameObject
2. Position behind logo
3. Settings:
   - Shape: Box or Circle
   - Start Color: #9D4EDD with alpha 0.3
   - Start Size: 1-3
   - Speed: 0.5 upward
   - Emission: 20 particles/sec

### 6. Configure Build Settings

1. File > Build Settings
2. Add Open Scenes
3. Ensure SPLASH scene is first in build order (index 0)
4. Set "Game" scene as nextSceneName in SplashScreenLoader

### 7. Color Reference

```
Purple Glow:    #9D4EDD  (RGB: 157, 78, 221)
Cyan Accent:  #00F5FF  (RGB: 0, 245, 255)
Deep Space:   #050510  (RGB: 5, 5, 16)
Cell Nucleus: #E0AAFF  (RGB: 224, 170, 255)
Mitochondria: #FF6B35  (RGB: 255, 107, 53)
```

### 8. Animation Timing Reference

```
0.0s - Logo scale up + fade in (0.6s)
0.8s - Title fade in (0.5s)
1.0s - Subtitle fade in (0.4s)
1.2s - Tagline fade in (0.3s)
1.5s - Progress bar starts (2s)
3.5s - Scene transition
```

## Testing

1. Press Play in Unity Editor
2. Check animation sequence
3. Verify scene transition works
4. Test on mobile device

## Notes

- Minimum display time is 3 seconds (adjustable in inspector)
- Add a "Tap to Skip" button if desired
- Ensure TextMeshPro package is installed
- For mobile: Set target frame rate to 60 in Awake()
