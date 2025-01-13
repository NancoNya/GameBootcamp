using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EActionState
{
    normal,
    climb,
    dash,
    attack,
    size
}

public abstract class PlayerState 
{
    public PlayerController playerController;
    public EActionState stateStyle;

    public PlayerState(EActionState style, PlayerController ctx)
    {
        this.stateStyle = style;
        this.playerController = ctx;
    }

    public abstract void Start();
    
    public abstract EActionState Update(float deltaTime);
    
    public abstract void FixedUpdate();

    public abstract void Exit();

    public abstract IEnumerator Coroutine();
    
    public abstract bool IsCoroutine();

}

public class FiniteStateMachine<S> where S : PlayerState
{
    public int currState = -1;
    public int prevState = -1;
    public Coroutine curCoroutine;
    
    private S[] states =  new S[(int)EActionState.size];

    public FiniteStateMachine()
    {
        curCoroutine = new Coroutine(true);
    }

    public void Update(float deltaTime)
    {
        State = (int)states[currState].Update(deltaTime);
        if (this.curCoroutine.Active)
        {
            this.curCoroutine.Update(deltaTime);
        }
    }

    public void FixedUpdate()
    {
        states[currState].FixedUpdate();
    }
    
    public void AddState(S s)
    {
        states[(int)s.stateStyle] = s;
    }

    public int State
    {
        get =>  this.currState;
        set
        {
           if (value == this.currState) return;
           
            prevState = this.currState;
            this.currState = value;

            if (prevState != -1)
            {
                states[prevState].Exit();
            }
            states[currState].Start();

            if (states[currState].IsCoroutine())
            {
                curCoroutine.Replace(states[currState].Coroutine());
                return;
            }
            curCoroutine.Cancel();
        }
    }
}
