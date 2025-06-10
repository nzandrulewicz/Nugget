using FlatRedBall;
using FlatRedBall.Audio;
using FlatRedBall.Entities;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.Screens;
using Microsoft.Xna.Framework.Graphics;
using Nugget.Entities;
using Nugget.Screens;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
namespace Nugget.Screens
{
    public partial class GameScreen
    {
        void OnPlayerVsEnemyCollided (Entities.Player player, Entities.Enemy enemy) 
        {
            // ShouldTakeDamage checks if
            // * Enemy and Player are on different teams
            // * The player should take damage according to
            //   the last time the player has taken damage 
            //   (for damage over time).
            if (player.ShouldTakeDamage(enemy))
            {
                // Raises all events for damage dealing and ultimately
                // modifies the player's health.
                player.TakeDamage(enemy);
            }
        }

        void OnPlayerSwordCollisionVsEnemyCollided (Entities.Player player, Entities.Enemy enemy) 
        {
            if (player.bIsAttackActive && enemy.ShouldTakeDamage(player))
            {
                enemy.TakeDamage(player);

                if (enemy.iEnemyCurrentHealth <= 0)
                {
                    enemy.Destroy();
                }
            }

            // Debug.WriteLine("Enemy Health: " + enemy.CurrentHealth);
        }
        
        /// <summary>
        /// ALlows player to pick up items that the enemy drops after dying and updates the number of items the user currently has.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="seed"></param>
        void OnPlayerVsSeedCollided (Entities.Player player, Entities.Seed seed) 
        {
            // Remove instance of seed from game.
            seed.Destroy();

            DataTypes.GlobalData.PlayerData.iSeedCount += 1;
            this.TextInstance.Text = DataTypes.GlobalData.PlayerData.iSeedCount.ToString();
        }
    }
}
