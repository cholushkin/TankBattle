using UnityEngine;
using System.Collections.Generic;
using Alg;
using UnityEngine.Assertions;


namespace Resources
{
    public class PrefabHolder : Singleton<PrefabHolder>
    {
        public List<GameObject> Prefabs;
        private Dictionary<string, GameObject> _name2obj;


        public void Awake()
        {
            PrepareDictionary();
        }

        public GameObject GetPrefab(string name)
        {
            if (_name2obj == null)
                PrepareDictionary();
            Assert.IsTrue(_name2obj.ContainsKey(name), "Doesn't contain key:" + name);
            return _name2obj[name];
        }

        public GameObject Instantiate(string name, Vector3 pos)
        {
            var prefab = GetPrefab(name);
            Assert.IsNotNull(prefab);
            return Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        }

        void PrepareDictionary()
        {
            _name2obj = new Dictionary<string, GameObject>();
            foreach (var prefab in Prefabs)
                if (!_name2obj.ContainsKey(prefab.name))
                    _name2obj.Add(prefab.name, prefab);
                else
                    Debug.LogError("Dup prefab in prefab list " + prefab.name);
        }
    }
}
