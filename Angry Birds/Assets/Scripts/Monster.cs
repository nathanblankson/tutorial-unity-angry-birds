using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The sprite to show when the Monster dies")]
    private Sprite _deadSprite;
    [SerializeField]
    [Tooltip("The particle system for the death particles")]
    private ParticleSystem _particleSystem;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private bool _isDead = false;
    private float _disableDelay = 2f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_isDead && ShouldDieFromCollision(other))
        {
            StartCoroutine(Die());
        }
    }

    private bool ShouldDieFromCollision(Collision2D other)
    {
        Bird bird = other.gameObject.GetComponent<Bird>();
        if (bird != null)
        {
            return true;
        }

        // is collision coming from above ~ 45 .deg angle
        if (other.GetContact(0).normal.y < -.5f)
        {
            return true;
        }

        return false;
    }

    IEnumerator Die()
    {
        _isDead = true;
        _animator.enabled = false;
        _spriteRenderer.sprite = _deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(_disableDelay);
        gameObject.SetActive(false);
    }
}
