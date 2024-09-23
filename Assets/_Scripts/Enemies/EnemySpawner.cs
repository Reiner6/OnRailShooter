using UnityEngine;

namespace RailShooter.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] SpawnerRadius[] radii;
        [SerializeField] Enemy enemyPrefab;
        [SerializeField] float spawnInterval = 5f;

        [SerializeField] Transform enemyParent;
        [SerializeField] Transform flightPathParent;

        [SerializeField] bool parentLessSpawn;
        float spawnTimer =5f;


        private void Update()
        {
            if(spawnTimer > spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0;
            }
            spawnTimer += Time.deltaTime;
        }

        private void SpawnEnemy()
        {
            var flightPath = FlightPathFactory.GenerateFlightPath(radii);
            EnemyFactory.GenerateEnemy(enemyPrefab, flightPath, enemyParent, flightPathParent, parentLessSpawn);
        }

        private void OnDrawGizmos()
        {
            foreach(var radius in radii)
            {
                DebugExtension.DrawCircle(this.transform.position + (this.transform.forward* radius.distance) + (this.transform.up * radius.verticalOffset),this.transform.forward,radius.innerRadius);
                DebugExtension.DrawCircle(this.transform.position + (this.transform.forward *radius.distance) + (this.transform.up * radius.verticalOffset), this.transform.forward, radius.outerRadius);

            }
        }
    }
}
