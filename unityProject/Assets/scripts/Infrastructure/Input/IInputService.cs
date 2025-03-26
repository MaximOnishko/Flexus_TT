using System;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public interface IInputService : IService
  {
    public Action OnClickFire { get; set; }
    public Action<Vector2> OnRotate { get; set; }

    public void Fire();
    public void Rotate(Vector2 dir);
  }
}