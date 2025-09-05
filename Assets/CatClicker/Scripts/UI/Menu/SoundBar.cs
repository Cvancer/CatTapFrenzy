using System.Collections.Generic;
using UnityEngine;

public class SoundBar : MonoBehaviour
{
    [SerializeField] private List<GameObject> _fillImages;
    [SerializeField] private Type _type;
    private GameState _gameState;
    private Sounds _sounds;
    private void FillSquares()
    {
        var volume = 0f;
        if(_type == Type.Music)
        {
            volume = _gameState.MusicVolume;
        }
        else
        {
            volume = _gameState.SoundsVolume;
        }
        for (int i = 0;  i < _fillImages.Count; i++)
        {
            _fillImages[i].SetActive(i + 1 <= Mathf.Round(volume / 0.1f));
        }
    }

    public enum Type
    {
        Music, 
        Sounds
            
    }

    public void Initialize(GameState gameState, Sounds sounds)
    {
        _sounds = sounds;
        _gameState = gameState;
        if (_type == Type.Music)
        {
            _gameState.OnMusicVolumeChange += FillSquares;
        }
        else
        {
            _gameState.OnSoundsVolumeChange += FillSquares;
        }
        FillSquares();
    }

    public void PlaySound()
    {
        _sounds.PlaySound(Sounds.CHANGE_VOLUME);
    }

    private void OnDestroy()
    {
        if (_type == Type.Music)
        {
            _gameState.OnMusicVolumeChange -= FillSquares;
        }
        else
        {
            _gameState.OnSoundsVolumeChange -= FillSquares;
        }

    }
    public void IncreaseSoundVolume()
    {
        _gameState.SetSoundsVolume(Mathf.Min(1f, _gameState.SoundsVolume + 0.1f));
    }
    public void DecreaseSoundVolume()
    {
        _gameState.SetSoundsVolume(Mathf.Max(0f, _gameState.SoundsVolume - 0.1f));
    }
    public void IncreaseMusicVolume()
    {
        _gameState.SetMusicVolume(Mathf.Min(1f, _gameState.MusicVolume + 0.1f));
    }
    public void DecreaseMusicVolume()
    {

        _gameState.SetMusicVolume(Mathf.Max(0f, _gameState.MusicVolume - 0.1f));
    }
}
