using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<GameObject> Bowls;
    [SerializeField] private List<Cat> Cats;
    [SerializeField] private float _animationDuration;
    [SerializeField] private Material _material;

    private GameState _gameState;
    private List<Cat> _spawnedCats;
    private float _timer = 0f;

    public void Initialize(GameState gameState)
    {
        _spawnedCats = new List<Cat>();
        _gameState = gameState;
        _gameState.OnBuyItem += DrawItems;
        if (_material != null)
            _material.SetFloat("_Progress", 0);
        if (gameState.CurrentRoom == 0)
        {
            return;
        }
        DrawItems();

    }

    
    public IEnumerator Refill()
    {
        _timer = 0f;
        foreach (Transform child in transform)
        {
            if (!child.gameObject.name.StartsWith("Bowl"))
            {
                child.gameObject.SetActive(false);
            }
        }
        while (_timer < _animationDuration)
        {
            _timer += Time.deltaTime;
            var progress = Mathf.Clamp01(_timer / _animationDuration);
            _material.SetFloat("_Progress", progress);
            yield return new WaitForEndOfFrame();
        }
    }
    private void OnDestroy()
    {
        _gameState.OnBuyItem -= DrawItems;
    }

    private void DrawItems()
    {

        for (int i = 0; i < Bowls.Count; i++)
        {
            if (i < _gameState.CurrentBowl)
            {
                Bowls[i].SetActive(true);
            }
            else
            {
                Bowls[i].SetActive(false);
            }
        }

        for (int i = 0; i < Cats.Count; i++)
        {
            if (i < _gameState.CurrentCat)
            {
                Cats[i].gameObject.SetActive(true);
            }
            else
            {
                Cats[i].gameObject.SetActive(false);
            }
        }
    }
}
