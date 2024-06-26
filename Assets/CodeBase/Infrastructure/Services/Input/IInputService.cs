using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        public event Action<Vector3> ClickPosition;
        public void Enable();
        public void Disable();
    }
}