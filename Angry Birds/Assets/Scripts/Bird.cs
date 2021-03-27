using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The amount of force to apply when launching the Bird")]
    private float _launchForce = 300f;
    [SerializeField]
    [Tooltip("The colour to apply to the Bird when dragging")]
    private Color _dragColour = Color.red;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Camera _mainCamera;

    private Vector3 _initialPosition;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = true;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _mainCamera = Camera.main;

        _initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        _spriteRenderer.color = _dragColour;
    }

    private void OnMouseUp()
    {
        _spriteRenderer.color = Color.white;

        Vector2 launchDirection = _initialPosition - transform.position;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(launchDirection.normalized * _launchForce);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }
}
