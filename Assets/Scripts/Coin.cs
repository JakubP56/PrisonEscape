using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinClip;
    [SerializeField] int coinScore = 20;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player")
        {
            FindObjectOfType<GameSession>().AddScore(coinScore);
            AudioSource.PlayClipAtPoint(coinClip,Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
