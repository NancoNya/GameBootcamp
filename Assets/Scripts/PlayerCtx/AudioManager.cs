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

   public void PlayenemyAttack1(AudioSource resource)
   {
      resource.clip = enemyAttack1;
      resource.Play();
   }

   public void PlayenemyAttack2(AudioSource resource)
   {
      audioSource2.clip = enemyAttack2;
      audioSource2.Play();
   }
   
   public void PlayenemyAttack3(AudioSource resource)
   {
      resource.clip = enemyAttack3;
      resource.Play();
   }

   public void PlayenemyHurt1(AudioSource resource)
   {
      resource.clip = enemyHurt1;
      resource.Play();
   }

   public void PlayenemyHurt2(AudioSource resource)
   {
      resource.clip = enemyHurt2;
      resource.Play();
   }

   public void PlayenemyHurt3(AudioSource resource)
   {
      resource.clip = enemyHurt3;
      resource.Play();
   }

   public void PlayenemyRun(AudioSource resource)
   {
      resource.clip = run;
      resource.Play();
   }
  
   public void Playattack1()
   {
      audioSource.PlayOneShot(attack1);
   }

   public void Playattack2()
   {
      audioSource.PlayOneShot(attack2);
   }

   public void Playattack3()
   {
      audioSource.PlayOneShot(attack3);
   }

   public void Playrun()
   {
      audioSource.PlayOneShot(run);
   }

   public void Playwalk()
   {
      audioSource.PlayOneShot(walk);
   }

   public void Playjump()
   {
      audioSource.PlayOneShot(jump);
   }

   public void Playrush()
   {
      audioSource.PlayOneShot(rush);
   }

   public void PlayHeartBreak()
   {
      audioSource.PlayOneShot(HeartBreak);
   }
}
