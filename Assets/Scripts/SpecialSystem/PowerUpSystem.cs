using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUpSystem : MonoBehaviour
{
    public GameObject backgorund2;
    public Character player;  //player攻击力，防御力
    public Contants contants;  //player速度
    private List<Enemy> enemies = new List<Enemy>();

    private bool powerUpActive = false;
    private bool powerUpCooldown = false;
    private float powerUpDuration = 10f;
    private float powerUpTimer = 0f;
    private Dictionary<Enemy, EnemyData> originalEnemyData = new Dictionary<Enemy, EnemyData>();
    private PlayerData originalPlayerData;

    //存储 Enemy 的原始数据
    private class EnemyData
    {
        public float attackPower;
        public float currentSpeed;
    }

    //存储 Player 的原始数据
    private class PlayerData
    {
        public float attackPower;
        public float currentSpeed;
        public float defensePower;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        // 查找所有的 Enemy 子类组件
        RedEnemy[] redEnemies = GameObject.FindObjectsOfType<RedEnemy>();
        BlueEnemy[] blueEnemies = GameObject.FindObjectsOfType<BlueEnemy>();
        Boss[] bosses = GameObject.FindObjectsOfType<Boss>();
        enemies.AddRange(redEnemies);
        enemies.AddRange(blueEnemies);
        enemies.AddRange(bosses);

        originalPlayerData = new PlayerData
        {
            attackPower = player.attackPower,
            currentSpeed = contants.MaxRun,
            defensePower = player.defensePower
        };
    }

    private void OnEnable()
    {
        InputSystem.onActionTriggered += OnActionTriggered;
    }

    private void OnDisable()
    {
        InputSystem.onActionTriggered -= OnActionTriggered;
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        if (context.action.name == "R")
        {
            if (context.phase == InputActionPhase.Performed)
            {
                if (!powerUpActive && !powerUpCooldown)
                {
                    TriggerPowerUp();
                }
            }
        }
    }

    private void TriggerPowerUp()
    {
        if (backgorund2 == null)
        {
            Debug.LogError("Backgorund2 not found!");
            return;
        }

        backgorund2.SetActive(false);
        powerUpActive = true;
        powerUpCooldown = true;
        powerUpTimer = powerUpDuration;

        // 存储原始数据
        originalEnemyData.Clear();
        foreach (var enemy in enemies)
        {
            originalEnemyData[enemy] = new EnemyData
            {
                attackPower = enemy.attackPower,
                currentSpeed = enemy.currentSpeed
            };
            enemy.attackPower *= 0.5f;
            enemy.currentSpeed *= 0.8f;
        }
        player.attackPower *= 1.25f;
        contants.MaxRun *= 1.1f;
        player.defensePower *= 1.25f;
    }

    private void Update()
    {
        if (powerUpActive)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0)
            {
                RestoreOriginalValues();
            }
        }
    }

    private void RestoreOriginalValues()
    {
        foreach (var pair in originalEnemyData)
        {
            pair.Key.attackPower = pair.Value.attackPower;
            pair.Key.currentSpeed = pair.Value.currentSpeed;
        }
        player.attackPower = originalPlayerData.attackPower;
        contants.MaxRun = originalPlayerData.currentSpeed;
        player.defensePower = originalPlayerData.defensePower;
        backgorund2.SetActive(true);
        powerUpActive = false;
        powerUpCooldown = false;
    }
}