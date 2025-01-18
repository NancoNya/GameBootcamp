using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSigleton<AudioManager>
{
   public AudioSource BGMSource;
   public AudioSource HeartBreakSource;
   public AudioSource audioSource;
   public AudioSource audioSource2;

   public AudioClip BGM;

   public AudioClip HeartBreak;
   public AudioClip attack1;
   public AudioClip attack2;
   public AudioClip attack3;
   public AudioClip run;
   public AudioClip walk;
   public AudioClip jump;
   public AudioClip rush;

   public AudioClip enemyAttack1;
   public AudioClip enemyAttack2;
   public AudioClip enemyAttack3;
   public AudioClip enemyHurt1;
   public AudioClip enemyHurt2;
   public AudioClip enemyHurt3;
   public AudioClip enemyRun;

   public void Start()
   {
      BGMSource.clip = BGM;
      BGMSource.loop = true;
      BGMSource.Play();
   }

   public void PlayenemyAttack1()
   {
      audioSource2.clip = enemyAttack1;
      audioSource2.Play();
   }

   public void PlayenemyAttack2()
   {
      audioSource2.clip = enemyAttack2;
      audioSource2.Play();
   }
   
   public void PlayenemyAttack3()
   {
      audioSource2.clip = enemyAttack3;
      audioSource2.Play();
   }

   public void PlayenemyHurt1()
   {
      audioSource2.clip = enemyHurt1;
      audioSource2.Play();
   }

   public void PlayenemyHurt2()
   {
      audioSource2.clip = enemyHurt2;
      audioSource2.Play();
   }

   public void PlayenemyHurt3()
   {
      audioSource2.clip = enemyHurt3;
      audioSource2.Play();
   }

   public void PlayenemyRun()
   {
      audioSource2.clip = run;
      audioSource2.Play();
   }
  
   public void Playattack1()
   {
      audioSource.clip = attack1;
      audioSource.Play();
   }

   public void Playattack2()
   {
      audioSource.clip = attack2;
      audioSource.Play();
   }

   public void Playattack3()
   {
      audioSource.clip = attack3;
      audioSource.Play();
   }

   public void Playrun()
   {
      audioSource.clip = run;
      audioSource.Play();
   }

   public void Playwalk()
   {
      audioSource.clip = walk;
      audioSource.Play();
   }

   public void Playjump()
   {
      audioSource.clip = jump;
      audioSource.Play();
   }

   public void Playrush()
   {
      audioSource.clip = rush;
      audioSource.Play();
   }

   public void PlayHeartBreak()
   {
      audioSource.clip = HeartBreak;
      audioSource.Play();
   }
}
