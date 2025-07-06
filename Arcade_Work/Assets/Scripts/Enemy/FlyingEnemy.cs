using System.Collections;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    /*
    enum EnemyState
    {
        Fly,
        Shoot,
        Idle
    }
    */ // ENUM az Update-megoldashoz kell

    [SerializeField] float speed = 15;
    [SerializeField] float randomRadius = 10f;
    [SerializeField] float minWaitingTime = 0f;
    [SerializeField] float maxWaitingTime = 1f;

    [SerializeField] Projectile projectile;

    void OnEnable()
    {
        StartCoroutine(LifeCycle());
    }

    void Osable()
    {
        StopAllCoroutines();       
    }

    IEnumerator LifeCycle()
    {
        while (true)
        {
            foreach (var v in MovePhase())
            {
                yield return v;
            }

            SpaceshipController spaceShip = FindAnyObjectByType<SpaceshipController>();
            Vector3 direction = (spaceShip.transform.position - transform.position).normalized;
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            Quaternion rotation = Quaternion.Euler(0,0,angle);
            Instantiate(projectile, transform.position, rotation);

            float waitTime = Random.Range(minWaitingTime, maxWaitingTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerable MovePhase()
    {
        Vector2 targetPoint = Random.insideUnitCircle * randomRadius;
        while ((Vector2)transform.position != targetPoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, randomRadius);
    }

    /*
    EnemyState currentState = EnemyState.Idle;
    float timer;
    Vector2 targetPoint;

    void Update()
    {
        if (currentState == EnemyState.Idle)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                currentState = EnemyState.Fly;
                targetPoint = Random.insideUnitCircle * randomRadius;

            }

        }
        else if (currentState == EnemyState.Fly)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetPoint) == 0)
            {
                currentState = EnemyState.Shoot;
            }
        }
        else if (currentState == EnemyState.Shoot)
        {
            Debug.LogError("BOOOMMMM");
            timer = Random.Range(minWaitingTime, maxWaitingTime);
            currentState = EnemyState.Idle;
        }
    }
    */
}
