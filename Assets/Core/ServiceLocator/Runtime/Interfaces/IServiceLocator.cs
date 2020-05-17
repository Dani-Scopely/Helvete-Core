using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Frozenbullets.Services
{
    public interface IServiceLocator
    {
        T GetService<T>();
        void Register(IService service);
        void Unregister(IService service);

        bool IsDebug { get; set; }
    }
}