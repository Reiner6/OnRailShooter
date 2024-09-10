using UnityEngine;
using UnityEngine.Splines;

namespace RailShooter.Enemies
{
    public static class FlightPathFactory
    {
        public static SplineContainer GenerateFlightPath(SpawnerRadius[] radii)
        {
            Vector3[] pathPoints = new Vector3[radii.Length];
            for(int i = 0; i < radii.Length; i++)
            {
                pathPoints[i] = radii[i].GetRandomPoint();
            }

            return CreateFlightPath(pathPoints);
        }

        private static SplineContainer CreateFlightPath(Vector3[] pathPoints)
        {
            GameObject flightPath = new GameObject("FlightPath");

            var container = flightPath.AddComponent<SplineContainer>();
            var spline = container.AddSpline();
            var knots = new BezierKnot[pathPoints.Length];

            for(int i = 0; i < pathPoints.Length; i++)
            {
                knots[i] = new BezierKnot(
                    pathPoints[i],
                    -30 * Vector3.back,
                    30 * Vector3.back);
            }

            spline.Knots = knots;

            return container;
        }
    }
}