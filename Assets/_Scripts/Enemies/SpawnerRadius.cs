using UnityEngine;

namespace RailShooter.Enemies
{
    [System.Serializable]
    public class SpawnerRadius
    {
        public float distance;
        public float innerRadius;
        public float outerRadius;
        public float verticalOffset;

        public Vector3 GetRandomPoint()
        {
            float angle = Random.Range(0,Mathf.PI*2f);
            float radius = Random.Range(innerRadius,outerRadius);
            float x =radius * Mathf.Cos(angle);
            float y =radius * Mathf.Sin(angle) + verticalOffset;

            return new Vector3(x,y,distance);
        }
    }
}
