using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class CellPartPositionHelper : MonoBehaviour
{
    [Header("Cell Part Reference")]
    public Transform cellPartTransform;
    public string partName = "Cell Part";
    
    [Header("Camera Settings")]
    public float distanceFromPart = 50f;
    public float heightOffset = 10f;
    public float sideAngle = 45f;
    
    [Header("Output (Read Only)")]
    public Vector3 calculatedCameraPosition;
    public Vector3 calculatedLookAtPosition;
    
    [Header("Debug")]
    public bool showGizmos = true;
    public Color gizmoColor = Color.cyan;
    
    void OnDrawGizmos()
    {
        if (!showGizmos || cellPartTransform == null) return;
        
        Gizmos.color = gizmoColor;
        
        // Draw line from camera position to cell part
        Gizmos.DrawLine(calculatedCameraPosition, calculatedLookAtPosition);
        
        // Draw camera position
        Gizmos.DrawSphere(calculatedCameraPosition, 5f);
        
        // Draw look at position
        Gizmos.DrawSphere(calculatedLookAtPosition, 3f);
        
        // Draw view cone
        Vector3 direction = (calculatedLookAtPosition - calculatedCameraPosition).normalized;
        Gizmos.DrawRay(calculatedCameraPosition, direction * 20f);
    }
    
    [ContextMenu("Calculate Positions")]
    public void CalculatePositions()
    {
        if (cellPartTransform == null)
        {
            Debug.LogError("Cell Part Transform is not assigned!");
            return;
        }
        
        // Calculate look at position (center of cell part)
        calculatedLookAtPosition = cellPartTransform.position;
        
        // Calculate camera position
        Vector3 offset = new Vector3(
            Mathf.Sin(sideAngle * Mathf.Deg2Rad) * distanceFromPart,
            heightOffset,
            Mathf.Cos(sideAngle * Mathf.Deg2Rad) * distanceFromPart
        );
        
        calculatedCameraPosition = cellPartTransform.position + offset;
        
        Debug.Log($"=== {partName} Position Data ===");
        Debug.Log($"Camera Position: {calculatedCameraPosition}");
        Debug.Log($"Look At Position: {calculatedLookAtPosition}");
        
        #if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        #endif
    }
    
    [ContextMenu("Copy to Clipboard")]
    public void CopyToClipboard()
    {
        string data = $"// {partName}\n" +
                      $"cameraPosition = new Vector3({calculatedCameraPosition.x:F2}f, {calculatedCameraPosition.y:F2}f, {calculatedCameraPosition.z:F2}f);\n" +
                      $"lookAtPosition = new Vector3({calculatedLookAtPosition.x:F2}f, {calculatedLookAtPosition.y:F2}f, {calculatedLookAtPosition.z:F2}f);\n";
        
        GUIUtility.systemCopyBuffer = data;
        Debug.Log($"Copied to clipboard!\n{data}");
    }
    
    [ContextMenu("Preview Camera Position")]
    public void PreviewCameraPosition()
    {
        #if UNITY_EDITOR
        if (SceneView.lastActiveSceneView != null && calculatedCameraPosition != Vector3.zero)
        {
            SceneView.lastActiveSceneView.LookAt(calculatedLookAtPosition);
        }
        #endif
    }
}
