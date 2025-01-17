using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy,IReSpond
{
    public Attack attack2;
    private Character character;
    public Animator hitAnimator;

    public bool CanReSpond;
    
    protected override void Awake()
    {
        base.Awake();
        patrolState = new BossRunState();
        bossAttack2 = new BossAttack2State();
        bossAttack1 = new BossAttack1State();
        dead = new BossDeadState();
        hurt = new BossHurtState();
        character = GetComponent<Character>();
    }

    protected override void Update()
    {
        
        faceDirNoNormalized = new Vector3(transform.localScale.x, 0, 0);
        if(faceDirNoNormalized.x>0)faceDir=new Vector3(1, 0, 0);
        else faceDir=new Vector3(-1, 0, 0);
        
        currentState.Update();
    }
    
    void OnAnimationComplete()
    {
        int randomValue = UnityEngine.Random.Range(0, 2);
        Vector3 targetPosition=Vector3.zero;
        if (randomValue == 0)
        {
            targetPosition=attacker.transform.position+new Vector3(5,0,0);
        }
        else if (randomValue == 1)
        {
            targetPosition=attacker.transform.position+new Vector3(-5,0,0);
        }
        transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
        
        if (attacker.position.x >= transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(faceDirNoNormalized.x),transform.localScale.y ,transform.localScale.z );
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(faceDirNoNormalized.x),transform.localScale.y ,transform.localScale.z );
        }

        StartCoroutine(WaitSeconds());
        
    }

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(1f);
        distanceToPlayer=Vector2.Distance(transform.position, attacker.position);
        if (distanceToPlayer >= 5)
        {
            SwitchState(NPCState.BossAttack1);
        }
        else
        {
            SwitchState(NPCState.Patrol);
        }
    }
    
    public void BossAttack2()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position + (Vector3)centerOffset, faceDir, checkDistance/3, attackLayer);
        if (hit1.collider != null)
        {
            attacker = hit1.transform;
            Debug.Log(attacker.name);
            attacker.GetComponent<Character>()?.GetDamage(attack2);
        }
    }

    public void BossDead()
    {
        SwitchState(NPCState.Dead);
        isDead = true;

    }

    public void BossHurt()
    {
        if (!character.isHurt)
        {
            SwitchState(NPCState.Hurt);
            character.isHurt = true;
            character.hurtCounter = character.hurtTime;
        }
        
    }

    public bool CheckReSpond()
    {
        if (CanReSpond)
        {
            //击退
            return true;
        }
        
        return false;
    }

    public void SetReSpond() => CanReSpond = true;

    public void StopReSpond() => CanReSpond = false;
    
    public void GetHit()
    {
        faceDir = new Vector2(-Player.Instance.playerController.Facing, 1);
        GetComponent<Rigidbody2D>().AddForce(3f * -faceDir, ForceMode2D.Impulse);
        hitAnimator.SetTrigger("Hit");
    }
}
