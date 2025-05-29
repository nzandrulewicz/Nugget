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
    public partial class GameScreen
    {
        private void CustomInitialize()
        {
            // This foreach handles enemies created before the screen's initialize.
            foreach (var enemy in EnemyBaseList)
            {
                PrepareEnemyPathfinding(enemy);
            }
            // This event handler handles enemies created after the screen's initialize.
            Factories.EnemyBaseFactory.EntitySpawned += PrepareEnemyPathfinding;
        }

        private void CustomActivity(bool firstTimeCalled)
        {
            /*
            FlatRedBallServices.Game.IsMouseVisible = true;
            if (GuiManager.Cursor.PrimaryClick)
            {
                Factories.EnemyBaseFactory.CreateNew(GuiManager.Cursor.WorldPosition);
            }
            */
        }

        private void CustomDestroy()
        {
            
        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {
            
        }

        private void PrepareEnemyPathfinding(EnemyBase enemy)
        {
            enemy.InitializePathfinding(Player1, WalkingNodeNetwork, SolidCollision);
        }
    }
}
