using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Items/ShopItem")]
public class ShopItem : ScriptableObject
{
    public ShopItemType Type => _type;
    public Sprite Image => _image;
    public int Price => _price;
    public int Amount => _amount;
    public int ID => _id;
    public string Kind => _kind; 


    [SerializeField] private ShopItemType _type;
    [SerializeField] private Sprite _image;
    [SerializeField] private int _price;
    [SerializeField] private int _amount;
    [SerializeField] private int _id;
    [SerializeField] private string _kind;
}
