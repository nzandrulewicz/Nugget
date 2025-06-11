using Nugget.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nugget.DataTypes
{
    internal class PlayerData
    {
        /// <summary>
        /// Get instance of the Player entity.
        /// </summary>
        // Entities.Player player;

        /*
        private int myProperty;
        
        public int MyProperty
        {
            get 
            { 
                return myProperty; 
            }
            set
            {
                myProperty = value;
            }
        }
        */

        /// <summary>
        /// Preserve the player's Starting Health value.
        /// </summary>
        public int iStartingHealth { get; set; } = 100;

        /// <summary>
        /// Preserve the player's Current Health value.
        /// </summary>
        public int iCurrentHealth { get; set; } = 100;

        /// <summary>
        /// Preserve the player's Attack Damage value.
        /// </summary>
        public int iAttackDamage { get; set; } = 10;

        /// <summary>
        /// Preserve the number of seed the player has.
        /// </summary>
        public int iSeedCount
        {
            get;
            set;
        }
    }
}
