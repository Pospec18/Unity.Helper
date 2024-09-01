using System;
using UnityEngine;

namespace Pospec.Helper
{
    public class ReturnSingleton : MonoBehaviour
    {
        public Action returnCallback;

        private void OnDestroy()
        {
            returnCallback?.Invoke();
        }
    }
}
