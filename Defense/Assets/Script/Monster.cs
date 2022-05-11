using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : EntityBaseInfo
{
    public float StartHealth;
    public float Health;

    public GameObject HealthBar; 
   public GameObject DamageText;
   public GameObject TextPos;
   public GameObject mov;

   public void GetDamage(float damage)
   {
       GameObject dmgText = Instantiate(DamageText, TextPos.transform.position, Quaternion.identity);
       dmgText.GetComponent<Text>().text = damage.ToString();
       Health -= damage;
       HealthBar.GetComponent<Image>().fillAmount = Health / StartHealth;
       Destroy(dmgText, 1f);

       if(Health < 0)
       {
           //Destroy(gameObject);
       }
   }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Splash")
        {
            GetDamage(other.GetComponent<Splash>().Damage);
        }
    }
    private void AImove()
    {

        mov.GetComponent<ArtificalMovement>();
    }
}
