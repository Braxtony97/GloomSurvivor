using GloomSurvivor.Scripts.Logic;
using UnityEditor;
using UnityEngine;

namespace GloomSurvivor.Scripts.Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)] //how draw
        public static void RenderCustomGizmo(EnemySpawner spawner, GizmoType gizmoType)
        { 
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }
    }
}