using UnityEngine;
using TMPro;


[RequireComponent(typeof(TextMeshProUGUI))]
public class FishText : MonoBehaviour
{
    private FishStorage _fishStorage;
    private TextMeshProUGUI _text;

    
    public void Initialize(FishStorage fishStorage)
    {
        _fishStorage = fishStorage;
        _text = GetComponent<TextMeshProUGUI>();
        _fishStorage.OnFishCountChanged += UpdateText;
        UpdateText(_fishStorage.FishCount);
    }

    private void OnDestroy()
    {
        _fishStorage.OnFishCountChanged -= UpdateText;

    }

    private void UpdateText(long fish)
    {
        if (_text != null)
        {
            _text.text = $"Fishes: {fish}";
        }
    }
    

     
}
