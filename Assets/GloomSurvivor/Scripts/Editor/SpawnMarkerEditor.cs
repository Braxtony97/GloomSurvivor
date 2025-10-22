using GloomSurvivor.Scripts.Logic;
using UnityEditor;
using UnityEngine;

namespace GloomSurvivor.Scripts.Editor
{

    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)] //how draw
        public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmoType)
        { 
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }
    }
}
