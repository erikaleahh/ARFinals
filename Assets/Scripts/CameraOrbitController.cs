using UnityEngine;
using Vuforia;

public class CameraOrbitController : MonoBehaviour
{
    [Header("Transition Settings")]
    [Tooltip("How fast the camera moves to the target position")]
    public float moveSpeed = 3f;

    [Tooltip("How fast the camera rotates to look at the target")]
    public float rotateSpeed = 3f;

    [Header("Default Camera Position")]
    [Tooltip("The default camera position (copy your ARCamera's starting Transform Position here)")]
    public Vector3 defaultPosition = new Vector3(-739f, 451.75f, -1191f);

    [Tooltip("Where the camera looks at by default (center of cell model)")]
    public Vector3 defaultLookAt = new Vector3(-740f, 450f, -500f);

    // Private variables
    private Vector3 targetPosition;
    private Vector3 targetLookAt;
    private bool isTransitioning = false;
    private bool isResetting = false;
    private VuforiaBehaviour vuforiaBehaviour;

    void Start()
    {
        // Get Vuforia reference
        vuforiaBehaviour = GetComponent<VuforiaBehaviour>();

        // Set initial targets
        targetPosition = defaultPosition;
        targetLookAt = defaultLookAt;
    }

    void LateUpdate()
    {
        if (!isTransitioning) return;

        // Smoothly move camera to target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Smoothly rotate camera to look at target
        Vector3 direction = targetLookAt - transform.position;
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        // Check if we've arrived (close enough)
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget < 1f)
        {
            transform.position = targetPosition;
            transform.LookAt(targetLookAt);
            isTransitioning = false;

            // Re-enable Vuforia ONLY after Reset transition is complete
            if (isResetting)
            {
                isResetting = false;
                if (vuforiaBehaviour != null)
                {
                    vuforiaBehaviour.enabled = true;
                    Debug.Log("Vuforia re-enabled after reset.");
                }
            }
        }
    }

    /// <summary>
    /// Navigate the camera to focus on a specific cell part.
    /// </summary>
    public void NavigateTo(CellPartTarget cellPart)
    {
        // Disable Vuforia so it doesn't override our camera movement
        if (vuforiaBehaviour != null && vuforiaBehaviour.enabled)
        {
            vuforiaBehaviour.enabled = false;
        }

        targetPosition = cellPart.cameraPosition;
        targetLookAt = cellPart.lookAtPosition;
        isTransitioning = true;
        isResetting = false;

        Debug.Log($"Camera navigating to: {cellPart.partName} at position {cellPart.cameraPosition}");
    }

    /// <summary>
    /// Return the camera smoothly to the default position.
    /// Vuforia will re-enable AFTER the transition is complete.
    /// </summary>
    public void ResetToDefault()
    {
        targetPosition = defaultPosition;
        targetLookAt = defaultLookAt;
        isTransitioning = true;
        isResetting = true;  // Flag to re-enable Vuforia after arriving

        Debug.Log("Camera resetting to default view (smooth transition)...");
    }
}
