using UnityEngine;

public class CameraOrbitController : MonoBehaviour
{
    [Header("Pivot Settings")]
    [Tooltip("The center point of the cell model (the camera orbits around this)")]
    public Transform pivotPoint;

    [Header("Transition Settings")]
    [Tooltip("How fast the camera moves to the target position")]
    public float moveSpeed = 3f;

    [Tooltip("How fast the camera rotates to look at the target")]
    public float rotateSpeed = 3f;

    [Header("Default View")]
    [Tooltip("Default camera offset when viewing the full cell")]
    public Vector3 defaultOffset = new Vector3(0f, 5f, -10f);

    // Private variables
    private Vector3 targetPosition;
    private Vector3 targetLookAt;
    private bool isTransitioning = false;
    private bool isAtDefault = true;

    void Start()
    {
        if (pivotPoint == null)
        {
            Debug.LogError("CameraOrbitController: Pivot Point is not assigned!");
            return;
        }

        // Set initial position
        targetPosition = pivotPoint.position + defaultOffset;
        targetLookAt = pivotPoint.position;
        transform.position = targetPosition;
        transform.LookAt(targetLookAt);
    }

    void Update()
    {
        if (!isTransitioning) return;

        // Smoothly move camera to target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Smoothly rotate camera to look at target
        Vector3 direction = targetLookAt - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        // Check if we've arrived (close enough)
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget < 0.05f)
        {
            transform.position = targetPosition;
            transform.LookAt(targetLookAt);
            isTransitioning = false;
        }
    }

    /// <summary>
    /// Navigate the camera to focus on a specific cell part.
    /// Called by CellNavigationManager when a button is clicked.
    /// </summary>
    public void NavigateTo(CellPartTarget cellPart)
    {
        if (pivotPoint == null) return;

        targetPosition = pivotPoint.position + cellPart.cameraOffset;
        targetLookAt = pivotPoint.position + cellPart.lookAtOffset;
        isTransitioning = true;
        isAtDefault = false;

        Debug.Log($"Camera navigating to: {cellPart.partName}");
    }

    /// <summary>
    /// Return the camera to the default overview position.
    /// Called when "Reset" button is clicked.
    /// </summary>
    public void ResetToDefault()
    {
        targetPosition = pivotPoint.position + defaultOffset;
        targetLookAt = pivotPoint.position;
        isTransitioning = true;
        isAtDefault = true;

        Debug.Log("Camera resetting to default view.");
    }

    /// <summary>
    /// Check if the camera is currently at the default position.
    /// </summary>
    public bool IsAtDefault()
    {
        return isAtDefault;
    }
}
