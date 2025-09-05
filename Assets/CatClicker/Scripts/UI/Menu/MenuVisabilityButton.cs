using UnityEngine;
using UnityEngine.UI;

public class MenuVisabilityButton : MonoBehaviour
{
    [SerializeField] private Sprite _spriteOpen;
    [SerializeField] private Sprite _spriteClose;

    private Image _image;
    private bool _isOpened = false;

    public void ToggleImage()
    {
        _isOpened = !_isOpened;
        _image.sprite = _isOpened ? _spriteClose : _spriteOpen ;
    }
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

}
