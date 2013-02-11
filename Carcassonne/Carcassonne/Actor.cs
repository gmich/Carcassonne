﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TileEngine;

namespace Carcassonne
{
    public class Actor : Tile
    {
        private bool locked;
        private float transparency;
        public bool mouseOver;
        public bool available;
        private bool inactive;
        private bool dragging;
        #region Constructor
        public Actor(int background,
                            int interactive,
                            int foreground,
                            string code,
                            Vector2 Position,
                            bool locked)
            : base(background, interactive, foreground, code, Position)
        {
            this.locked = locked;
            transparency = 1.0f;
            mouseOver = false;
            this.available = true;
            dragging = false;
        }

        #endregion

        #region Properties

        public bool Available //if true can trigger event
        {
            get { return available; }
            set { available = value; }
        }

        public bool Inactive  //if true remove from list
        {
            get { return inactive; }
            set { inactive = value; }
        }

        public bool Locked //if true can drag
        {
            get { return locked; }
            set { locked = value; }
        }

        public float Transparency
        {
            get { return transparency;  } 
            set { transparency = value; }
        }

        #endregion

        #region MouseEvents

        public bool MouseOver(Vector2 mousePos)
        {
            if (new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1).Intersects
                  (TileGrid.ExactScreenRectangle(position)))
            {
                mouseOver = true;
                 return true;
            }
            else
            {
                mouseOver = false;
                return false;
            }
        }

        #endregion

        #region Helper Methods

        public void TransparencyHandler(Vector2 mousePos)
        {
            if (MouseOver(mousePos))
                transparency = 1.0f;
            else
                transparency = 0.4f;
        }
            
        public void updateTilePosition(Vector2 mousePos)
        {
            locked = false;
            position = TileGrid.MouseCenter(mousePos);
        }


        public void tilePlacemenet(Vector2 mousePos)
        {
            if (!Locked)
                if ((TileGrid.mapCells[TileGrid.GetCellByPixelX((int)mousePos.X),
                    TileGrid.GetCellByPixelY((int)mousePos.Y)].LayerTiles[1]) == 0
                    && TileManager.AvailabilityMap[TileGrid.GetCellByPixelX((int)mousePos.X),
                    TileGrid.GetCellByPixelY((int)mousePos.Y)]==true)
                {
                    position = TileGrid.GetCellLocation(mousePos);
                    TileGrid.mapCells[TileGrid.GetCellByPixelX((int)mousePos.X),
                    TileGrid.GetCellByPixelY((int)mousePos.Y)].Passable=false;
                    available = true;

                }
                else
                    available = false;
              locked = true;
        }

        public bool readyToCommit()
        {
            return ((TileGrid.mapCells[TileGrid.GetCellByPixelX((int)position.X + TileGrid.TileWidth / 2),
                 TileGrid.GetCellByPixelY((int)position.Y+TileGrid.TileHeight / 2)].LayerTiles[1]) == 0);
               

        }

        public void commitTile()
        {
            if (readyToCommit())
            {
                TileGrid.mapCells[TileGrid.GetCellByPixelX((int)position.X),
                                    TileGrid.GetCellByPixelX((int)position.Y)].LayerTiles[1] = LayerTiles[2];
                TileGrid.mapCells[TileGrid.GetCellByPixelX((int)position.X),
                                    TileGrid.GetCellByPixelX((int)position.Y)].rotation = rotation;
                TileGrid.mapCells[TileGrid.GetCellByPixelX((int)position.X),
                                    TileGrid.GetCellByPixelX((int)position.Y)].CodeValue = CodeValue;
                inactive = true;
            }
        }



        #endregion


    }
}