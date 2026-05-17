using UnityEngine;

[System.Serializable]
public class CellPartTarget
{
    [Header("Cell Part Info")]
    public string partName;           // e.g. "Nucleus", "Mitochondria"
    public string description;        // Short description of the cell part
    public GameObject partObject;     // Reference to the 3D object of this cell part (optional, for highlighting)

    [Header("Camera Position (Where the camera goes)")]
    public Vector3 cameraOffset;      // Camera position offset from pivot (e.g. x=2, y=1, z=3)
    
    [Header("Look At Offset")]
    public Vector3 lookAtOffset;      // Where the camera looks at (offset from pivot center)
}
