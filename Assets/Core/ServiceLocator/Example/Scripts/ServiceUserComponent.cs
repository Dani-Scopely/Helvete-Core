using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Frozenbullets.Services.Examples
{
    public class ServiceUserComponent : MonoBehaviour
    {
        void Start()
        {
            ServiceA a = new ServiceA();
            ServiceB b = new ServiceB();

            ServiceLocator.Instance.Register(a);
            ServiceLocator.Instance.Register(b);

            var c = ServiceLocator.Instance.GetService<ServiceA>();
            var d = ServiceLocator.Instance.GetService<ServiceB>();
        }
    }
}