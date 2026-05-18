using UnityEngine;

[System.Serializable]
public class CellPartTarget
{
    [Header("Cell Part Info")]
    public string partName;           // e.g. "Nucleus", "Nucleolus"
    public string description;        // Short description of the cell part
    public GameObject partObject;     // Reference to the 3D object (optional, for highlighting)

    [Header("Camera Target Position (World Position)")]
    [Tooltip("The exact world position where the camera should go. To find this: move the camera in Scene view to the position you want, then copy the Transform Position values here.")]
    public Vector3 cameraPosition;

    [Header("Camera Look At Position (World Position)")]
    [Tooltip("The exact world position the camera should look at (usually the cell part position).")]
    public Vector3 lookAtPosition;
}
