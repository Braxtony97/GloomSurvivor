using GloomSurvivor.Scripts.Data;
using TMPro;
using UnityEngine;

public class LootCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _counter;
    
    private WorldData _worldData;

    public void Construct(WorldData worldData)
    {
        _worldData = worldData;
        _worldData.LootData.Changed += UpdateCounter;
    }

    private void Start()
    {
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        _counter.text = $"{_worldData.LootData.Collected}";
    }
}
