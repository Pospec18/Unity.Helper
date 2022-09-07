using UnityEngine;
using Pospec.Helper.Hit;

namespace Pospec.Helper.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Character : Deathable
    {
        private Rigidbody2D _rb;
        protected Rigidbody2D Rb
        {
            get
            {
                if (_rb == null)
                    _rb = GetComponent<Rigidbody2D>();
                return _rb;
            }
        }
    }
}
