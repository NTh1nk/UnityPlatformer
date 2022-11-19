using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_shoot : MonoBehaviour
{
    [SerializeField] GameObject fireball;
    [SerializeField] float fireRateSec = 2;
    Quaternion rotation;
    Vector3 pos;
    float rateTimeS;
    void Start()
    {
        rateTimeS = 0;
        rotation = transform.rotation * Quaternion.Euler(0, 0, 180);
        pos = new Vector3(transform.position.x, transform.position.y,
        transform.position.z + 0.1f);
    }
    void Update()
    {
        rateTimeS += Time.deltaTime;
        if (rateTimeS > fireRateSec)
        {
            // Shoot
            Instantiate(fireball, pos, rotation);
            rateTimeS = 0;
        }
    }
}
