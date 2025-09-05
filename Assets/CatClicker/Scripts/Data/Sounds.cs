using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public const string MUSIC = "Music";
    public const string CLICK = "ClickSound";
    public const string LIGHTON = "LightOn";
    public const string REPAIR = "Repair";
    public const string CHANGE_VOLUME = "ChangeVolume";
    public const string SHOW_MENU = "ShowMenu";
    public const string NOTIFICATION = "Notification";
    public const string TOUCH_BUTTON = "TouchButton";


    [SerializeField] private List<AudioSource> _sounds;

    private GameState _gameState;

    public void Initialize(GameState gameState)
    {
        _gameState = gameState;
        SetSoundsVolume();
        SetMusicVolume();
        _gameState.OnSoundsVolumeChange += SetSoundsVolume;
        _gameState.OnMusicVolumeChange += SetMusicVolume;

    }

    public float GetSoundDuration(string soundName)
    {
        var sound = _sounds.Find(sound => sound.gameObject.name == soundName);
        return sound.clip.length;
    }

    public void PlaySound(string soundName)
    {
        var sound = _sounds.Find(sound => sound.gameObject.name == soundName);
        sound.Play();
    }

    public void StopSound(string soundName)
    {
        var sound = _sounds.Find(sound => sound.gameObject.name == soundName);
        sound.Stop();
    }

    private void SetSoundsVolume()
    {
        foreach (var sound in _sounds)
        {
            if (sound.gameObject.name != MUSIC)
            {
                sound.volume = _gameState.SoundsVolume;
            }
        }
    }
    private void SetMusicVolume()
    {
        var sound = _sounds.Find(sound => sound.gameObject.name == MUSIC);
        sound.volume = _gameState.MusicVolume;
    }

    private void OnDestroy()
    {
        _gameState.OnSoundsVolumeChange -= SetSoundsVolume;
        _gameState.OnMusicVolumeChange -= SetMusicVolume;
    }
}
