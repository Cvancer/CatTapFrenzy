using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemNotification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _info;
    [SerializeField] private TextMeshProUGUI _income;
    [SerializeField] private Button _button;
    
    public void Initialize (ShopItem item)
    {
        _info.text = $"You bought a {item.Kind}";
        var per = (item.Type == ShopItemType.CAT ? "second" : "click");
        _income.text = $"It brings an extra {item.Amount} per {per}";
        _button.onClick.AddListener(() => gameObject.SetActive(false));
        Object.FindFirstObjectByType<Sounds>().PlaySound(Sounds.NOTIFICATION);
    }

}
