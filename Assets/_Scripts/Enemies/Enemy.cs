using KBCore.Refs;
using UnityEngine;
using UnityEngine.Splines;

namespace RailShooter
{
    public class Enemy : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private SplineAnimate splineAnimate;
        [SerializeField] private GameObject explosionPrefab;

        private SplineContainer flightPath;

        public SplineContainer FlightPath
        {
            get => flightPath;
            set => flightPath = value;
        }


        private void Update()
        {
            if(splineAnimate != null && splineAnimate.ElapsedTime >= splineAnimate.Duration)
            {
                //TODO: pooling system
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var explosionObject = Instantiate(explosionPrefab,transform.position, Quaternion.identity);
            //TODO: pooling system
            Destroy(gameObject);
            Destroy(explosionObject, 5f);
        }

        private void OnDestroy()
        {
            if(flightPath != null) 
                Destroy(flightPath.gameObject);
        }
    }
}
