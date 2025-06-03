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
        // Initiates the number of "items" (probably seeds) the player starts with.
        public int numberOfItems = 0;

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

            // Cursor's relative position to the player position.
            var deltaX = cursorX - playerX;
            var deltaY = cursorY - playerY;

            // Get the angle from the player's position to the cursor's position in radians.
            var angleToCursor = Math.Atan2(deltaY, deltaX);

            /// <summary>
            /// There are four, 90 degree slices.
            /// Top, Down, Left, and Right.
            /// </summary>
            var numberOfNinetyDegreeSlices = Math.Round(angleToCursor / MathConstants.QuarterCircle) * MathConstants.QuarterCircle;
            
            // If Left Mouse click and Attack Available
            if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.LeftButton) && IsAttackAvailable)
            {
                // Get the time stamp in seconds since the beginning of the last attack.
                LastTimeAttackStarted = TimeManager.CurrentScreenTime;
                // Show SwordCollision
                SwordCollision.Visible = true;
                // Stop the player from moving
                this.CurrentMovement = TopDownValues[DataTypes.TopDownValues.AttackMovement];
                this.RotationZ = (float) numberOfNinetyDegreeSlices;
            }

            // If the last time the player attacked was 0.25 seconds ago...
            if (LastTimeAttackStarted < TimeManager.CurrentScreenTime - 0.25)
            {
                // Allow the player to start walking again.
                this.CurrentMovement = TopDownValues[DataTypes.TopDownValues.Default];
            }
            
            // Boolean that shows or hides the sword collision circle.
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
