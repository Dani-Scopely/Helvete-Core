using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Com.Frozenbullets.Services
{
	public class ServiceLocator : IServiceLocator
	{
		private IDictionary<Type, IService> m_services;
		private bool m_isDebug = false;

		private static readonly Lazy<ServiceLocator> m_instance = new Lazy<ServiceLocator>(() => new ServiceLocator());

		ServiceLocator()
		{
			m_services = new Dictionary<Type, IService>();

			Log("ServiceLocator diccionary has been created.");
		}

		public bool IsDebug { get { return m_isDebug; } set { m_isDebug = value; } }

		public static ServiceLocator Instance { get { return m_instance.Value; } }

		public T GetService<T>()
		{
			try
			{
				Log("Returning ServiceLocator instance.");
				return (T)m_services[typeof(T)];
			}
			catch (KeyNotFoundException)
			{
				throw new ApplicationException("The requested service is not registered");
			}
		}

		public void Register(IService service)
		{
			Type t = service.GetType();

			if (m_services.ContainsKey(t))
			{
				return;
			}

			Log(string.Format("Trying to add a new service {0}.", service.Name));
			m_services.Add(service.GetType(), service);
			Log(string.Format("Service {0} added.", service.Name));
			m_services[service.GetType()].Init();
			Log(string.Format("Service {0} initialized.", service.Name));
		}

		public void Unregister(IService service)
		{
			Log(string.Format("Trying to delete service {0}.", service.Name));
			Type t = service.GetType();

			if (m_services.ContainsKey(t))
			{
				Log(string.Format("Service {0} has been deleted.", service.Name));
				m_services.Remove(t);
			}
		}

		private void Log(string message)
		{
			if (!m_isDebug)
				return;

			Debug.Log(string.Format("[Com.Frozenbullets.Services]: {0}", message));
		}
	}
}