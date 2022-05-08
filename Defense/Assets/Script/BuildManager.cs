using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoSingleton<PhaseManager>
{
   public static BuildManager instance;
   public GameObject SelectNode;
   public GameObject Tower;

   public void Start()
   {
       instance = this;
   }

   public void BuildToTower()
   {
       Instantiate(Tower, SelectNode.transform.position, Quaternion.identity);//생성할 오브젝트, 생성될 위치, 각도
   }

}
