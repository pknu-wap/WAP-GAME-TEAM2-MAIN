using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public EntityBaseInfo entityBaseInfo;
    public float StartHealth;
    public float Health;

    public Transform Target;
    public GameObject MoveSpeed;




    public GameObject HealthBar; 
<<<<<<< HEAD
   public GameObject DamageText;
   public GameObject TextPos;
   public GameObject mov;
   public GameObject MakeSlow;
=======
    public GameObject DamageText;
    public GameObject TextPos;
    public GameObject mov;
>>>>>>> 2cb37740641365659c9b395783d53eaf41a645bf

   public void GetDamage(float damage)
   {
       GameObject dmgText = Instantiate(DamageText, TextPos.transform.position, Quaternion.identity);
       dmgText.GetComponent<Text>().text = damage.ToString();
       Health -= damage;
       HealthBar.GetComponent<Image>().fillAmount = Health / StartHealth;
       Destroy(dmgText, 1f);

       
       
   }

   public void Getunderspeed(float down)
   {
        MoveSpeed.GetComponent<ArtificalMovement>();

       
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
