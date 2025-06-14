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
using System.Diagnostics;

namespace Nugget.Entities
{
    public partial class Plant
    {
        // Tracks how long the plant is growing before it's ready to be harvested.
        float fGrowthTimer = 0f;

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
            GrowSeedToPlant();
        }

        private void CustomDestroy()
        {
            
        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {
            
        }

        private void GrowSeedToPlant()
        {
            // Keeps track of time in seconds.
            fGrowthTimer += TimeManager.SecondDifference;

            // If plant has been planted for 5 seconds...
            if (fGrowthTimer >= 5f)
            {
                // Change color to green.
                PlantCircle.Color = Color.Green;
            }
        }
    }
}
