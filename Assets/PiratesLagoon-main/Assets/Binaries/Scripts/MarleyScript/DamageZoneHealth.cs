using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    public class DamageZoneHealth : MonoBehaviour
    {

        public float zoneHealth;

       
        public void TakeDamage(float damage)
        {
            zoneHealth -= damage;
        }
    }
}
