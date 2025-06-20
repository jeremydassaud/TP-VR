using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    [Header("Teleport Point Settings")]
    public string pointName = "Salle Quiz 2";
    public Color gizmoColor = Color.blue;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

        Vector3 forward = transform.forward;
        Gizmos.DrawRay(transform.position, forward * 1.5f);
        Gizmos.DrawRay(transform.position + forward * 1.5f, -forward * 0.3f + transform.right * 0.3f);
        Gizmos.DrawRay(transform.position + forward * 1.5f, -forward * 0.3f - transform.right * 0.3f);

#if UNITY_EDITOR
        UnityEditor.Handles.Label(transform.position + Vector3.up * 0.8f, pointName);
#endif
    }
}