using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    [SerializeField] private Vector2 _anchoredPositionIn = new Vector2(0, 0);
    [SerializeField] private Vector2 _anchoredPositionOut = new Vector2(900, 0);
    [SerializeField] private float _animationDuration = 0.5f;
    [SerializeField] private List<SoundBar> _soundBars;
    [SerializeField] private GameObject _clickArea;

    private RectTransform _rectTransform;
    private bool _isVisible = false;
    private Tweener _tween;
    private GameState _gameState;
    private Sounds _sounds;

    public void Initialize(GameState gameState)
    {
        _sounds = Object.FindFirstObjectByType<Sounds>();
        _gameState = gameState;
        foreach (var bar in _soundBars)
        {
            bar.Initialize(gameState, _sounds);
        }
    }
    public void ToggleVisability()
    {
       
        _tween?.Kill();
        _sounds.PlaySound(Sounds.SHOW_MENU);
        Vector2 targetPosition = _isVisible ? _anchoredPositionOut : _anchoredPositionIn;

        _tween = _rectTransform.DOAnchorPos(targetPosition, _animationDuration).SetEase(Ease.OutCubic);

        _isVisible = !_isVisible;
        _clickArea.SetActive(_isVisible);
    }
    
    public void SaveGame()
    {
        _gameState.SaveGame();
    }

    public void LoadGame()
    {
        _gameState.LoadGame(new GameState());
    }

    public void ResetGame()
    {
        _gameState.SetState(new GameState());
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }


}
