using UnityEngine;

enum ShootingType
{
    Loop,
    SameTime,
    PinPong
}

public class SpaceshipGunController : MonoBehaviour
{
    [Header("Shooting Params")]
    [SerializeField] SpaceshipGun[] guns;
    [SerializeField] KeyCode shootKey = KeyCode.Space;
    [SerializeField] ShootingType shootingType;

    [SerializeField] GameObject[] projectiles; 

    //[Header("Irany alapu loves")]
    //[SerializeField] Vector2 leftDirection = new Vector2(-1, 1);
    //[SerializeField] Vector2 rightDirection = new Vector2(1, 1);
    //[SerializeField] int gunCount = 5;




    int count = 0;
    int direction = 1;
    int pinPongIndex = 0;

    void OnValidate()
    {
        if (guns == null || guns.Length == 0)
            guns = GetComponentsInChildren<SpaceshipGun>(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            KeyShoot();
        }
    }

    void KeyShoot()
    {
        if (shootingType == ShootingType.Loop)
        {
            guns[count % guns.Length].Shoot();

        }
        else if (shootingType == ShootingType.SameTime)
        {
            foreach (var gun in guns)
            {
                GunShoot(gun.transform.position, gun.transform.rotation);//gun.Shoot();
            }
        }
        if (shootingType == ShootingType.PinPong)
        {
            guns[pinPongIndex].Shoot();
            if (guns.Length > 1)
            {
                if (direction > 0 && pinPongIndex == guns.Length - 1)
                {
                    direction = -1;
                }
                else if (direction < 0 && pinPongIndex == 0)
                {
                    direction = 1;
                }
                pinPongIndex += direction;
            }


        }
        count++;
    }
    
    public void GunShoot(Vector3 position, Quaternion rotation)
    {
        int randomBullet = Random.Range(0, 1);
        GameObject p = projectiles[randomBullet];

        Instantiate(p, position, rotation);
        count++;
    }
}
