using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAnimatorState : MonoBehaviour {
 
   public AnimatorStateInfo animatorStateInfo;
 
   private Animator animator = null;
   private bool saved = false;
 
   void Start() {
        animator = GetComponent<Animator>();
   }
 
   void OnEnable() {
       if (saved && animator != null) animator.Play(animatorStateInfo.shortNameHash);
   }
 
   void OnDisable() {
       if (animator != null) {
           animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
           saved = true;
       }
   }
}