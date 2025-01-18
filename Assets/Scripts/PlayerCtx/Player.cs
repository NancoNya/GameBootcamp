
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

        if (Input.GetKeyDown(KeyCode.Z) && !stop)
        {
            StartCoroutine(QRedDash());
        }

        if (Input.GetKeyDown(KeyCode.Q) && !stop)
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
        if (!Dead)
         playerController.FixedUpdate();
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

    private IEnumerator QRedDash()
    {
        stop = true;
        animator.SetTrigger("RedAttack");
        yield return new WaitForSeconds(1.3f);
        transform.position = new Vector2(transform.position.x + playerController.Facing * 6.45f, transform.position.y);
        yield return new WaitForSeconds(2.5f);
        stop = false;
    }

    private IEnumerator PlayRedContinue()
    {
        stop = true;
        animator.SetTrigger("RedContinue");
        yield return new WaitForSeconds(1.3f);
        stop = false;
    }
}
