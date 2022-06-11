using DG.Tweening;
using UnityEngine;

namespace Battle
{
    public class BulletTesla : Bullet
    {
        public float Radius;

        private void Start()
        {
            transform.DOScale(new Vector3(Radius, Radius, Radius), 0.5f).OnComplete(() => Destroy(gameObject));
        }
    }
}