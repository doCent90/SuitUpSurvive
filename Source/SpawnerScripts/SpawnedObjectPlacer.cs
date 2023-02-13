using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Source
{
    [Serializable]
    public class SpawnedObjectPlacer
    {
        [SerializeField] private Vector2 RangeX;
        [SerializeField] private Vector2 RangeY;

        public void SetPosition(Presenter obj) => obj.transform.position = GetRandonPosition();

        private Vector3 GetRandonPosition()
        {
            float rndX = Random.Range(RangeX.x, RangeX.y);
            float rndZ = Random.Range(RangeY.x, RangeY.y);

            Vector3 position = new Vector3(rndX, 0, rndZ);

            return position;
        } 
    }
}
