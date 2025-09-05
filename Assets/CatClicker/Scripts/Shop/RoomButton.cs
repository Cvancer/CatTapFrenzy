using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private TextMeshProUGUI _priceText;

    private GameState _gameState;
    public void Initialize(GameState gameState)
    {
        _gameState = gameState;
        var button = GetComponent<Button>();
        _priceText.text = $"Price: {_price}";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => _gameState.FishStorage.TrySpendFish(_price, SuccessfulBuy));
        button.interactable = gameState.FishStorage.FishCount >= _price;
    }

    private void SuccessfulBuy()
    {
        _gameState.IncreaseCurrentRoom();
        gameObject.SetActive(false);

    }


}
