using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Helper
{
    public sealed class PersistentGameObject : MonoBehaviour
    {
        [SerializeField] private string id;
        [SerializeField] private bool overideWithNewOne;

        public static Dictionary<string, PersistentGameObject> Instances { get; private set; } = new Dictionary<string, PersistentGameObject>();

        private void Awake()
        {
            if (Instances.TryGetValue(id, out PersistentGameObject other))
            {
                if (overideWithNewOne)
                {
                    Destroy(other.gameObject);
                    Debug.LogWarning($"Overiding {nameof(PersistentGameObject)} with id {id}", this);
                }
                else
                {
                    Destroy(gameObject);
                    Debug.LogWarning($"More than one {nameof(PersistentGameObject)} with id {id} in scene, deleting younger one", other);
                    return;
                }
            }

            Instances[id] = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            if (Instances.TryGetValue(id, out PersistentGameObject value) && value == this)
                Instances.Remove(id);
        }
    }
}
