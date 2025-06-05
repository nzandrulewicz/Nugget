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
using System.Diagnostics;

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
            PlantSeed();
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

        private void PlantSeed()
        {
            var isOnePressed = InputManager.Keyboard.KeyDown(Microsoft.Xna.Framework.Input.Keys.D1);
            var isHovering1 = FlatRedBall.Gui.GuiManager.Cursor.IsOn(FarmPlot1.FarmPlotSquare);
            var isHovering2 = FlatRedBall.Gui.GuiManager.Cursor.IsOn(FarmPlot2.FarmPlotSquare);
            var isHovering3 = FlatRedBall.Gui.GuiManager.Cursor.IsOn(FarmPlot3.FarmPlotSquare);

            if (isHovering1 && isOnePressed && FarmPlot1.IsPlanted == false)
            {
                Plant plant = Factories.PlantFactory.CreateNew();
                plant.Position.X = FarmPlot1.Position.X;
                plant.Position.Y = FarmPlot1.Position.Y;
                FarmPlot1.IsPlanted = true;
            }

            if (isHovering2 && isOnePressed && FarmPlot2.IsPlanted == false)
            {
                Plant plant = Factories.PlantFactory.CreateNew();
                plant.Position.X = FarmPlot2.Position.X;
                plant.Position.Y = FarmPlot2.Position.Y;
                FarmPlot2.IsPlanted = true;
            }

            if (isHovering3 && isOnePressed && FarmPlot3.IsPlanted == false)
            {
                Plant plant = Factories.PlantFactory.CreateNew();
                plant.Position.X = FarmPlot3.Position.X;
                plant.Position.Y = FarmPlot3.Position.Y;
                FarmPlot3.IsPlanted = true;
            }
        }
    }
}
