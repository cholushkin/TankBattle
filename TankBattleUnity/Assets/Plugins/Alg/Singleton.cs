using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Alg
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly string _nameFormat = "{0}.Singleton";
        private static readonly object _lock = new object();
        private static bool _isApplicationQuiting;

        public static T Instance
        {
            get
            {
                if(_isApplicationQuiting)
                    return null;
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        var singletons = FindObjectsOfType(typeof(T));
                        Assert.IsTrue(singletons.Length <= 1, "we have somekind of problem, more than one singletons are on the scene");

                        if (singletons.Length == 0)
                        {
                            GameObject singleton = new GameObject(CookNameOfSingleton());
                            _instance = singleton.AddComponent<T>();
                            DontDestroyOnLoad(singleton);
                        }
                        else
                        {
                            _instance = singletons[0] as T;
                        }
                    }

                    return _instance;
                }
            }
        }

        protected static void AssignInstance(T refToObj)
        {
            _instance = refToObj;
        }

        static string CookNameOfSingleton()
        {
            var className = typeof (T).Name;
            if (className.EndsWith("Singleton"))
                className = className.Remove(className.Length - 9);
            return String.Format(_nameFormat, className);
        }

        //// note: when Unity quits, it destroys objects in a random order.
        //// A Singleton is only destroyed when application quits.
        //// If any script calls Instance after it have been destroyed, 
        //// it will create a buggy ghost object that will stay on the Editor scene
        //// even after stopping playing the Application 
        //public void OnDestroy()
        //{
        //    if (LogChecker.Verbose())
        //        Debug.Log("Singleton:OnDestroy: " + _instance.gameObject.name);
        //    _isApplicationQuiting = true;
        //}
    }
}
