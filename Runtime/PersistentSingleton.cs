using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Helper
{
    public sealed class PersistentSingleton : MonoBehaviour
    {
        [SerializeField] private string id;
        [SerializeField] private bool overideWithNewOne;

        public static Dictionary<string, PersistentSingleton> Instances { get; private set; } = new Dictionary<string, PersistentSingleton>();

        private void Awake()
        {
            if (Instances.TryGetValue(id, out PersistentSingleton other))
            {
                if (overideWithNewOne)
                {
                    Destroy(other.gameObject);
                    Debug.LogWarning($"Overiding {nameof(PersistentSingleton)} with id {id}", this);
                }
                else
                {
                    Destroy(gameObject);
                    Debug.LogWarning($"More than one {nameof(PersistentSingleton)} with id {id} in scene, deleting younger one", other);
                    return;
                }
            }

            Instances.Add(id, this);
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            Instances.Remove(id);
        }
    }
}
