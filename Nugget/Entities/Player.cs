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
using Nugget.DataTypes;

namespace Nugget.Entities
{
    public partial class Player
    {        
        public int iAttackDamage = GlobalData.PlayerData.iAttackDamage;

        // Give this some large negative value so logic
        // doesn't consider attacks to happen right when 
        // the entity is created
        TimeSpan tsLastTimeAttackStarted = TimeSpan.FromSeconds(-999);

        TimeSpan tsAttackDamageDuration = TimeSpan.FromSeconds(.5);

        TimeSpan tsAttackCooldown = TimeSpan.FromSeconds(1);

        public bool bIsAttackActive =>
            TimeManager.CurrentScreenSecondsSince(tsLastTimeAttackStarted.TotalSeconds) < tsAttackDamageDuration.TotalSeconds;

        public bool bIsAttackAvailable =>
                TimeManager.CurrentScreenSecondsSince(tsLastTimeAttackStarted.TotalSeconds) > tsAttackCooldown.TotalSeconds;

        TimeSpan tsWaitAfterAttackToMove = TimeSpan.FromSeconds(0.25);

        TimeSpan tsWaitAfterAttackToIdle = TimeSpan.FromSeconds(0.5);

        /// <summary>
        /// Initialization logic which is executed only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
        private void CustomInitialize()
        {
            /// <summary>
            /// Prevents the PlayerHealthBar from rotating with the player.
            /// </summary>
            var vHudParent = gumAttachmentWrappers[0];
            vHudParent.ParentRotationChangesRotation = false;

            SwordCollision.Visible = false;
        }

        private void CustomActivity()
        {
            Attack();
        }

        private void Attack()
        {
            /// <summary>
            /// Initialize cursor to get cursor's current position.
            /// </summary>
            var cursor = GuiManager.Cursor;

            /// <summary>
            /// Get mouse cursor position.
            /// </summary>
            var cursorX = cursor.WorldX;
            var cursorY = cursor.WorldY;

            /// <summary>
            /// Get player's current position.
            /// </summary>
            var playerX = PlayerRectangle.X;
            var playerY = PlayerRectangle.Y;

            /// <summary>
            /// Cursor's relative position to the player position.
            /// </summary>
            var deltaX = cursorX - playerX;
            var deltaY = cursorY - playerY;

            /// <summary>
            /// Get the angle from the player's position to the cursor's position in radians.
            /// </summary>
            var angleToCursor = Math.Atan2(deltaY, deltaX);

            /// <summary>
            /// There are four, 90 degree slices.
            /// Top, Down, Left, and Right.
            /// </summary>
            var numberOfNinetyDegreeSlices = Math.Round(angleToCursor / MathConstants.QuarterCircle) * MathConstants.QuarterCircle;

            // If Left Mouse click and Attack Available
            if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.LeftButton) && bIsAttackAvailable)
            {
                // Get the time stamp in seconds since the beginning of the last attack.
                tsLastTimeAttackStarted = TimeSpan.FromSeconds(TimeManager.CurrentScreenTime);
                // Show SwordCollision
                // SwordCollision.Visible = true;
                // Stop the player from moving
                this.CurrentMovement = TopDownValues[DataTypes.TopDownValues.AttackMovement];
                this.RotationZ = (float)numberOfNinetyDegreeSlices;

                PlayerSpriteInstance.Texture = PlayerAttack;
            }

            // If the last time the player attacked was 0.25 seconds ago...
            if (tsLastTimeAttackStarted.TotalSeconds < TimeManager.CurrentScreenTime - tsWaitAfterAttackToMove.TotalSeconds)
            {
                // Allow the player to start walking again.
                this.CurrentMovement = TopDownValues[DataTypes.TopDownValues.Default];
            }

            // If the last time the player attacked was 0.50 seconds ago...
            if (tsLastTimeAttackStarted.TotalSeconds < TimeManager.CurrentScreenTime - tsWaitAfterAttackToIdle.TotalSeconds)
            {
                PlayerSpriteInstance.Texture = PlayerIdle;
            }

            // Boolean that shows or hides the sword collision circle.
            // SwordCollision.Visible = bIsAttackActive;
        }

        /// <summary>
        /// Keeps track of how long (in seconds) since the player last received damage.
        /// </summary>
        float fTimeSinceLastDamage = 1;

        /// <summary>
        /// Returns true or false if the player should take damage from the enemy or not.
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public bool ShouldTakeDamage(Enemy enemy)
        {
            fTimeSinceLastDamage += TimeManager.SecondDifference;

            if (fTimeSinceLastDamage >= 1)
            {
                Debug.WriteLine("Player Health: " + GlobalData.PlayerData.iCurrentHealth);
                fTimeSinceLastDamage = 0;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Calculates how much damage the player received from an enemy collision.
        /// </summary>
        /// <param name="enemy"></param>
        public void TakeDamage(Enemy enemy)
        {
            GlobalData.PlayerData.iCurrentHealth -= enemy.iEnemyAttackDamage;
            PlayerHealthBarRuntimeInstance.PercentFull = 100 * GlobalData.PlayerData.iCurrentHealth / (float)GlobalData.PlayerData.iStartingHealth;
            Debug.WriteLine("Player Health: " + GlobalData.PlayerData.iCurrentHealth);

            if (GlobalData.PlayerData.iCurrentHealth <= 0)
            {
                this.Destroy();
            }
        }

        private void CustomDestroy()
        {
            
        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {
            
        }
    }
}
