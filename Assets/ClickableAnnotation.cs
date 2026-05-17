using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class ClickableAnnotation : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position + Vector3.up * 2f;

        Handles.color = Color.green;

        if (Handles.Button(
            pos,
            Quaternion.identity,
            2f,
            2f,
            Handles.SphereHandleCap))
        {
            Debug.Log("Annotation clicked!");
        }

        Handles.Label(pos + Vector3.up, "Click Me");
    }
}
