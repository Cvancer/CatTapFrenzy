using System;
using UnityEngine;

[Serializable]
public class FishStorage 
{
    public event Action<long> OnFishCountChanged;


    [SerializeField] private long _fishCount;
    public long FishCount => _fishCount;

    public FishStorage()
    {
        _fishCount = 0;
    }

    public FishStorage(long fishCount)
    {
        _fishCount = fishCount;
    }

    public void AddFish(int fish)
    {
        if (fish < 0)
        {
            throw new ArgumentException("Fish can not be negative");
        }
        _fishCount += fish;
        OnFishCountChanged?.Invoke(_fishCount);
    }

    public void TrySpendFish(int fish, Action onSuccess)
    {
        try
        {
            SpendFish(fish);
            onSuccess?.Invoke();
        }
        catch (ArgumentException e)
        {
            Debug.Log(e);
        }

    }

    private void SpendFish(int fish)
    {
        if (fish < 0)
        {
            throw new ArgumentException("Fish can not be negative");
        }
        if (fish > _fishCount)
        {
            throw new ArgumentException("Not enough fish");
        }
        _fishCount -= fish;
        OnFishCountChanged?.Invoke(_fishCount);
    }
}
