using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [SerializeField] float acceleration = 10f;
    [SerializeField] float maxSpeed = 25f;
    [SerializeField] float angularSpeed = 360;
    [SerializeField] float drag = 1f;

    Vector3 velocity;

    void Update()
    {
        transform.position += velocity * Time.deltaTime;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Rotate(0, 0, -horizontalInput * angularSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 accelerationVector = acceleration * transform.up * verticalInput;

        velocity += accelerationVector * Time.fixedDeltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        Vector3 dragVector = -velocity * drag;
        velocity += dragVector * Time.fixedDeltaTime;
    }

}
