# Cell Part Camera Position Setup Guide

## Quick Setup Method

### Step 1: Create Helper GameObjects

For each cell part, create an empty GameObject:

1. Right-click in Hierarchy → **Create Empty**
2. Name it: `PositionHelper_[PartName]` (e.g., `PositionHelper_Nucleus`)
3. Position this at your cell part's location

### Step 2: Add Helper Script

Add `CellPartPositionHelper.cs` to each helper GameObject

### Step 3: Configure Each Cell Part

| Setting | Description |
|---------|-------------|
| **Cell Part Transform** | Drag the actual 3D model of this cell part |
| **Part Name** | Name for reference (e.g., "Nucleus") |
| **Distance From Part** | How far camera should be (in Unity units) |
| **Height Offset** | How high above the part |
| **Side Angle** | Angle around the part (0=front, 90=side, 180=back) |

### Step 4: Calculate Positions

1. In Inspector, click **gear icon** (⋮)
2. Select **"Calculate Positions"**
3. Check Console for output values
4. Click **"Copy to Clipboard"** to get formatted code

### Step 5: Paste into CellPartTarget

Open **CellNavigationManager** → **Cell Parts** array:
- Paste `cameraPosition` value
- Paste `lookAtPosition` value

---

## Recommended Settings for Each Cell Part

### Nucleus (Center - Main Focus)
```
Distance: 40-50
Height Offset: 0-10
Side Angle: 30-45
```
**Camera Position:** Close and centered  
**Look At:** Center of nucleus

### Nucleolus (Inside Nucleus)
```
Distance: 25-30
Height Offset: 5
Side Angle: 30
```
**Camera Position:** Very close, zoomed in  
**Look At:** Center of nucleolus

### Nuclear Membrane (Outer Layer)
```
Distance: 45-55
Height Offset: 0
Side Angle: 0
```
**Camera Position:** Looking at membrane edge-on  
**Look At:** Edge of membrane

### Vacuole/Mitochondria (Side Organelles)
```
Distance: 35-45
Height Offset: 5
Side Angle: 60-90
```
**Camera Position:** Side angle to see shape  
**Look At:** Center of organelle

### Lysosome (Small)
```
Distance: 20-25
Height Offset: 0
Side Angle: 45
```
**Camera Position:** Very close, zoomed in  
**Look At:** Center of lysosome

### Cell Membrane (Outer Boundary)
```
Distance: 80-100
Height Offset: 20
Side Angle: 45
```
**Camera Position:** Far back, wide angle  
**Look At:** Center of entire cell

### Golgi Complex (Stacked Layers)
```
Distance: 40-50
Height Offset: 10
Side Angle: 45
```
**Camera Position:** Medium distance, slight elevation  
**Look At:** Center of Golgi

### Rough Endoplasmic Reticulum (Network)
```
Distance: 35-45
Height Offset: 5
Side Angle: 60
```
**Camera Position:** Side view to see network  
**Look At:** Dense area of ER

### Ribosomes (Tiny Dots)
```
Distance: 25-35
Height Offset: 0
Side Angle: 45
```
**Camera Position:** Close to see small dots  
**Look At:** Cluster of ribosomes

---

## Manual Method (Without Helper Script)

### Option 1: Scene View Method

1. Enter **Play Mode**
2. In Scene view, **Fly** or **Move** camera to desired position
3. Select **Main Camera** in Hierarchy
4. Copy Position from Inspector
5. Exit Play Mode
6. Paste into CellPartTarget

### Option 2: Runtime Debug Script

Add this script to your camera temporarily:

```csharp
using UnityEngine;

public class CameraPositionDebugger : MonoBehaviour 
{
    void Update() 
    {
        // Press SPACE to log current camera position
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"Camera Position: {transform.position}");
            Debug.Log($"Camera Rotation: {transform.rotation.eulerAngles}");
            
            // Raycast to find what we're looking at
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
            {
                Debug.Log($"Looking At: {hit.point} - {hit.collider.name}");
            }
        }
    }
}
```

Usage:
1. Add script to ARCamera
2. Enter Play Mode
3. Manually position camera where you want
4. Press **SPACE** to log position
5. Copy values from Console

---

## Tips for Good Camera Angles

### Rule of Thirds
- Don't center everything perfectly
- Offset slightly for visual interest

### Depth of Field Effect
- Closer parts = smaller distance
- Overview shots = larger distance

### Rotation
- Side angles (45°-90°) show 3D shape better
- Front angles (0°) for flat/2D views

### Height
- Look slightly DOWN at small parts
- Look slightly UP at large parts
- EYE LEVEL for membrane/cell boundary

---

## Example Output (Copy-Paste Ready)

After using the helper script, you'll get:

```csharp
// Nucleus
cameraPosition = new Vector3(-735.50f, 452.30f, -1190.25f);
lookAtPosition = new Vector3(-740.00f, 450.00f, -500.00f);

// Nucleolus
cameraPosition = new Vector3(-738.20f, 451.80f, -1185.40f);
lookAtPosition = new Vector3(-740.00f, 450.00f, -502.00f);

// etc...
```

Just copy these into your CellPartTarget array!

---

## Testing

After setting all positions:
1. Enter **Play Mode**
2. Click each button
3. Camera should smoothly transition
4. If movement is too fast/slow: adjust `moveSpeed` in **CameraOrbitController**

**Move Speed:** 2-5 (lower = slower, higher = faster)

---

## Troubleshooting

| Problem | Solution |
|---------|----------|
| Camera goes inside model | Increase `distanceFromPart` |
| Can't see the part | Check `lookAtPosition` is correct |
| Camera moves too fast | Lower `moveSpeed` in CameraOrbitController |
| Vuforia overrides position | Script already disables Vuforia during movement |
| Part not found | Check partName matches exactly (case-sensitive) |

---

Need help with a specific cell part's camera position?
