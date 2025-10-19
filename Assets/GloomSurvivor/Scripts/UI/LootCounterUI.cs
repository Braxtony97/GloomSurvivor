using GloomSurvivor.Scripts.Data;
using GloomSurvivor.Scripts.Services.PersistentProgress;
using TMPro;
using UnityEngine;

public class LootCounterUI : MonoBehaviour, ISavedProgress
{
    public int CollectedCoins
    {
        get => _worldData.LootData.Collected;
        set
        {
            _worldData.LootData.Collected = value;
            UpdateCounter();
        }
    }

    [SerializeField] private TMP_Text _counter;   
    private WorldData _worldData;

    public void Construct(WorldData worldData)
    {
        _worldData = worldData;
        _worldData.LootData.Changed += UpdateCounter;
    }

    private void Start() =>
       UpdateCounter();

    public void LoadProgress(Progress progress) => 
        CollectedCoins = progress.WorldData.LootData.Collected;

    public void UpdateProgress(Progress progress) => 
        progress.WorldData.LootData.Collected = CollectedCoins;

    private void UpdateCounter() => 
        _counter.text = $"{_worldData.LootData.Collected}";
}
