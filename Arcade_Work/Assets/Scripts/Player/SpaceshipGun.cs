using UnityEngine;

public class SpaceshipGun : MonoBehaviour
{
    [SerializeField] GameObject[] projectiles; 

    int count;

    public void Shoot()
    {
        int randomBullet = Random.Range(0, 1);
        GameObject p = projectiles[randomBullet];

        Instantiate(p, transform.position, transform.rotation);
        count++;
    }

/*
    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            //GameObject p = projectiles[count % projectiles.Length];

            int randomBullet = Random.Range(0, 1);
            GameObject p = projectiles[randomBullet];


            Instantiate(p, transform.position, transform.rotation);
            count++;

        }
    }
*/
}
