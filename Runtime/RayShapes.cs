using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Helper
{
    public abstract class RayShape : MonoBehaviour
    {
        public Color GizmoColor { get; set; } = new Color(0.9f, 1, 0.6f);

        public abstract Collider2D Overlap(LayerMask layer);
        public abstract Collider2D[] OverlapAll(LayerMask layer);
        public bool IsOverlaping(LayerMask layer)
        {
            return Overlap(layer) != null;
        }

        protected virtual void Reset()
        {
            GizmoColor = new Color(0.9f, 1, 0.6f);
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;
        }
    }
}
