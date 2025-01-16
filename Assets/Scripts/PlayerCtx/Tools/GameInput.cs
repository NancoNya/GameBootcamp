using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public enum Facings
{
    Left = - 1,
    Right = 1
}

public struct VisualJoystick
{
    public bool consumed;
    public Vector2 value
    {
        get
        {
            if (consumed)
            {
                return Vector2.zero;
            }
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}

public struct VisualButton
{
    private KeyCode key;
    private float bufferTime;
    private float bufferCount;
    public bool consumed;
    
    public VisualButton(bool consumed = false) : this() {}

    public VisualButton(KeyCode key, float bufferTime)
    {
        this.key = key;
        this.bufferTime = bufferTime;
        this.bufferCount = 0;
        this.consumed = false;
    }

    public void ConsumeBuffer() => bufferCount = 0;

    public bool Pressed() => (Input.GetKeyDown(key) && !consumed) || (!this.consumed && bufferCount > 0);
    
    public bool Checked() => Input.GetKey(key);

    public void Update(float deltaTime)
    {
        //this.consumed = false;
        bufferCount -= deltaTime;
        bool flag = false;
        if (Input.GetKeyDown(key))
        {
            bufferCount = bufferTime;
            flag = true;
        }

        if (Input.GetKey(key))
        {
            flag = true;
        }

        if (!flag)
        {
            bufferCount = 0;
            return;
        }
    }


}

public class GameInput 
{
    public static VisualButton Jump = new VisualButton(KeyCode.Space, 0.08f);
    public static VisualJoystick Aim = new VisualJoystick();
    public static VisualButton Dash = new VisualButton(KeyCode.LeftShift, 0.08f);
    public static VisualButton Attack = new VisualButton(KeyCode.Mouse0, 0.08f);
    public static VisualButton Slide = new VisualButton(KeyCode.LeftControl, 0.08f);
    public static Vector2 LastAim;

    /*public static Vector2 GetAimVector(Facing defaultFacing = Facing.Right)
    {
        Vector2 value = Aim.value;

        if (value == Vector2.zero)
        {
            LastAim = Vector2.right * (int)defaultFacing;
        }
        else
        {
            LastAim = value;
        }
        return LastAim.normalized;
    }*/

    public static void Update(float deltaTime)
    {
        Jump.Update(deltaTime);
    }

    public static void ConsumeAllButtons()
    {
        Jump.consumed = true;
         Aim.consumed = true;
         Dash.consumed = true;
         Attack.consumed = true;
         Slide.consumed = true;
    }

    public static void RefreshAllButtons()
    {
        Jump.consumed = false;
        Aim.consumed = false;
        Dash.consumed = false;
        Attack.consumed = false;
        Slide.consumed = false;
    }
}
