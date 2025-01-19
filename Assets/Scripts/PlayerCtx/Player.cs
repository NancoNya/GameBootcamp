
using System;
using System.Collections;
using UnityEngine;

public class Player : MonoSigleton<Player>,IEffectControl
{
    public PlayerController playerController;
    public BoxCollider2D boxCollider;
    public Rigidbody2D playerRigidbody;
    public Animator animator;
    public Animator RedAttack;
    public Animator RedTail;
    private Character _character;
    
    public Contants contants;
    
    public float FreezeCooldownTimer;
    public bool Dead;
    public bool isHit;
    public bool stop;
    
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        _character = GetComponent<Character>();
    }

    private void Start()
    {
        playerController = new PlayerController(this);
    }

    private void Update()
    {
        float deltaTime = Time.unscaledDeltaTime;

        if (_character.currentHealth < _character.maxHealth * 0.4f)
        {
            StartCoroutine(PlayHeartBreak());
        }
        
        if (Input.GetKeyDown(KeyCode.Z) && !stop && _character.currentFiniteSkill > 0)
        {
            StartCoroutine(QRedDash());
        }

        if (Input.GetKeyDown(KeyCode.Q) && !stop && _character.currentFiniteSkill > 0)
        {
            StartCoroutine(PlayRedContinue());
        }

        if (_character.currentHealth > 0)
        {
            Dead = false;
        }
        else
        {
            Dead = true;
        }
        
        if (!stop)
        {
            if (UpdateTime(deltaTime) && !Dead && !isHit)
            {
                GameInput.Update(deltaTime);
                playerController.Update(deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!Dead && !stop)
         playerController.FixedUpdate();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("buff"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                other.GetComponent<IPropPickup>().Pickup(gameObject);
                Destroy(other.gameObject);
            }
        }
    }

    public bool UpdateTime(float deltaTime)
    {
        if (FreezeCooldownTimer > 0)
        {
            FreezeCooldownTimer = Mathf.Max(0, FreezeCooldownTimer - deltaTime);
            return false;
        }

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;   
        }
        
        return true;
    }

    public void Freeze(float freezeTime)
    {
        FreezeCooldownTimer = Mathf.Max(this.FreezeCooldownTimer, freezeTime);
        if (FreezeCooldownTimer > 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Hurt(float hurtDirection) => StartCoroutine(HurtFoce(hurtDirection));

    public IEnumerator HurtFoce(float hurtDirection)
    {
        isHit = true;
        playerRigidbody.velocity = new Vector2(12f * hurtDirection, 1);
        yield return new WaitForSeconds(0.5f);
        playerRigidbody.velocity = new Vector2(0,0);
        isHit = false;
    }

    public void HurtAni()
    {
        animator.SetTrigger(AniPara.Hurt.ToString());
    }

    public void PlayRedAttack() => RedAttack.SetTrigger("RedAttack");
    
    public void PlayRedTail() => RedTail.SetTrigger("RedTail");

    public void AfterDie()
    {
        transform.tag = "Untagged";
        playerRigidbody.bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private IEnumerator QRedDash()
    {
        _character.currentSkill -= 20f;
        _character.invulnerable = true;
        stop = true;
        animator.SetTrigger("RedAttack");
        yield return new WaitForSeconds(1.3f);
        transform.position = new Vector2(transform.position.x + playerController.Facing * 4f, transform.position.y);
        yield return new WaitForSeconds(0.5f);
        stop = false;
        _character.invulnerable = false;
    }

    private IEnumerator PlayRedContinue()
    {
        _character.currentFiniteSkill -= 30f;
        _character.invulnerable = true;
        stop = true;
        animator.SetTrigger("RedContinue");
        yield return new WaitForSeconds(1.3f);
        stop = false;
        _character.invulnerable = false;
    }

    private IEnumerator PlayHeartBreak()
    {
        AudioManager.Instance.PlayHeartBreak();
        yield return new WaitForSeconds(5f);
    }
}
