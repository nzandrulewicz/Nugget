using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nugget.DataTypes
{
    internal class GlobalData
    {
        public static PlayerData PlayerData
        {
            get;
            private set;
        } = new PlayerData();
    }
}
