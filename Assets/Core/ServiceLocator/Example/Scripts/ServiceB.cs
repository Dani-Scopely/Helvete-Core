﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Frozenbullets.Services.Examples
{
    public class ServiceB : IService
    {
        public string Name { get => this.GetType().Name; }

        public void Init()
        {
            Debug.Log("Init "+Name);
        }
    }
}