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

    [Header("Shot Amounth Settings")]
    [SerializeField, Range(1,10)] int shotsPerAttack = 3;
    [SerializeField] float timeBetweenShots = 0.3f;

    [Space(5)]
    [Header("Draw Gizmos Sphere")]
    [SerializeField] int segment = 32;
    [SerializeField] Color gizmosColor = Color.green;

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
            for (int i = 0; i < shotsPerAttack; i++)
            {

                if (spaceShip != null)
                {
                    Vector3 direction = (spaceShip.transform.position - transform.position).normalized;
                    float angle = Vector2.SignedAngle(Vector2.up, direction);
                    Quaternion rotation = Quaternion.Euler(0, 0, angle);
                    Instantiate(projectile, transform.position, rotation);
                }

                if (i < shotsPerAttack - 1)
                    yield return new WaitForSeconds(timeBetweenShots);
            }

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
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(Vector3.zero, randomRadius);
        CircleGizmo();

    }

    void CircleGizmo()
    {
        Gizmos.color = gizmosColor;
        float steps = 360 / segment;
        Vector3 center = Vector3.zero;
        Vector3 point = center + new Vector3(Mathf.Cos(0f), Mathf.Sin(0f), 0f) * randomRadius;

        for (int i = 0; i <= segment + 1; i++)
        {
            float angle = i * steps * Mathf.Deg2Rad;
            Vector3 newPoint = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * randomRadius;

            Gizmos.DrawLine(point, newPoint);
            point = newPoint;
        }
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
