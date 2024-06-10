using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float lifespan = 2f; // Adjust this value to change the lifespan of the fireball
    private ParticleSystem fireballVFX; // Reference to the Particle System component
  
    private void Start()
    {
      
        fireballVFX = GetComponent<ParticleSystem>();

     
        if (fireballVFX != null)
        {
            fireballVFX.Play();
        }
        else
        {
            Debug.LogWarning("Particle System component is not attached to the fireball.");
        }

        Destroy(gameObject, lifespan);
    }
  
}
