using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public class UiBoatHealth : MonoBehaviour
    {

       
        public Image currentHealth,leftZoneHealth,middleZoneHealth,rightZoneHealth;



        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void UiValue(DamageZoneHealth[] _damageZoneHealths,float _health)
        {

            
               
            
                leftZoneHealth.fillAmount = _damageZoneHealths[0].zoneHealth / 100;
                middleZoneHealth.fillAmount = _damageZoneHealths[1].zoneHealth / 100;
                rightZoneHealth.fillAmount = _damageZoneHealths[2].zoneHealth / 100;
           


            currentHealth.fillAmount = _health / 300;
           
        }

       
    }
}
