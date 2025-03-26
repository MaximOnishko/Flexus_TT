using System;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Input
{
  public interface IInputService : IService
  {
    public Action<Vector2> OnPointerDown { get; set; }
    public Action<Vector2> OnPointerUp { get; set; }
    public Action<Vector2> OnPointerDrag { get; set; }

    public void PointerDown(Vector2 pos);
    public void PointerUp(Vector2 pos);
    public void Drag(Vector2 pos);
  }
}