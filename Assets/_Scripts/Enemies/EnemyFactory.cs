using UnityEngine;
using UnityEngine.Splines;

namespace RailShooter.Enemies
{
    public static class EnemyFactory 
    {
       public static Enemy GenerateEnemy(Enemy enemyPrefab, SplineContainer flightPath, Transform enemyParent, Transform flightPathParent, bool parentless = false)
        {
            return new EnemyBuilder()
                .withPrefab(enemyPrefab)
                .withFlightPath(flightPath)
                .withFlightPathParent(flightPathParent)
                .build(enemyParent, parentless);

        }
    }
}
