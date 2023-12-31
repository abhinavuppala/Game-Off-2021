using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTarget : MonoBehaviour
{
    // when an object with the DoorTarget class is hit,
    // TakeDamage will be called
    // therefore, it needs to be a public function

    public float health = 50f;
    public ParticleSystem destructionParticles;
    public Door door;

    public void TakeDamage(float amt)
    {
        health -= amt;
        if (health <= 0f)
        {
            door.Open();
            Die();
        }
    }

    void Die()
    {
        // creates a new parentless particle system at the same spot which plays particles as gameObject is destroyed
        // to change a Quaternion you multiply it by the change, NOT add
        ParticleSystem destroy = Instantiate(destructionParticles, transform.position, transform.rotation * Quaternion.Euler(0, 90, 0), null);
        destroy.Play();
        Destroy(gameObject);
    }
}
