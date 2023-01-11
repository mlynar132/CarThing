using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] private float maxSpeed = 40f;
    [SerializeField] private float accelerationFactor = 30.0f;
    [SerializeField] private float turnFactor = 3.5f;
    [SerializeField] private float driftFactor = 0.98f;


    private float accelerationInput = 0f;
    private float steeringInput = 0f;
    private float rotationAngle = 0f;
    private float upVelocity;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    private void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    private void ApplyEngineForce()
    {
        upVelocity = Vector2.Dot(transform.up, rb.velocity);

        Debug.Log(rb.velocity.magnitude + " " + upVelocity);

        if (accelerationInput > 0 && upVelocity > maxSpeed)
        {
            return;
        }
        if (accelerationInput < 0 && upVelocity < -maxSpeed * 0.5f)
        {
            return;
        }

        if (accelerationInput == 0)
        {
            rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.deltaTime * 3);
        }
        else
        {
            rb.drag = 0;
        }
        Vector2 engineForce = transform.up * accelerationInput * accelerationFactor;
        rb.AddForce(engineForce, ForceMode2D.Force);
    }
    private void ApplySteering()
    {
        float minTurningSpeed = (rb.velocity.magnitude / 2);
        minTurningSpeed = Mathf.Clamp01(minTurningSpeed);
        rotationAngle -= steeringInput * turnFactor * minTurningSpeed;
        //rotationAngle %= 360; make it betttwen 0 and 360 degrees
        rb.MoveRotation(rotationAngle);
    }

    private void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);

        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetaccelerationInput(float value)
    {
        accelerationInput = value;
    }
    public void SetSteeringInput(float value)
    {
        steeringInput = value;
    }
}
