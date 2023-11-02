using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
   protected override void Attack(float percent)
   {
        print("My weapon attacked: " + percent);
   }
}
