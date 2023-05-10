using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;

namespace Pospec.Helper.Math
{
    /// <summary>
    /// Closed Catmull-Rom curve
    /// </summary>
    public class CatmullRomCurve : MonoBehaviour
    {
        /// <summary>
        /// Points in curve.
        /// Last point is conected to the first point
        /// </summary>
        [SerializeField] private Vector3[] points;
        public float Speed { get; set; }

        /// <summary>
        /// Move and rotate transform on curve
        /// </summary>
        /// <param name="t">time</param>
        /// <param name="transform">transform to be moved</param>
        public void Evaluate(float t, Transform transform)
        {
            Vector3 pos = EvaluatePos(t);
            Vector3 dir = EvaluareDerivation(t);
            transform.Translate(pos);
            transform.LookAt(pos + dir);
        }

        /// <summary>
        /// Creates Matrix for moving transform on curve
        /// </summary>
        /// <param name="t">time</param>
        /// <returns>Matrix that moves and rotates object</returns>
        public Matrix4x4 Evaluate(float t)
        {
            Vector3 pos = EvaluatePos(t);
            Vector3 dir = EvaluareDerivation(t);
            return Transforms.AlignObject(pos, dir, new Vector3(0, 1, 0));
        }

        private Vector3 EvaluatePos(float t)
        {
            int i = (int)(t * Speed);
            int signOfSpeed = System.Math.Sign(Speed);
            return EvaluateSegmentPos(
                points[i % points.Length],
                points[(i + 1) % points.Length],
                points[(i + 2) % points.Length],
                points[(i + 3) % points.Length],
                ((t * Speed) - i) * signOfSpeed);
        }

        private Vector3 EvaluareDerivation(float t)
        {
            int i = (int)(t * Speed);
            int signOfSpeed = System.Math.Sign(Speed);
            return EvaluareSegmentDerivation(
                points[i % points.Length],
                points[(i + 1) % points.Length],
                points[(i + 2) % points.Length],
                points[(i + 3) % points.Length],
                ((t * Speed) - i) * signOfSpeed);
        }

        /// <summary>
        /// Evaluates position on curve segment
        /// </summary>
        /// <param name="p1">first point</param>
        /// <param name="p2">second point</param>
        /// <param name="p3">third point</param>
        /// <param name="p4">last point</param>
        /// <param name="t">time (in range from 0 to 1)</param>
        /// <returns>Position on curve in given time</returns>
        public static Vector3 EvaluateSegmentPos(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
        {
            float t2 = t * t;
            float t3 = t2 * t;
            return (p1 * (-t3 + 2 * t2 - t) + p2 * (3 * t3 - 5 * t2 + 2) + p3 * (-3 * t3 + 4 * t2 + t) + p4 * (t3 - t2)) / 2.0f;
        }

        /// <summary>
        /// Evaluates derivation (forward vector of object) of curve segment
        /// </summary>
        /// <param name="p1">first point</param>
        /// <param name="p2">second point</param>
        /// <param name="p3">third point</param>
        /// <param name="p4">last point</param>
        /// <param name="t">time (in range from 0 to 1)</param>
        /// <returns>Derivation of curve in given time</returns>
        public static Vector3 EvaluareSegmentDerivation(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
        {
            float t2 = 2 * t;
            float t3 = 3 * t * t;
            return (p1 * (-t3 + 2 * t2 - 1) + p2 * (3 * t3 - 5 * t2) + p3 * (-3 * t3 + 4 * t2 + 1) + p4 * (t3 - t2)) / 2.0f;
        }
    }
}
