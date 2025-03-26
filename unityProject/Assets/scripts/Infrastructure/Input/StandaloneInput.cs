using System;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class StandaloneInput : IInputService
    {
        public Action OnClickFire { get; set; }
        public Action<Vector2> OnRotate { get; set; }
        
        public void Fire()
        {
           OnClickFire?.Invoke();
        }

        public void Rotate(Vector2 dir)
        {
            OnRotate?.Invoke(dir);
        }
    }
}