using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Helper
{
    public static class Helper
    {
        private static Camera mainCamera;
        public static Camera MainCamera
        {
            get
            {
                if (mainCamera == null)
                    mainCamera = Camera.main;

                return mainCamera;
            }
        }

        private static Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
        private static Dictionary<float, WaitForSecondsRealtime> WaitRealtimeDictionary = new Dictionary<float, WaitForSecondsRealtime>();
        
        public static WaitForSeconds GetWait(float time)
        {
            WaitForSeconds wait;
            if (WaitDictionary.TryGetValue(time, out wait))
                return wait;

            wait = new WaitForSeconds(time);
            WaitDictionary.Add(time, wait);
            return wait;
        }

        public static WaitForSecondsRealtime GetUnscaledWait(float time)
        {
            WaitForSecondsRealtime wait;
            if (WaitRealtimeDictionary.TryGetValue(time, out wait))
                return wait;

            wait = new WaitForSecondsRealtime(time);
            WaitRealtimeDictionary.Add(time, wait);
            return wait;
        }

        /// <summary>
        /// Returns a random value out of this Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T GetRandomEnumValue<T>() where T : Enum
        {
            Array v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(UnityEngine.Random.Range(0, v.Length));
        }
    }
}
