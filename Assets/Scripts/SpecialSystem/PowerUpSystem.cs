using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUpSystem : MonoBehaviour
{
    public PlayerInputControl inputActions;
    public GameObject background1;
    public Character player;
    public Contants constants;
    private List<Enemy> enemies = new List<Enemy>();

    public bool powerUpActive = false;
    public bool powerUpCooldown = false;
    private float powerUpDuration = 10f;
    public float powerUpTimer = 0f;
    private Dictionary<Enemy, EnemyData> originalEnemyData = new Dictionary<Enemy, EnemyData>();
    private PlayerData originalPlayerData;

    // 存储 Enemy 的原始数据
    private class EnemyData
    {
        public float attackPower;
        public float currentSpeed;
    }

    // 存储 Player 的原始数据
    private class PlayerData
    {
        public PlayerData(float _attackPower, float _currentSpeed, float _defensePower)
        {
            attackPower = _attackPower;
            currentSpeed = _currentSpeed;
            defensePower = _defensePower;
        }
        public float attackPower;
        public float currentSpeed;
        public float defensePower;
    }

    private void Awake()
    {
        inputActions = new PlayerInputControl();
    }

    private void Start()
    {
        // 查找玩家对象并获取组件
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<Character>();
            if (player == null)
            {
                Debug.LogError("Player not found!");
                return;
            }
        }
        else
        {
            Debug.LogError("Player object not found!");
            return;
        }

        // 存储玩家的原始数据
        originalPlayerData = new PlayerData(player.attackPower, constants.MaxRun, player.defensePower);

        // 查找场景中的各种敌人
        FindEnemies();
    }

    private void FindEnemies()
    {
        RedEnemy[] redEnemies = GameObject.FindObjectsOfType<RedEnemy>();
        BlueEnemy[] blueEnemies = GameObject.FindObjectsOfType<BlueEnemy>();
        Boss[] bosses = GameObject.FindObjectsOfType<Boss>();
        enemies.AddRange(redEnemies);
        enemies.AddRange(blueEnemies);
        enemies.AddRange(bosses);
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Gameplay.PowerUp.started += OnActionTriggered;
    }

    private void OnDisable()
    {
        inputActions.Gameplay.PowerUp.started -= OnActionTriggered;
        inputActions.Disable();
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (!powerUpActive && !powerUpCooldown)
            {
                Debug.Log("press R and power up");
                TriggerPowerUp();
            }
        }
    }

    private void TriggerPowerUp()
    {
        if (background1 == null)
        {
            Debug.LogError("Background1 not found!");
            return;
        }

        background1.SetActive(false);
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
        constants.MaxRun *= 1.1f;
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
        constants.MaxRun = originalPlayerData.currentSpeed;
        player.defensePower = originalPlayerData.defensePower;
        background1.SetActive(true);
        powerUpActive = false;
        powerUpCooldown = false;
    }
}