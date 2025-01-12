using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
        public void Start();
        public void Update(float deltaTime);
        public void FixedUpdate();
        public void Exit();
}
