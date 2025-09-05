using UnityEngine;
using UnityEngine.UI;
using TMPro;



[RequireComponent(typeof(Button))]
public class ClickButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private ParticleSystem _particles;
    private Button _button;


    public void Initialize(GameState gameState)
    {
        var button = GetComponent<Button>();
        _button = button;
        button.onClick.AddListener(() => gameState.AddFishPerClick());
        _particles = FindFirstObjectByType<ParticleSystem>();
        button.onClick.AddListener(() => PlayParicles());
        var sounds = FindFirstObjectByType<Sounds>();
        button.onClick.AddListener(() => sounds.PlaySound(Sounds.CLICK));
        button.onClick.AddListener( FirstClick);
        
    }

    private void FirstClick()
    {
        _text.gameObject.SetActive(false);
        _button.onClick.RemoveListener(FirstClick);
    }

    private void PlayParicles()
    {
        
        _particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        _particles.Play();
    }

}
