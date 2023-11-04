using UnityEngine;

namespace Interface
{
    public interface IBullet
    {
        void Move();
        void OnCollisionEnter(Collision other);
    }
}
