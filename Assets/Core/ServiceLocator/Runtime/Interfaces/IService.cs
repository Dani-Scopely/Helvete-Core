using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Frozenbullets.Services
{
    public interface IService
    {
        void Init();
        string Name { get; }
    }
}