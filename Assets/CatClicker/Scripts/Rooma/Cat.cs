using System.Collections;

using UnityEngine;

public class Cat : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _currentDirection;
    private const float _moveSpeed = 0.1f;
    private const float _directionChangeTime = 3f;
    private const float _timeBetweenAnimationsMax = 15f;
    private const float _timeBetweenAnimationsMin = 7f;
    private bool _isRandomAnimationPlay = false;

    private float _directionTimer;



    private IEnumerator EndRandomAnimation()
    {
        yield return new WaitForSeconds(3f);

        _isRandomAnimationPlay = false;
        yield return PlayNextAnimation();
    }

    private void PlayRandomAnimation()
    {
        _isRandomAnimationPlay = true;
        int randomChoice = Random.Range(0, 3);
        switch (randomChoice)
        {
            case 0:
                PlayAnimation("Idle");
                break;
            case 1:
                PlayAnimation("Lick");
                break;
            case 2:
                PlayAnimation("LickBalls");
                break;
        }
    }

    private IEnumerator PlayNextAnimation()
    {
        yield return new WaitForSeconds(Random.Range(_timeBetweenAnimationsMin, _timeBetweenAnimationsMax));
        PlayRandomAnimation();
    }

    private void PlayAnimation(string triggerName)
    {
        _animator.SetBool("IsWalking", false);
        _animator.SetTrigger(triggerName);
        StartCoroutine(EndRandomAnimation());

    }


    private void Move()
    {
        transform.Translate(_currentDirection * _moveSpeed * Time.deltaTime);
        _directionTimer += Time.deltaTime;
        if (_directionTimer >= _directionChangeTime)
        {
            ChangeDirection();
            _directionTimer = 0;
        }

    }
    private void ChangeDirection()
    {
        _currentDirection = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        if (_spriteRenderer != null) // Защита от NullReference
            _spriteRenderer.flipX = _currentDirection.x < 0;
    }
    private void Update()
    {
        if (!_isRandomAnimationPlay)
        {
            _animator.SetBool("IsWalking", true );
            Move();
        }

    }

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeDirection();
        _animator = GetComponent<Animator>();
        PlayRandomAnimation();
    }
}
