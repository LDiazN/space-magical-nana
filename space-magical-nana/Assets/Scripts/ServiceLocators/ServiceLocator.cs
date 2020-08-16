using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    [SerializeField]
    private Dictionary<Type, object> _services;
    [HideInInspector]
    [SerializeField]
    private int testeo;

    private void Start()
    {
        Debug.Log(testeo);
        Debug.Log(_services.Count);
        foreach (Type type in _services.Keys)
            Debug.Log(type);
    }

    public bool RegisterService<T>(T service) where T : class
    {
        Type servTpe = service.GetType();
        if (!_services.ContainsKey(servTpe))
        {
            _services.Add(servTpe, service);
            return true;
        }
        return false;
    }


    public T GetService<T>() where T : class
    {
        if (_services.ContainsKey(typeof(T)))
            return (T)_services[typeof(T)];
        return null;
    }


    public bool UnregisterService<T>(T service) where T : class
    {
        if (_services.ContainsKey(service.GetType()))
        {
            if (_services[service.GetType()] == service)
            {
                _services.Remove(service.GetType());
                return true;
            }
        }
        return false;
    }


    public void SwapService<T>(T service) where T : class
    {
        if (!_services.ContainsKey(service.GetType()))
            RegisterService(service);
        _services[service.GetType()] = service;
    }
}
