using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState 
{
    public event Action OnGameStateChanged;
    public event Action OnBuyItem;
    public event Action OnSoundsVolumeChange;
    public event Action OnMusicVolumeChange;
    public event Action OnBuyRoom;
    public int FishPerClick => _fishPerClick;
    public int FishPerSecond => _fishPerSecond;
    public int CurrentBowl => _currentBowl;
    public int CurrentCat => _currentCat;
    public float SoundsVolume => _soundsVolume;
    public float MusicVolume => _musicVolume;
    public int CurrentRoom => _currentRoom;
    public FishStorage FishStorage => _fishStorage;
    [SerializeField] private int _fishPerClick;
    [SerializeField] private int _fishPerSecond;
    [SerializeField] private FishStorage _fishStorage;
    [SerializeField] private int _currentBowl;
    [SerializeField] private int _currentCat;
    [SerializeField] private float _soundsVolume;
    [SerializeField] private float _musicVolume;
    [SerializeField] private int _currentRoom;
    public GameState()
    {
        _currentBowl = 0;
        _currentCat = 0;
        _fishPerClick = 1;
        _fishPerSecond = 0;
        _soundsVolume = 0.2f;
        _musicVolume = 0.2f;
        _currentRoom = 0;
        

        _fishStorage = new FishStorage();
    }

    

    public GameState(int fishPerClick, int fishPerSecond, FishStorage fishStorage, int currentBowl, int currentCat, float soundsVolume, float musicVolume, int currentRoom)
    {
        _currentBowl = currentBowl;
        _currentCat = currentCat;
        _fishPerClick = fishPerClick;
        _fishPerSecond = fishPerSecond;
        _fishStorage = fishStorage;
        _soundsVolume = soundsVolume;
        _musicVolume = musicVolume;
        _currentRoom = currentRoom;
    }

    public GameState(GameState gameState)
    {
        SetState(gameState);
    }

    public void SaveGame()
    {
        var json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("Game", json);
        Debug.Log("GameSaved");
    }

    public void LoadGame(GameState defaultData)
    {
        if (!PlayerPrefs.HasKey("Game"))
        {
            SetState(defaultData);
        }
        var json = PlayerPrefs.GetString("Game");
        SetState(JsonUtility.FromJson<GameState>(json));
    }

    

    public void SetState(GameState gameState)
    {
        _currentBowl = gameState.CurrentBowl;
        _currentCat = gameState.CurrentCat;
        _fishPerClick = gameState.FishPerClick;
        _fishPerSecond = gameState.FishPerSecond;
        _fishStorage = gameState.FishStorage;
        _soundsVolume = gameState.SoundsVolume;
        _musicVolume = gameState.MusicVolume;
        _currentRoom = gameState.CurrentRoom;
        OnGameStateChanged?.Invoke();
    }
    
    public void SetSoundsVolume(float volume)
    {
        _soundsVolume = volume;
        OnSoundsVolumeChange?.Invoke();
    }

    public void SetMusicVolume(float volume)
    {
        _musicVolume = volume;
        OnMusicVolumeChange?.Invoke();
    }


    
    public void AddFishPerClick()
    {
        _fishStorage.AddFish(_fishPerClick);
    }

    public void AddFishPerSecond()
    {
        _fishStorage.AddFish(_fishPerSecond);
    }

    public void IncreaseFishPerClick(int value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Value can not be negative");
        }
        _fishPerClick += value;
        
    }

    public void IncreaseFishPerSecond(int value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Value can not be negative");
        }
        _fishPerSecond += value;
    }
    
    public void IncreaseCurrentBowl()
    {
        _currentBowl++;
        OnBuyItem?.Invoke();
    }

    public void IncreaseCurrentCat()
    {
        _currentCat++;
        OnBuyItem?.Invoke();
    }
    public void IncreaseCurrentRoom()
    {
        _currentRoom++;
        OnBuyRoom?.Invoke();
    }

}
