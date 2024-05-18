using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AdaptivePerformance;

namespace Health
{
    public class BoatHealth : MonoBehaviour
    {


         float currentHealth;
        // permet de recuperer les zone de vie du bateau 
        public DamageZoneHealth[] zoneHealths;
        UiBoatHealth uiBoatHealth;
       // permet d'ajouter ou d'enlever les zones de vie du bateau
        List<DamageZoneHealth> zoneHealthList = new List<DamageZoneHealth>();

        public float Health {
            get { return currentHealth ; } 
            set { 
                currentHealth = value;
                currentHealth = Mathf.Clamp(currentHealth, 0, zoneHealths[0].zoneHealth + zoneHealths[1].zoneHealth + zoneHealths[2].zoneHealth); 
                UpdateValue();
            }
        }


      
        void Start()
        {
          uiBoatHealth = FindFirstObjectByType<UiBoatHealth>();
           Health = zoneHealths[0].zoneHealth + zoneHealths[1].zoneHealth + zoneHealths[2].zoneHealth;
           // rempli la liste
            foreach (DamageZoneHealth health in zoneHealths)
            {
                zoneHealthList.Add(health);

            }
           
        }

        // Update is called once per frame
        void Update()
        {
           

            // permet de debugger//

          //  Debug.Log(Health);
          /*  Debug.Log("left a : " + ZoneHealths[0].zoneHealth + "   " +
                "middle a : " + ZoneHealths[1].zoneHealth + "   " +
                "right a : " + ZoneHealths[2].zoneHealth);
            */
            // debug de la vie avec l'ui pour tester 
            if (UnityEngine.InputSystem.Keyboard.current.sKey.wasPressedThisFrame)
            {
                         int i = Random.Range(0, zoneHealthList.Count);
               
                          zoneHealthList[i].TakeDamage(10);
                        //a ne pas oublier dois perdre la meme quantitée que take damamge
                      Health -= 10;

                // retire une zone si elle est a zero
                foreach (DamageZoneHealth Zonehealth in zoneHealths)
                {
                    if (Zonehealth.zoneHealth <= 0)
                    {
                        zoneHealthList.Remove(Zonehealth);  
                    }

                }

            }

        }

        public void UpdateValue()
        {
           
            uiBoatHealth.UiValue(zoneHealths, Health);
        }
    }
}
