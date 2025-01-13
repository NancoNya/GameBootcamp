using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private Character _character;
    public Contants contants;

    private float preSpeed;
    private float curSpeed;
    private float preAttack;
    
    private float addSpeedCooldownTimer;
    private float specialCooldownTimer;
    private float doubleAttackSpecialCooldownTimer;
    private float threeAttackCooldownTimer;
    private float attackCooldownTimer;
    
    private float decreaseHurtCooldownTimer;
    private float decreaseSpeedCooldownTimer;
    
    private void Awake()
    {
        _character = GetComponent<Character>();
        preAttack = _character.attackPower;
        preSpeed = contants.MaxRun;
    }

    private void Update()
    {
        if (addSpeedCooldownTimer > 0)
        {
            addSpeedCooldownTimer -= Time.deltaTime;
        }
        else
        {
            contants.MaxRun = preSpeed;
        }
        
        if (decreaseSpeedCooldownTimer > 0)
        {
            decreaseSpeedCooldownTimer -= Time.deltaTime;
        }
        else
        {
            contants.MaxRun = preSpeed;
        }

        if (specialCooldownTimer > 0)
        {
            specialCooldownTimer -= Time.deltaTime;
            _character.OneHit_Kill = true;
        }
        else
        {
            _character.OneHit_Kill = false;
        }

        if (doubleAttackSpecialCooldownTimer > 0)
        {
            doubleAttackSpecialCooldownTimer -= Time.deltaTime;
            _character.DoubleHurt = true;
        }
        else
        {
            _character.DoubleHurt = false;
            _character.attackPower = preAttack;
        }

        if (threeAttackCooldownTimer > 0)
        {
            threeAttackCooldownTimer -= Time.deltaTime;
            _character.ThreeHurt = true;
        }
        else
        {
            _character.ThreeHurt = false;
        }
        
    }

    private void AddSpeed(float speed)
    {
        contants.MaxRun += speed;
        addSpeedCooldownTimer = 120f;
    }

    private void AddAttack()
    {
        _character.attackPower *= 2f;
        doubleAttackSpecialCooldownTimer = 60f;
    }
    
    private void decreaseSpeed(float speed)
    {
        contants.MaxRun -= speed;
        decreaseSpeedCooldownTimer = 120f;
    }

    private void strAttack(float mult) => _character.attackPower += _character.attackPower * mult;
    
    private void strDeffence(float mult) => _character.defensePower += _character.defensePower * mult;
    
    private void strHealth(float mult) => _character.defensePower += _character.maxHealth * mult;
    
    private void decreaseAttack(float mult) => _character.attackPower -= _character.attackPower * mult;
    
    private void decreaseDeffence(float mult) => _character.defensePower -= _character.defensePower * mult;
    
    private void decreaseHealth(float mult) => _character.maxHealth -= _character.defensePower * mult;
    
    public void GetBuff(int buffID)
    {
        switch (buffID)
        {
            case 0:
            {
                strAttack(0.1f);
                break;
            }
            case 1:
            {
                strAttack(0.15f);
                break;
            }
            case 2:
            {
                strAttack(0.2f);
                break;
            }
            case 3:
            {
                strDeffence(0.1f);
                break;
            }
            case 4:
            {
                strDeffence(0.15f);
                break;
            }
            case 5:
            {
                strDeffence(0.2f);
                break;
            }
            case 6:
            {
                strHealth(0.1f);
                break;
            }
            case 7:
            {
                strHealth(0.15f);
                break;
            }
            case 8:
            {
                strHealth(0.2f);
                break;
            }
            case 9:
            {
                AddSpeed(5f);
                break;
            }
            case 10:
            {
                AddSpeed(10f);
                break;
            }
            case 11:
            {
                specialCooldownTimer = 60f;
                break;
            }
            case 12:
            {
                AddAttack();
                break;
            }
            case 13:
            {
                decreaseHurtCooldownTimer = 120f;
                //ΩµµÕ ‹…À
                break;
            }
            case 14:
            {
                decreaseHurtCooldownTimer = 120f;
                
                break;
            }
            case 15:
            {
                decreaseHurtCooldownTimer = 120f;
                
                break;
            }
            case 16:
            {
                decreaseAttack(0.05f);
                break;
            }
            case 17:
            {
                decreaseAttack(0.1f);
                break;
            }
            case 18:
            {
                decreaseAttack(0.15f);
                break;
            }
            case 19:
            {
                decreaseDeffence(0.05f);
                break;
            }
            case 20:
            {
                decreaseDeffence(0.1f);
                break;
            }
            case 21:
            {
                decreaseDeffence(0.15f);
                break;
            }
            case 22:
            {
                decreaseHealth(0.05f);
                break;
            }
            case 23:
            {
                decreaseHealth(0.1f);
                break;
            }
            case 24:
            {
                decreaseHealth(0.15f);
                break;
            }
            case 25:
            {
                decreaseSpeed(3f);
                break;
            }
            case 26:
            {
                decreaseSpeed(5f);
                break;
            }
            case 27:
            {
                doubleAttackSpecialCooldownTimer = 60f;
                break;
            }
            case 28:
            {
                threeAttackCooldownTimer = 60f;
                break;
            }
            default: break;
                
        }
    }
}
