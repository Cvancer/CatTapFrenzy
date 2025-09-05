using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Button))] 
public class TouchButtonSound : MonoBehaviour, IPointerEnterHandler
{
    private Sounds _sounds;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _sounds.PlaySound(Sounds.TOUCH_BUTTON);
    }
    private void Awake()
    {
        _sounds = Object.FindFirstObjectByType<Sounds>();
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => _sounds.PlaySound(Sounds.CHANGE_VOLUME)); 
        
    }
}
