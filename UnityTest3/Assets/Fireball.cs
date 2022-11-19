using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    float maxLifeTimeS = 60000;
    void Update()
    {
        maxLifeTimeS -= Time.deltaTime;
        if (maxLifeTimeS <= 0)
            Destroy(gameObject);
        transform.Translate(0, -5 * Time.deltaTime, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") ||
       (collision.gameObject.layer == LayerMask.NameToLayer("Ground")))
        {
            print(collision.gameObject.name);
            Destroy(gameObject);
        }
    }

}
