using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(Button))]
public class ShopButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _price;

    

    private ShopItem _shopItem;
    private GameState _gameState;
    private UIRoot _uIRoot;
    private Button _button;

    public void Initialize (GameState gameState, ShopItem shopItem, UIRoot uIRoot)
    {
        _uIRoot = uIRoot;
        if (shopItem == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        var button = GetComponent<Button>();
        button.interactable = gameState.FishStorage.FishCount >= shopItem.Price;
        _price.text = $"Price: {shopItem.Price}";
        _shopItem = shopItem;
        _gameState = gameState;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => _gameState.FishStorage.TrySpendFish(_shopItem.Price, SuccessfulBuy));
    }
    public void Activate()
    {
        var button = GetComponent<Button>();
        button.interactable = _gameState.FishStorage.FishCount >= _shopItem.Price;
    }
    public void DeActivate()
    {
        var button = GetComponent<Button>();
        button.interactable = false;
    }

    private ItemNotification GetPrefab()
    {
        var prefabName = _shopItem.Type == ShopItemType.BOWL ? "Bowl" : _shopItem.name;
        return Resources.Load<ItemNotification>($"Prefabs/Notifications/{prefabName}");
    }

    private void SuccessfulBuy()
    {
        var panel = Instantiate(GetPrefab(),_uIRoot.transform);
        gameObject.SetActive(false);
        panel.Initialize(_shopItem);
        switch (_shopItem.Type)
        {
            case ShopItemType.CAT:
                _gameState.IncreaseFishPerSecond(_shopItem.Amount);
                _gameState.IncreaseCurrentCat();
                break;
            case ShopItemType.BOWL:
                _gameState.IncreaseFishPerClick(_shopItem.Amount);
                _gameState.IncreaseCurrentBowl();
                break;
            default:
                throw new ArgumentException("Unexpected type");

        }

    }
    
}
