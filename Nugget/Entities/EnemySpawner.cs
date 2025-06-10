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
        double dLastSpawnTime;
        bool bIsTimeToSpawn
        {
            get
            {
                float spawnFrequency = 1 / EnemiesPerSecond;
                return TimeManager.CurrentScreenSecondsSince(dLastSpawnTime) > spawnFrequency;
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
            if (bIsTimeToSpawn)
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

            Enemy enemy = Factories.EnemyFactory.CreateNew();
            enemy.Position = position;
            enemy.Velocity = velocity;

            dLastSpawnTime = TimeManager.CurrentScreenTime;
        }

        private Vector3 GetRandomEnemyPosition()
        {
            // 1. Pick the top, right, bottom, or left.  These values will be 0, 1, 2, 3 respectively

            // The argument 4 is exclusive, so this will return 0,1,2, or 3
            int iRandomSide = FlatRedBallServices.Random.Next(4);

            // 2. Pick a random point on the side.  We'll do this by getting min and max X and Y values.  
            // Two of the values will always be the same.  
            // In other words, the min and max X on the left side will always be equal.

            // Let's get the absolute coordinates of the edge of the screen:
            float fTopEdge = Camera.Main.AbsoluteTopYEdgeAt(0);
            float fBottomEdge = Camera.Main.AbsoluteBottomYEdgeAt(0);
            float fLeftEdge = Camera.Main.AbsoluteLeftXEdgeAt(0);
            float fRightEdge = Camera.Main.AbsoluteRightXEdgeAt(0);

            // Now let's set the values according to randomSide
            float fMinX = 0;
            float fMaxX = 0;
            float fMinY = 0;
            float fMaxY = 0;
            switch (iRandomSide)
            {
                case 0: // top
                    fMinX = fLeftEdge;
                    fMaxX = fRightEdge;
                    fMinY = fTopEdge;
                    fMaxY = fTopEdge;
                    break;
                case 1: // right
                    fMinX = fRightEdge;
                    fMaxX = fRightEdge;
                    fMinY = fBottomEdge;
                    fMaxY = fTopEdge;
                    break;
                case 2: // bottom
                    fMinX = fLeftEdge;
                    fMaxX = fRightEdge;
                    fMinY = fBottomEdge;
                    fMaxY = fBottomEdge;
                    break;
                case 3: // left
                    fMinX = fLeftEdge;
                    fMaxX = fLeftEdge;
                    fMinY = fBottomEdge;
                    fMaxY = fTopEdge;
                    break;
            }

            // Now we can pick our point randomly using the min and max values:
            float fOffScreenX = FlatRedBallServices.Random.Between(fMinX, fMaxX);
            float fOffScreenY = FlatRedBallServices.Random.Between(fMinY, fMaxY);

            // 3.  Finally we move the point off-screen, since the value right now will be right on the border

            // Our largest Rock is 128x128.  Since rocks are positioned at their center, we only need
            // to move half of that amount (64) to guarantee that rocks spawn fully off-screen.
            float fAmountToMoveBy = 64;
            switch (iRandomSide)
            {
                case 0: // top
                    fOffScreenY += fAmountToMoveBy;
                    break;
                case 1: // right
                    fOffScreenX += fAmountToMoveBy;
                    break;
                case 2: // bottom
                    fOffScreenY -= fAmountToMoveBy;
                    break;
                case 3: // left
                    fOffScreenX -= fAmountToMoveBy;
                    break;
            }

            // Now we can return the value
            return new Vector3(fOffScreenX, fOffScreenY, 0);
        }

        private Vector3 GetRandomEnemyVelocity(Vector3 position)
        {
            // 1.  Find the center of the screen.

            // First we need to get the direction that we want to move.  We can do this
            // by subtracting the argument position from the very center of our game screen.
            // We get the center by using the Camera's X and Y, but not its Z, because the camera is
            // positioned above the game screen looking down at it.
            Vector3 v3CenterOfGameScreen = new Vector3(Camera.Main.X, Camera.Main.Y, 0);

            // 2.  Get the direction towards the center of the screen
            Vector3 v3DirectionToCenter = v3CenterOfGameScreen - position;

            // 3.  Normalize the direction, then multiply it by the desired speed.
            // We "normalize" it, which means we make the vector have a length of 1
            // Once it is normalized, we can multiply it by the speed that we want
            // the Rock to move at to get to get the final Velocity value
            v3DirectionToCenter.Normalize();

            float fSpeed = MinVelocity +
                FlatRedBallServices.Random.Between(MinVelocity, MaxVelocity);

            return fSpeed * v3DirectionToCenter;
        }

        private void CustomDestroy()
        {
            
        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {
            
        }
    }
}
