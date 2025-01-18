using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PowerUpSystem : MonoBehaviour
{
    public GameObject background1;
    private Character playerCharacter; // player攻击力，防御力
    public Contants constants; // player速度

    //进入特殊系统前的数值
    private float playerInitialAttack;
    private float playerInitialDefense;
    private float playerInitialSpeed;

    [Header("特殊系统状态")]
    public bool canTrigger = true;
    public float timer;
    private float triggerDuration; //持续时间

    private void Start()
    {
        playerCharacter = Player.Instance.gameObject.GetComponent<Character>();
        background1 = GameObject.FindGameObjectWithTag("BackGround1");
        triggerDuration = 10f;

        //存储初始值
        playerInitialAttack = playerCharacter.attackPower;
        playerInitialDefense = playerCharacter.defensePower;
        playerInitialSpeed = constants.MaxRun;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && canTrigger)
        {
            Debug.Log("press R");
            // 触发功能
            canTrigger = false;
            background1.SetActive(false);
            Debug.Log("已移除背景一");

            // 修改 Player 属性
            playerCharacter.attackPower = playerInitialAttack * 1.25f;
            playerCharacter.defensePower = playerInitialDefense * 1.25f;
            constants.MaxRun = playerInitialSpeed * 1.1f;

            timer = 0f;
        }

        if (!canTrigger)
        {
            timer += Time.deltaTime;
            if (timer >= triggerDuration)
            {
                // 恢复属性
                playerCharacter.attackPower = playerInitialAttack;
                playerCharacter.defensePower = playerInitialDefense;
                constants.MaxRun = playerInitialSpeed;

                background1.SetActive(true);
                canTrigger = true;
            }
        }
    }
}