using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nugget.Utilities
{
    public static class MathConstants
    {
        /// <summary>
        /// Represents the absence of rotation.
        /// </summary>
        public const float NoRotation = 0;

        /// <summary>
        /// Represents a full 360-degree rotation in radians.
        /// </summary>
        public const float FullTurn = 2 * MathF.PI;

        /// <summary>
        /// Represents a 270-degree rotation in radians.
        /// </summary>
        public const float ThreeQuartersTurn = 3 * MathF.PI / 2;

        /// <summary>
        /// Represents a 180-degree rotation in radians.
        /// </summary>
        public const float HalfTurn = MathF.PI;

        /// <summary>
        /// Represents a 90-degree rotation in radians.
        /// </summary>
        public const float QuarterCircle = MathF.PI / 2;

        /// <summary>
        /// Represents a 45-degree rotation in radians.
        /// </summary>
        public const float EighthTurn = MathF.PI / 4;

        /// <summary>
        /// Counter-clockwise rotation is represented by a positive value.
        /// </summary>
        /// <example><see cref="RotateCcw"/> * <see cref="QuarterCircle"/> is the value of a 90 degree counter-clockwise rotation in radians.</example>
        public const int RotateCcw = 1;

        /// <summary>
        /// Clockwise rotation is represented by a negative value.
        /// </summary>
        /// <example><see cref="RotateCw"/> * <see cref="EighthTurn"/> is the value of a 45 degree clockwise rotation in radians.</example>
        public const int RotateCw = -1;
    }
}
