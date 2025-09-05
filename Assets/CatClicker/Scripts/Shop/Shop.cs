using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopButton> _buttons;
    [SerializeField] private List<ShopItem> _items;
    [SerializeField] private List<RoomButton> _roomButtons;
    private GameState _gameState;
    private UIRoot _uIRoot;
    private bool _active = true;


    public void SetActive(bool active)
    {
        _active = active;
        gameObject.SetActive(active);
    }
    public void ActivateButtons()
    {
        foreach( var button in _buttons)
        {
            button.Activate();
        }
    }
    public void DeActivateButtons()
    {
        foreach (var button in _buttons)
        {
            button.DeActivate();
        }
    }
    public void Initialize(GameState gameState, UIRoot uIRoot)
    {
        _gameState = gameState;
        _uIRoot = uIRoot;
        _gameState.OnBuyItem += DrawButtons;
        _gameState.FishStorage.OnFishCountChanged += HandleFishCountChanged;
        _gameState.OnBuyRoom += DrawButtons;
        DrawButtons();
    }

    private void OnDestroy()
    {
        _gameState.OnBuyItem -= DrawButtons;
        _gameState.FishStorage.OnFishCountChanged -= HandleFishCountChanged;
        _gameState.OnBuyRoom -= DrawButtons;

    }
    private void DrawButtons()
    {
        var cat = GetCurrentCat();
        var bowl = GetCurrentBowl();
        if (_gameState.CurrentCat == _gameState.CurrentRoom * 2 && _gameState.CurrentBowl == _gameState.CurrentRoom * 2)
        {
            if (_gameState.CurrentRoom >= _roomButtons.Count)
            {
                gameObject.SetActive(false);
                return;
            }

            _roomButtons[_gameState.CurrentRoom].gameObject.SetActive(true);
            _roomButtons[_gameState.CurrentRoom].Initialize(_gameState);
            return;
        }
        
        if (cat != null && cat.ID <= _gameState.CurrentRoom * 2)
        {
            _buttons[0].Initialize(_gameState, cat, _uIRoot);
        }
        else
        {
            _buttons[0].gameObject.SetActive(false);
        }
        if (bowl != null && bowl.ID <= _gameState.CurrentRoom * 2)
        {
            _buttons[1].Initialize(_gameState, bowl, _uIRoot);
        }
        else
        {
            _buttons[1].gameObject.SetActive(false);
        }
        
        gameObject.SetActive(!(_gameState.CurrentCat + _gameState.CurrentBowl >= _items.Count) && _active);


    }
    private ShopItem GetCurrentBowl()
    {
        return _items.Find(item => item.Type == ShopItemType.BOWL && item.ID == _gameState.CurrentBowl + 1);
    }

    private void HandleFishCountChanged(long count)
    {
        DrawButtons();
    }
    private ShopItem GetCurrentCat()
    {
        return _items.Find(item => item.Type == ShopItemType.CAT && item.ID == _gameState.CurrentCat + 1);

    }

}
