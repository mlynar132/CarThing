using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemysScript : MonoBehaviour, IKillable
{
    [Header("Stats")]
    [SerializeField] private float _maxHp = 100f;
    [SerializeField] private float _currentHp;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float dmg = 1;

    private Vector3 dir;

    [Header("Set up")]
    [SerializeField] private Transform _traget;
    [SerializeField] private Rigidbody2D _rb;
    private void Awake()
    {
        _currentHp = _maxHp;
    }
    void Update()
    {
        dir = (_traget.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward,dir);
    }
    private void FixedUpdate()
    {
        
        _rb.velocity = dir * _speed * Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
       // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            IKillable _interfaceOfTarget = collision.gameObject.GetComponent<IKillable>();
            if (_interfaceOfTarget != null)
            {
                _interfaceOfTarget.TakeDamage(dmg);
            }
        }
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
        //wywal z listy menagera 
        Destroy(this.gameObject);
    }
}
