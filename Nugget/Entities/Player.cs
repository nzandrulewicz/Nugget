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
using FlatRedBall.Gui;
using System.Diagnostics;
using Nugget.Utilities;

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
            // Initialize cursor to get cursor's current position.
            var cursor = GuiManager.Cursor;

            // Get mouse cursor position.
            var cursorX = cursor.WorldX;
            var cursorY = cursor.WorldY;

            // Get player's current position.
            var playerX = PlayerRectangle.X;
            var playerY = PlayerRectangle.Y;

            var deltaX = cursorX - playerX;
            var deltaY = cursorY - playerY;

            var angleToCursor = Math.Atan2(deltaY, deltaX);

            var numberOfNinetyDegreeSlices = Math.Round(angleToCursor / MathConstants.QuarterCircle) * MathConstants.QuarterCircle;

            if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.LeftButton) && IsAttackAvailable)
            {
                SwordCollision.Visible = true;
                LastTimeAttackStarted = TimeManager.CurrentScreenTime;
                this.RotationZ = (float)numberOfNinetyDegreeSlices;
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
