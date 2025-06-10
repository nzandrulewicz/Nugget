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
        /// <summary>
        /// Grabs the number of seeds the player is currently holding.
        /// </summary>
        int iTotalSeeds;

        private void CustomInitialize()
        {
            // Text for SeedCount text view in the game.
            // SeedCount is in DataTypes > PlayerData.cs
            this.TextInstance.Text = "0";

            // This foreach handles enemies created before the screen's initialize.
            foreach (var enemy in EnemyList)
            {
                PrepareEnemyPathfinding(enemy);
            }
            // This event handler handles enemies created after the screen's initialize.
            Factories.EnemyFactory.EntitySpawned += PrepareEnemyPathfinding;
        }

        private void CustomActivity(bool firstTimeCalled)
        {
            iTotalSeeds = DataTypes.GlobalData.PlayerData.iSeedCount;

            // Check when "1" is pressed on the keyboard.
            var isOnePressed = InputManager.Keyboard.KeyDown(Microsoft.Xna.Framework.Input.Keys.D1);

            // Checks if the mouse cursor is hovering over the Farm Plots.
            var isHovering1 = FlatRedBall.Gui.GuiManager.Cursor.IsOn(FarmPlot1.FarmPlotSquare);
            var isHovering2 = FlatRedBall.Gui.GuiManager.Cursor.IsOn(FarmPlot2.FarmPlotSquare);
            var isHovering3 = FlatRedBall.Gui.GuiManager.Cursor.IsOn(FarmPlot3.FarmPlotSquare);

            // If 1 is pressed and Player has seeds...
            if (isOnePressed && iTotalSeeds > 0)
            {
                // If hovering over FarmPlot1 and FarmPlot1 is not planted...
                if (isHovering1 && FarmPlot1.IsPlanted == false)
                {
                    PlantSeed(FarmPlot1);
                }
                // If hovering over FarmPlot2 and FarmPlot2 is not planted...
                else if (isHovering2 && FarmPlot2.IsPlanted == false)
                {
                    PlantSeed(FarmPlot2);
                }
                // If hovering over FarmPlot2 and FarmPlot2 is not planted...
                else if (isHovering3 && FarmPlot3.IsPlanted == false)
                {
                    PlantSeed(FarmPlot3);
                }
            }
            
        }

        private void CustomDestroy()
        {

        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {

        }

        private void PrepareEnemyPathfinding(Enemy enemy)
        {
            enemy.InitializePathfinding(Player1, WalkingNodeNetwork, SolidCollision);
        }

        /// <summary>
        /// Plants a seed in a FarmPlot when the mouse is hovering over a specific farm plot that hasn't been planted yet.
        /// Called from CustomActivity
        /// </summary>
        private void PlantSeed(FarmPlot pFarmPlot)
        {
            // Create a new plant inside the pFarmPlot
            Plant plant = Factories.PlantFactory.CreateNew();
            plant.Position.X = pFarmPlot.Position.X;
            plant.Position.Y = pFarmPlot.Position.Y;
            pFarmPlot.IsPlanted = true;

            // Subtract 1 seed from the total of seeds the player is currently holding.
            DataTypes.GlobalData.PlayerData.iSeedCount -= 1;
            this.TextInstance.Text = DataTypes.GlobalData.PlayerData.iSeedCount.ToString();
        }
    }
}
