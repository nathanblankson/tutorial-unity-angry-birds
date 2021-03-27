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

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Camera _mainCamera;

    private Vector3 _initialPosition;
    private float _resetAfterIdle = 3f;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = true;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _mainCamera = Camera.main;

        _initialPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBird();
        }
    }

    private void OnMouseDown()
    {
        _spriteRenderer.color = _dragColour;
    }

    private void OnMouseUp()
    {
        _spriteRenderer.color = Color.white;

        Vector2 launchDirection = _initialPosition - transform.position;
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(launchDirection.normalized * _launchForce);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
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

        transform.position = _initialPosition;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(_resetAfterIdle);
        ResetBird();
    }
}
