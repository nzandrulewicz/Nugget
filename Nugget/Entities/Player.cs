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
    public partial class Player
    {
        // Give this some large negative value so logic
        // doesn't consider attacks to happen right when 
        // the entity is created
        double LastTimeAttackStarted = -999;

        // How long the attack can deal damage
        double AttackDamageDuration = .5;

        // How long the player must wait before attacking again after the attack ends
        double AttackCooldown = 1;

        public bool IsAttackActive =>
            TimeManager.CurrentScreenSecondsSince(LastTimeAttackStarted) < AttackDamageDuration;

        public bool IsAttackAvailable =>
                TimeManager.CurrentScreenSecondsSince(LastTimeAttackStarted) > AttackCooldown;

        /// <summary>
        /// Initialization logic which is executed only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
        private void CustomInitialize()
        {
            SwordCollision.Visible = false;
        }

        private void CustomActivity()
        {
            if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.LeftButton) && IsAttackAvailable)
            {
                LastTimeAttackStarted = TimeManager.CurrentScreenTime;

                // Note - you may want to adjust the rectangle depending on direction facing
                // For example, you can handle left vs right like this:
                if (DirectionFacing == TopDownDirection.Right)
                {
                    SwordCollision.RelativeX = 16;
                    SwordCollision.RelativeY = 0;
                }
                else if (DirectionFacing == TopDownDirection.Left)
                {
                    SwordCollision.RelativeX = -16;
                    SwordCollision.RelativeY = 0;
                }
                else if (DirectionFacing == TopDownDirection.Up)
                {
                    SwordCollision.RelativeX = 0;
                    SwordCollision.RelativeY = 16;
                }
                else if (DirectionFacing == TopDownDirection.Down)
                {
                    SwordCollision.RelativeX = 0;
                    SwordCollision.RelativeY = -16;
                }
            }

            SwordCollision.Visible = IsAttackActive;
        }

        private void CustomDestroy()
        {
            
        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {
            
        }
    }
}
