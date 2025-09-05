using UnityEngine;

public class UIRoot : MonoBehaviour
{
    [SerializeField] private ClickButton _clickButton;
    [SerializeField] private FishText _fishText;
    [SerializeField] private Shop _shop;
    [SerializeField] private Menu _menu;
    [SerializeField] private GameObject _lightOff;
    public void Initialize(GameState gameState)
    {
        _shop.Initialize(gameState, this);
        _clickButton.Initialize(gameState);
        _fishText.Initialize(gameState.FishStorage);
        _menu.Initialize(gameState);
        
    }
    public void DeactivateAll()
    {
        _shop.DeActivateButtons();
        _shop.SetActive(false);
    }
    public void ActivateAll()
    {
        _shop.SetActive(true);
        _shop.ActivateButtons();
    }
}
