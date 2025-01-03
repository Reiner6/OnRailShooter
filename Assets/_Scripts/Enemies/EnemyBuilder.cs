﻿using UnityEngine;
using UnityEngine.Splines;

namespace RailShooter.Enemies
{
    public class EnemyBuilder
    {
        Transform flightPathParent;
        Enemy enemyPrefab;
        SplineContainer flightPath;
        SplineAnimate.LoopMode loopMode = SplineAnimate.LoopMode.Once;

        public EnemyBuilder withPrefab(Enemy enemyPrefab)
        {
            this.enemyPrefab = enemyPrefab;
            return this;
        }

        public EnemyBuilder withFlightPath(SplineContainer flightPath)
        {
            this.flightPath = flightPath;
            return this;
        }

        public EnemyBuilder withFlightPathParent(Transform flightPathParent)
        {
            this.flightPathParent = flightPathParent;
            return this;
        }

        public void setPosition(Vector3 position)
        {

        }

        public Enemy build(Transform enemyParent, bool parentless)
        {
            var enemy = Object.Instantiate(enemyPrefab, parentless? null: enemyParent);

            enemy.FlightPath = flightPath;

            if(flightPath != null)
            {
                var splineAnimate = enemy.GetComponent<SplineAnimate>();
                splineAnimate.Container = flightPath;
                splineAnimate.Loop = loopMode;
                splineAnimate.ElapsedTime = 0f;
            }

            if(flightPathParent != null && flightPath != null)
            {
                Transform transform;
                transform = flightPath.transform;
                if(parentless)
                {
                    transform.position = flightPathParent.position;
                    transform.rotation = flightPathParent.rotation;
                }
                else
                {
                    transform.SetParent(flightPathParent);
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                }
            }

            return enemy;

        }
    }
}
