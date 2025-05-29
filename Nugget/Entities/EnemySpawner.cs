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

            // new code:
            this.EnemiesPerSecond += TimeManager.SecondDifference * this.SpawnRateIncrease;
        }

        private void PerformSpawn()
        {
            Vector3 position = GetRandomEnemyPosition();
            Vector3 velocity = GetRandomEnemyVelocity(position);

            EnemyBase enemy = Factories.EnemyBaseFactory.CreateNew();
            enemy.Position = position;
            enemy.Velocity = velocity;

            lastSpawnTime = TimeManager.CurrentScreenTime;
        }

        private Vector3 GetRandomEnemyPosition()
        {
            // 1. Pick the top, right, bottom, or left.  These values will be 0, 1, 2, 3 respectively

            // The argument 4 is exclusive, so this will return 0,1,2, or 3
            int randomSide = FlatRedBallServices.Random.Next(4);

            // 2. Pick a random point on the side.  We'll do this by getting min and max X and Y values.  
            // Two of the values will always be the same.  
            // In other words, the min and max X on the left side will always be equal.

            // Let's get the absolute coordinates of the edge of the screen:
            float topEdge = Camera.Main.AbsoluteTopYEdgeAt(0);
            float bottomEdge = Camera.Main.AbsoluteBottomYEdgeAt(0);
            float leftEdge = Camera.Main.AbsoluteLeftXEdgeAt(0);
            float rightEdge = Camera.Main.AbsoluteRightXEdgeAt(0);

            // Now let's set the values according to randomSide
            float minX = 0;
            float maxX = 0;
            float minY = 0;
            float maxY = 0;
            switch (randomSide)
            {
                case 0: // top
                    minX = leftEdge;
                    maxX = rightEdge;
                    minY = topEdge;
                    maxY = topEdge;
                    break;
                case 1: // right
                    minX = rightEdge;
                    maxX = rightEdge;
                    minY = bottomEdge;
                    maxY = topEdge;
                    break;
                case 2: // bottom
                    minX = leftEdge;
                    maxX = rightEdge;
                    minY = bottomEdge;
                    maxY = bottomEdge;
                    break;
                case 3: // left
                    minX = leftEdge;
                    maxX = leftEdge;
                    minY = bottomEdge;
                    maxY = topEdge;
                    break;
            }

            // Now we can pick our point randomly using the min and max values:
            float offScreenX = FlatRedBallServices.Random.Between(minX, maxX);
            float offScreenY = FlatRedBallServices.Random.Between(minY, maxY);

            // 3.  Finally we move the point off-screen, since the value right now will be right on the border

            // Our largest Rock is 128x128.  Since rocks are positioned at their center, we only need
            // to move half of that amount (64) to guarantee that rocks spawn fully off-screen.
            float amountToMoveBy = 64;
            switch (randomSide)
            {
                case 0: // top
                    offScreenY += amountToMoveBy;
                    break;
                case 1: // right
                    offScreenX += amountToMoveBy;
                    break;
                case 2: // bottom
                    offScreenY -= amountToMoveBy;
                    break;
                case 3: // left
                    offScreenX -= amountToMoveBy;
                    break;
            }

            // Now we can return the value
            return new Vector3(offScreenX, offScreenY, 0);
        }

        private Vector3 GetRandomEnemyVelocity(Vector3 position)
        {
            // 1.  Find the center of the screen.

            // First we need to get the direction that we want to move.  We can do this
            // by subtracting the argument position from the very center of our game screen.
            // We get the center by using the Camera's X and Y, but not its Z, because the camera is
            // positioned above the game screen looking down at it.
            Vector3 centerOfGameScreen = new Vector3(Camera.Main.X, Camera.Main.Y, 0);

            // 2.  Get the direction towards the center of the screen
            Vector3 directionToCenter = centerOfGameScreen - position;

            // 3.  Normalize the direction, then multiply it by the desired speed.
            // We "normalize" it, which means we make the vector have a length of 1
            // Once it is normalized, we can multiply it by the speed that we want
            // the Rock to move at to get to get the final Velocity value
            directionToCenter.Normalize();

            float speed = MinVelocity +
                FlatRedBallServices.Random.Between(MinVelocity, MaxVelocity);

            return speed * directionToCenter;
        }

        private void CustomDestroy()
        {
            
        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {
            
        }
    }
}
