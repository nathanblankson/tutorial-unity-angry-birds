using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (ShouldDieFromCollision(other))
        {
            Die();
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

    private void Die()
    {
        Debug.Log("Die");
        gameObject.SetActive(false);
    }
}
