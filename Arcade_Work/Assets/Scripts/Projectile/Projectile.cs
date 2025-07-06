using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    [SerializeField] float lifeTime = 5f;

    float destroyTimer = 0;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;


        destroyTimer += Time.deltaTime;
        if (destroyTimer >= lifeTime)
        {
            Destroy(gameObject);
        }  
    }


}
