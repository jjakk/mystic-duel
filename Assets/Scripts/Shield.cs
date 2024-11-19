using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fireball"))
        {

            Destroy(collision.gameObject);

            Player player = GetComponentInParent<Player>();
            player.deactivateShield();
        }
    }
}
