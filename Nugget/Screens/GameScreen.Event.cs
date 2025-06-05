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
        void OnPlayerVsEnemyCollided (Entities.Player player, Entities.EnemyBase enemy) 
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

                // Typically when an entity reaches 0 health, it should be destroyed
                if (player.CurrentHealth <= 0)
                {
                    player.Destroy();
                }
            }
        }

        void OnPlayerSwordCollisionVsEnemyCollided (Entities.Player player, Entities.EnemyBase enemy) 
        {
            if (player.IsAttackActive && enemy.ShouldTakeDamage(player))
            {
                enemy.TakeDamage(player);

                if (enemy.CurrentHealth <= 0)
                {
                    enemy.Destroy();
                }
            }
        }
        
        /// <summary>
        /// ALlows player to pick up items that the enemy drops after dying and updates the number of items the user currently has.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="seed"></param>
        void OnPlayerVsSeedCollided (Entities.Player player, Entities.Seed seed) 
        {
            player.numberOfSeeds += 1;
            var totalItems = player.numberOfSeeds;
            StatHud1.SeedCount1 = totalItems;
            seed.Destroy();
        }
    }
}
