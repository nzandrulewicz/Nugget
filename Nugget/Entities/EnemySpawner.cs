using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework;

namespace Nugget.Entities
{
    public partial class EnemySpawner
    {
        double lastSpawnTime;
        bool IsTimeToSpawn
        {
            get
            {
                float spawnFrequency = 1 / EnemiesPerSecond;
                return TimeManager.CurrentScreenSecondsSince(lastSpawnTime) > spawnFrequency;
            }
        }

        /// <summary>
        /// Initialization logic which is executed only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
        private void CustomInitialize()
        {
            
        }

        private void CustomActivity()
        {
            if (IsTimeToSpawn)
            {
                PerformSpawn();
            }
        }

        private void PerformSpawn()
        {
            Vector3 position = GetRandomEnemyPosition();
            Vector3 velocity = GetRandomEnemyVelocity(position);

            Enemy enemy = Factories.EnemyFactory.CreateNew();
            enemy.Position = position;
            enemy.Velocity = velocity;

            lastSpawnTime = TimeManager.CurrentScreenTime;
        }

        private void CustomDestroy()
        {
            
        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {
            
        }
    }
}
