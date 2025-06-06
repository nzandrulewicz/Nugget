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
        private int health;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;

                // Multiply the value by 100 so that full health is 100%
                //PlayerHealthBarRuntimeInstance.PercentFull = 100 * Health / (float)MaxHealth;

                if (health <= 0)
                {
                    //Destroy();
                }
            }
        }

        public int SeedCount
        {
            get;
            set;
        }
    }
}
