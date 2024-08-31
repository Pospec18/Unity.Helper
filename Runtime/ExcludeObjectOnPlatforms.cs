using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Helper
{
    public class ExcludeObjectOnPlatforms : MonoBehaviour
    {
        [SerializeField] private List<BuildPlatform> excludeOnPlatforms;

        private void Awake()
        {
            if (excludeOnPlatforms.Contains(BuildPlatformInfo.current))
                Destroy(gameObject);
        }
    }
}
