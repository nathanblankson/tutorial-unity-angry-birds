using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The amount of force to apply when launching the Bird")]
    private float _launchForce = 300f;
    [SerializeField]
    [Tooltip("The maximum distance the player can drag the Bird backwards")]
    private float _maxDragDistance = 3f;
    [SerializeField]
    [Tooltip("The colour to apply to the Bird when dragging")]
    private Color _dragColour = Color.red;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Camera _mainCamera;

    private Vector2 _initialPosition;
    private float _resetAfterIdle = 3f;
    private bool _isDraggable = true;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = true;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _mainCamera = Camera.main;

        _initialPosition = _rigidbody2D.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBird();
        }
    }

    private void OnMouseUp()
    {
        if (_isDraggable)
        {
            _isDraggable = false;
            _spriteRenderer.color = Color.white;

            Vector2 launchDirection = _initialPosition - _rigidbody2D.position;
            _rigidbody2D.isKinematic = false;
            _rigidbody2D.AddForce(launchDirection.normalized * _launchForce);
        }
    }

    private void OnMouseDrag()
    {
        if (_isDraggable)
        {
            _spriteRenderer.color = _dragColour;

            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 desiredPosition = mousePosition;

            // Clamp position
            float distance = Vector2.Distance(desiredPosition, _initialPosition);
            if (distance > _maxDragDistance)
            {
                Vector2 direction = (desiredPosition - _initialPosition).normalized;
                desiredPosition = _initialPosition + (direction * _maxDragDistance);
            }
            if (desiredPosition.x > _initialPosition.x)
            {
                desiredPosition.x = _initialPosition.x;
            }

            _rigidbody2D.position = desiredPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        StartCoroutine(ResetAfterDelay());
    }

    public void ResetBird()
    {
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0;
        _rigidbody2D.position = _initialPosition;
        _rigidbody2D.rotation = 0;
        _isDraggable = true;
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(_resetAfterIdle);
        ResetBird();
    }
}
