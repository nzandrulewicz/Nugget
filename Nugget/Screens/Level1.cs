using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Gui;
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Localization;
using Microsoft.Xna.Framework;

using Nugget.Entities;


namespace Nugget.Screens
{
    public partial class Level1
    {
        private void CustomInitialize()
        {
            
        }

        private void CustomActivity(bool firstTimeCalled)
        {
            if (InputManager.Mouse.ButtonPushed(Mouse.MouseButtons.LeftButton))
            {
                PlayerList[0].SwordCircle.Visible = true;
            } else
            {
                PlayerList[0].SwordCircle.Visible = false;
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
