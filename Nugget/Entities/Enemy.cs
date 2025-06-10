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
using Nugget.TopDown;
using Nugget.DataTypes;
using System.Diagnostics;

namespace Nugget.Entities
{
    public partial class Enemy
    {
        TopDownAiInput<Enemy> topDownAiInput;

        /// <summary>
        /// Initialization logic which is executed only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
        private void CustomInitialize()
        {
            topDownAiInput = new TopDownAiInput<Enemy>(this);

            // This helps us visualize the path the EnemyBase takes to get to
            // the player
            topDownAiInput.IsPathVisible = false;

            // Use a darker color so it stands out over the bright level tiles
            topDownAiInput.PathColor = Color.Purple;
            this.InitializeTopDownInput(topDownAiInput);
        }

        // Initialize it to a large negative number so the path updates immediately
        double dLastTimePathUpdates = -999;

        // How often the path should update. We do this to improve performance
        double dPathUpdateFrequency = 1;

        /// <summary>
        /// Damage to deal on collide.
        /// </summary>
        public int iEnemyAttackDamage = 10;

        /// <summary>
        /// Enemy health.
        /// </summary>
        public int iEnemyCurrentHealth = 30;

        /// <summary>
        /// Keeps track of how long (in seconds) since the enemy last received damage.
        /// </summary>
        float fTimeSinceLastDamage;

        private void CustomActivity()
        {
            if (TimeManager.CurrentScreenSecondsSince(dLastTimePathUpdates) > dPathUpdateFrequency)
            {
                dLastTimePathUpdates = TimeManager.CurrentScreenTime;
                topDownAiInput.UpdatePath();

                // Remove the first node in the path to prevent backtracking
                if (topDownAiInput.Path.Count > 2)
                {
                    topDownAiInput.Path.RemoveAt(0);
                    topDownAiInput.NextImmediateTarget = topDownAiInput.Path[0];
                }
            }
            topDownAiInput.DoTargetFollowingActivity();
        }

        private void CustomDestroy()
        {
            // The top down input path must be made invisible so it cleans up
            // any shapes it creates in the path:
            // topDownAiInput.IsPathVisible = false;

            Seed seed = Factories.SeedFactory.CreateNew();
            seed.Position.X = EnemyBaseRectangle.X;
            seed.Position.Y = EnemyBaseRectangle.Y;
        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {
            
        }

        public void InitializePathfinding(Player player, TileNodeNetwork nodeNetwork, FlatRedBall.TileCollisions.TileShapeCollection collision)
        {
            topDownAiInput.FollowingTarget = player;
            topDownAiInput.NodeNetwork = nodeNetwork;

            topDownAiInput.SetLineOfSightPathfinding(EnemyBaseRectangle.Width * 2,
                // This can take multiple shape collections since some games use more than one
                // TileShapeCollection for pathfinding
                new List<FlatRedBall.TileCollisions.TileShapeCollection> { collision });
        }

        /// <summary>
        /// Returns true or false if the enemy should take damage from the player or not.
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public bool ShouldTakeDamage(Player player)
        {
            fTimeSinceLastDamage += TimeManager.LastSecondDifference;

            if (fTimeSinceLastDamage > 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Calculates how much damage the enemy received from the player attack collision.
        /// </summary>
        /// <param name="enemy"></param>
        public void TakeDamage(Player player)
        {
            iEnemyCurrentHealth -= player.iAttackDamage;
            Debug.WriteLine("Enemy Health: " + iEnemyCurrentHealth);

            if (iEnemyCurrentHealth < 0)
            {
                this.Destroy();
            }
        }
    }
}
