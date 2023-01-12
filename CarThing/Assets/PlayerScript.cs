using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour, IKillable
{
    [Header("Stats")]
    [SerializeField] private float _maxHp = 100f;
    [SerializeField] private float _currentHp;
    [Header("Car Settings")]
    [SerializeField] private float _maxSpeed = 40f;
    [SerializeField] private float _accelerationFactor = 30.0f;
    [SerializeField] private float _turnFactor = 3.5f;
    [SerializeField] private float _driftFactor = 0.98f;

    private float _accelerationInput = 0f;
    private float _steeringInput = 0f;
    private float _rotationAngle = 0f;
    private float _upVelocity;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rb;

    private void Awake()
    {
        _currentHp = _maxHp;
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    private void ApplyEngineForce()
    {
        _upVelocity = Vector2.Dot(transform.up, _rb.velocity);

        if (_accelerationInput > 0 && _upVelocity > _maxSpeed)
        {
            return;
        }
        if (_accelerationInput < 0 && _upVelocity < -_maxSpeed * 0.5f)
        {
            return;
        }

        if (_accelerationInput == 0)
        {
            _rb.drag = Mathf.Lerp(_rb.drag, 3.0f, Time.deltaTime * 3);
        }
        else
        {
            _rb.drag = 0;
        }
        Vector2 engineForce = transform.up * _accelerationInput * _accelerationFactor;
        _rb.AddForce(engineForce, ForceMode2D.Force);
    }
    private void ApplySteering()
    {
        float minTurningSpeed = (_rb.velocity.magnitude / 2);
        minTurningSpeed = Mathf.Clamp01(minTurningSpeed);
        float forwardBack = 1;
        if (_upVelocity < 0)
        {
            forwardBack = -1;
        }
        _rotationAngle -= _steeringInput * _turnFactor * minTurningSpeed * forwardBack;
        //rotationAngle %= 360; make it betttwen 0 and 360 degrees
        _rb.MoveRotation(_rotationAngle);
    }

    private void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_rb.velocity, transform.up);

        Vector2 rightVelocity = transform.right * Vector2.Dot(_rb.velocity, transform.right);

        _rb.velocity = forwardVelocity + rightVelocity * _driftFactor;
    }

    public void SetaccelerationInput(float value)
    {
        _accelerationInput = value;
    }
    public void SetSteeringInput(float value)
    {
        _steeringInput = value;
    }

    void IKillable.TakeDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp<=0)
        {
            (this as IKillable).Death();
        }

    }

    void IKillable.Death()
    {
        Debug.Log("END");
    }
}
