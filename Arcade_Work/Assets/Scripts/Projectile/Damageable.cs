using UnityEngine;

public class Damageable : MonoBehaviour
{
    float maxHealth = 100;
    [SerializeField] Behaviour[] disableWhenDie;


    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float _damage)
    {
        currentHealth -= _damage;
        if (currentHealth <= 0)
        {
            foreach (var n in disableWhenDie)
            {
                n.enabled = false;
            }
            //  disableWhenDie.enabled = false;
        }
    }
}
