using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source
{
    public class MassiveAttackModule
    {
        private float _range = 6;

        private readonly Transform _directionPoint;

        public Transform[] _directionPointsMassiveAttack = new Transform[0];
        public IReadOnlyList<Transform> DirectionsMassiveAttack => _directionPointsMassiveAttack;

        public MassiveAttackModule(Transform directionPoint) => _directionPoint = directionPoint;

        public void AddedMassiveAttack(float countMassiveAttack)
        {
            if (_directionPointsMassiveAttack.Length > 0)
            {
                for (int i = 1; i < _directionPointsMassiveAttack.Length; i++)
                    Object.Destroy(_directionPointsMassiveAttack[i].gameObject);
            }

            _directionPointsMassiveAttack = new Transform[(int)countMassiveAttack];
            _directionPointsMassiveAttack[0] = _directionPoint;
            float range;

            for (int i = 1; i < countMassiveAttack; i++)
            {
                GameObject direction = new("Direction" + i);
                _directionPointsMassiveAttack[i] = direction.transform;
                direction.transform.parent = _directionPoint;
                direction.transform.rotation = _directionPoint.rotation;

                if (i % 2 == 0)
                    range = _range * -1;
                else
                    range = _range;

                direction.transform.localPosition = Vector3.zero;
                direction.transform.localPosition = new Vector3(range, 0, 20);
                direction.transform.parent = _directionPoint.parent;

                if (i % 2 == 0)
                    _range *= 2;
            }
        }
    }
}
