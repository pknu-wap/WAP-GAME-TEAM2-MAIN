using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public EntityBaseInfo entityBaseInfo;
    public ArtificalMovement MoveSpeed;
    public float StartHealth;
    public float Health;

    public Transform Target;
    
    
    [SerializeField] float SSpeed = 0.3f;



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
       
       MoveSpeed = MakeSlow.GetComponent<ArtificalMovement>(); 
       transform.Translate(MoveSpeed * SSpeed);  //Getunderspeed 발생시 몬스터 속도 줄임

       Destroy(MoveSpeed, 2f);
       
       
        
       
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
