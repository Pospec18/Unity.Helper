using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Helper
{
    public class ExcludeObjectOnPlatforms : MonoBehaviour
    {
        [SerializeField] private List<Platform> excludeOnPlatforms;

        private void Awake()
        {
            if (excludeOnPlatforms.Contains(PlatformInfo.current))
                Destroy(gameObject);
        }
    }
}
