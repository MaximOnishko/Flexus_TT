using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Input
{
  public class InputService : IInputService
  {
    public Action<Vector2> OnPointerDown { get; set; }
    public Action<Vector2> OnPointerUp { get; set; }
    public Action<Vector2> OnPointerDrag { get; set; }
    
    public void PointerDown(Vector2 pos) => OnPointerDown?.Invoke(pos);
    public void PointerUp(Vector2 pos) => OnPointerUp?.Invoke(pos);
    public void Drag(Vector2 pos) => OnPointerDrag?.Invoke(pos);
  }
}