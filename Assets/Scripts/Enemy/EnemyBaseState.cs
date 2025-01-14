using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{ protected Enemy currentEnemy;
    public abstract void OnEnter(Enemy enemy);
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void OnExit();
}
