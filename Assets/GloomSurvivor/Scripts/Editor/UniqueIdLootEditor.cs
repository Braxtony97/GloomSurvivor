using System;
using System.Linq;
using GloomSurvivor.Scripts.Logic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace GloomSurvivor.Scripts.Editor
{
    [CustomEditor(typeof(UniqueIdLoot))]
    public class UniqueIdLootEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (UniqueIdLoot) target;
            
            if (IsPrefab(uniqueId))
                return;

            if (string.IsNullOrEmpty(uniqueId.Id)) 
                Generate(uniqueId);
            else
            {
                UniqueIdLoot[] uniqueIdEnemySpawners = FindObjectsOfType<UniqueIdLoot>();
                
                if (uniqueIdEnemySpawners.Any(other => other != uniqueId && other.Id == uniqueId.Id))
                    Generate(uniqueId);
            }
        }

        private bool IsPrefab(UniqueIdLoot uniqueId) => 
            uniqueId.gameObject.scene.rootCount == 0;

        private void Generate(UniqueIdLoot uniqueId)
        {
            uniqueId.Id = uniqueId.gameObject.scene.name + "_" + Guid.NewGuid().ToString();

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene); 
            }
        }
    }
}