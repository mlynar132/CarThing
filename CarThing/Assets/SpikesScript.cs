using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    [SerializeField] private float dmg = 25f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IKillable _interfaceOfTarget = collision.gameObject.GetComponent<IKillable>();
            if (_interfaceOfTarget != null)
            {
                _interfaceOfTarget.TakeDamage(dmg);
            }
        }
    }
}
