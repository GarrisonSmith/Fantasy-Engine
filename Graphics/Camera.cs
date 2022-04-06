﻿using System;
using Microsoft.Xna.Framework;

namespace Fantasy.Content.Logic.Graphics
{
    class Camera
    {
        public Scene _scene;
        public Point cameraCenter;
        public Rectangle cameraPosition;
        public Rectangle boundingBox;
        public Vector2 zoom = new Vector2(1f, 1f);
        public Vector2 maxZoom = new Vector2(3f, 3f);
        public Vector2 minZoom = new Vector2(.5f, .5f);
        public bool movementAllowedVertical = true;
        public bool movementAllowedHorizontal = true;

        public Camera(Scene _scene, Point startingCoordinate)
        {
            this._scene = _scene;
            this.cameraCenter = startingCoordinate;
            cameraPosition.Width = _scene._graphics.PreferredBackBufferWidth;
            cameraPosition.Height = _scene._graphics.PreferredBackBufferHeight;
            Reposition();
            SetBoundingBox(true);
        }
        public Camera(Scene _scene, Point startingCoordinate, Vector2 zoom) : this(_scene, startingCoordinate)
        {
            Zoom(zoom, true);
        }
        public void Pan(Point destination, int speed)
        {
            destination.X = (int)(destination.X * zoom.X);
            destination.Y = (int)(destination.Y * zoom.Y);

            if (PointInBoundingBox(destination))
            {
                while (cameraCenter.X != destination.X || cameraCenter.Y != destination.Y)
                {
                    if (Math.Abs(destination.X - cameraCenter.X) <= speed)
                    {
                        SetHorizontal(destination.X);

                    }
                    else if (cameraCenter.X < destination.X)
                    {
                        MoveHorizontal(false, speed);
                    }
                    else if (cameraCenter.X > destination.X)
                    {
                        MoveHorizontal(true, speed);
                    }

                    if (Math.Abs(destination.Y - cameraCenter.Y) <= speed)
                    {
                        SetVertical(destination.Y);
                    }
                    else if (cameraCenter.Y < destination.Y)
                    {
                        MoveVertical(true, speed);
                    }
                    else if (cameraCenter.Y > destination.Y)
                    {
                        MoveVertical(false, speed);
                    }
                    _scene.ClearAndRedraw();
                }
            }
        }
        public void ForcePan(Point destination, int speed)
        {
            destination.X = (int)(destination.X * zoom.X);
            destination.Y = (int)(destination.Y * zoom.Y);

            if (PointInBoundingBox(destination))
            {
                while (cameraCenter.X != destination.X || cameraCenter.Y != destination.Y)
                {
                    if (Math.Abs(destination.X - cameraCenter.X) <= speed)
                    {
                        ForceSetHorizontal(destination.X);

                    }
                    else if (cameraCenter.X < destination.X)
                    {
                        ForceMoveHorizontal(false, speed);
                    }
                    else if (cameraCenter.X > destination.X)
                    {
                        ForceMoveHorizontal(true, speed);
                    }

                    if (Math.Abs(destination.Y - cameraCenter.Y) <= speed)
                    {
                        ForceSetVertical(destination.Y);
                    }
                    else if (cameraCenter.Y < destination.Y)
                    {
                        ForceMoveVertical(true, speed);
                    }
                    else if (cameraCenter.Y > destination.Y)
                    {
                        ForceMoveVertical(false, speed);
                    }
                    _scene.ClearAndRedraw();
                }
            }
        }
        public void Zoom(Vector2 amount, bool allowCentering)
        {
            if (amount.X >= minZoom.X && amount.X <= maxZoom.X)
            {
                cameraCenter.X = (int)Math.Round(((cameraCenter.X * amount.X) / zoom.X), 0);
                cameraPosition.Width = (int)Math.Round(((cameraPosition.Width * amount.X) / zoom.X), 0);
                zoom.X = amount.X;
            }
            if (amount.Y >= minZoom.Y && amount.Y <= maxZoom.Y)
            {
                cameraCenter.Y = (int)Math.Round(((cameraCenter.Y * amount.Y) / zoom.Y), 0);
                cameraPosition.Height = (int)Math.Round(((cameraPosition.Height * amount.Y) / zoom.Y), 0);
                zoom.Y = amount.Y;
            }

            SetBoundingBox(allowCentering);
        }
        public void PanWithZoom(Point destination, int speed)
        {
            if (movementAllowedVertical && movementAllowedHorizontal)
            {
                destination.X = (int)(destination.X * zoom.X);
                destination.Y = (int)(destination.Y * zoom.Y);
                if (PointInBoundingBox(destination))
                {
                    Vector2 original = zoom;
                    while (!(destination.X <= cameraPosition.X && destination.X >= cameraPosition.X - cameraPosition.Width) ||
                        !(destination.Y <= cameraPosition.Y && destination.Y >= cameraPosition.Y - cameraPosition.Height))
                    {
                        if ((zoom.X - .01f <= minZoom.X + .0f || zoom.X <= original.X - 1f) || (zoom.Y - .01f <= minZoom.Y + .0f || zoom.Y <= original.Y - 1f))
                        {
                            break;
                        }
                        Zoom(new Vector2(zoom.X - .01f, zoom.Y - .01f), false);
                        Reposition();
                        _scene.ClearAndRedraw();
                    }
                    ForcePan(destination, speed);
                    while (original != zoom)
                    {
                        Zoom(new Vector2(zoom.X + .01f, zoom.Y + .01f), false);
                        ForcePan(destination, speed);
                    }
                    Zoom(original, true);
                    ForcePan(destination, speed);
                    Reposition();
                }
            }

        }
        public void ForcePanWithZoom(Point destination, int speed)
        {
            destination.X = (int)(destination.X * zoom.X);
            destination.Y = (int)(destination.Y * zoom.Y);
            if (PointInBoundingBox(destination))
            {
                Vector2 original = zoom;
                while (!(destination.X <= cameraPosition.X && destination.X >= cameraPosition.X - cameraPosition.Width) ||
                    !(destination.Y <= cameraPosition.Y && destination.Y >= cameraPosition.Y - cameraPosition.Height))
                {
                    if ((zoom.X - .01f <= minZoom.X + .0f || zoom.X <= original.X - 1f) || (zoom.Y - .01f <= minZoom.Y + .0f || zoom.Y <= original.Y - 1f))
                    {
                        break;
                    }
                    Zoom(new Vector2(zoom.X - .01f, zoom.Y - .01f), false);
                    Reposition();
                    _scene.ClearAndRedraw();
                }
                ForcePan(destination, speed);
                while (original != zoom)
                {
                    Zoom(new Vector2(zoom.X + .01f, zoom.Y + .01f), false);
                    ForcePan(destination, speed);
                }
                Zoom(original, false);
                ForcePan(destination, speed);
                Reposition();
            }

        }
        public void Reposition()
        {
            //sets camera top right position based off the camera center
            cameraPosition.X = cameraCenter.X + (int)(cameraPosition.Width / (2 * zoom.X));
            cameraPosition.Y = cameraCenter.Y + (int)(cameraPosition.Height / (2 * zoom.Y));
        }
        public void MoveVertical(bool direction, int amount)
        {
            if (movementAllowedVertical)
            {
                if (direction)
                {
                    //moves up
                    if (PointInBoundingBox(new Point(cameraCenter.X, cameraCenter.Y + amount)))
                    {
                        cameraCenter.Y += amount;
                    }
                    else
                    {
                        cameraCenter.Y = boundingBox.Y;
                    }
                }
                else
                {
                    //moves down
                    if (PointInBoundingBox(new Point(cameraCenter.X, cameraCenter.Y - amount)))
                    {
                        cameraCenter.Y -= amount;
                    }
                    else
                    {
                        cameraCenter.Y = boundingBox.Y - boundingBox.Height;
                    }
                }
                Reposition();
            }
        }
        public void SetVertical(int Y)
        {
            if (movementAllowedVertical)
            {
                if (PointInBoundingBox(new Point(cameraCenter.X, Y)))
                {
                    cameraCenter.Y = Y;
                }
                else if (Y <= boundingBox.Y - boundingBox.Height)
                {
                    cameraCenter.Y = boundingBox.Y - boundingBox.Height;
                }
                else
                {
                    cameraCenter.Y = boundingBox.Y;
                }
                Reposition();
            }
        }
        public void MoveHorizontal(bool direction, int amount)
        {
            if (movementAllowedHorizontal)
            {
                if (direction)
                {
                    //moves right
                    if (PointInBoundingBox(new Point(cameraCenter.X - amount, cameraCenter.Y)))
                    {
                        cameraCenter.X -= amount;
                    }
                    else
                    {
                        cameraCenter.X = boundingBox.X - boundingBox.Width;
                    }
                }
                else
                {
                    //moves left
                    if (PointInBoundingBox(new Point(cameraCenter.X + amount, cameraCenter.Y)))
                    {
                        cameraCenter.X += amount;
                    }
                    else
                    {
                        cameraCenter.X = boundingBox.X;
                    }
                }
                Reposition();
            }
        }
        public void SetHorizontal(int X)
        {
            if (movementAllowedHorizontal)
            {
                if (PointInBoundingBox(new Point(X, cameraCenter.Y)))
                {
                    cameraCenter.X = X;
                }
                else if (X <= boundingBox.X - boundingBox.Width)
                {
                    cameraCenter.X = boundingBox.X - boundingBox.Width;
                }
                else
                {
                    cameraCenter.X = boundingBox.X;
                }
                Reposition();
            }
        }
        public void ForceMoveVertical(bool direction, int amount)
        {
            if (direction)
            {
                //moves up
                if (PointInBoundingBox(new Point(cameraCenter.X, cameraCenter.Y + amount)))
                {
                    cameraCenter.Y += amount;
                }
                else
                {
                    cameraCenter.Y = boundingBox.Y;
                }
            }
            else
            {
                //moves down
                if (PointInBoundingBox(new Point(cameraCenter.X, cameraCenter.Y - amount)))
                {
                    cameraCenter.Y -= amount;
                }
                else
                {
                    cameraCenter.Y = boundingBox.Y - boundingBox.Height;
                }
            }
            Reposition();
        }
        public void ForceSetVertical(int Y)
        {
            if (PointInBoundingBox(new Point(cameraCenter.X, Y)))
            {
                cameraCenter.Y = Y;
            }
            else if (Y <= boundingBox.Y - boundingBox.Height)
            {
                cameraCenter.Y = boundingBox.Y - boundingBox.Height;
            }
            else
            {
                cameraCenter.Y = boundingBox.Y;
            }
            Reposition();
        }
        public void ForceMoveHorizontal(bool direction, int amount)
        {
            if (direction)
            {
                //moves right
                if (PointInBoundingBox(new Point(cameraCenter.X - amount, cameraCenter.Y)))
                {
                    cameraCenter.X -= amount;
                }
                else
                {
                    cameraCenter.X = boundingBox.X - boundingBox.Width;
                }
            }
            else
            {
                //moves left
                if (PointInBoundingBox(new Point(cameraCenter.X + amount, cameraCenter.Y)))
                {
                    cameraCenter.X += amount;
                }
                else
                {
                    cameraCenter.X = boundingBox.X;
                }
            }
            Reposition();
        }
        public void ForceSetHorizontal(int X)
        {
            if (PointInBoundingBox(new Point(X, cameraCenter.Y)))
            {
                cameraCenter.X = X;
            }
            else if (X <= boundingBox.X - boundingBox.Width)
            {
                cameraCenter.X = boundingBox.X - boundingBox.Width;
            }
            else
            {
                cameraCenter.X = boundingBox.X;
            }
            Reposition();
        }
        public void SetBoundingBox(bool allowCentering)
        {
            Point _point = _scene._tileMap.GetTileMapCenter(ref zoom);
            Rectangle _rectangle = _scene._tileMap.GetTileMapBounding(ref zoom);
            if (_rectangle.Width <= (cameraPosition.Width / zoom.X) && allowCentering)
            {
                movementAllowedHorizontal = false;
                cameraCenter.X = _point.X;
            }
            else
            {
                movementAllowedHorizontal = true;
            }
            boundingBox.X = _rectangle.X;
            boundingBox.Width = _rectangle.Width;

            if (_rectangle.Height <= (cameraPosition.Height / zoom.Y) && allowCentering)
            {
                movementAllowedVertical = false;
                cameraCenter.Y = _point.Y;
            }
            else
            {
                movementAllowedVertical = true;
            }
            boundingBox.Y = _rectangle.Y;
            boundingBox.Height = _rectangle.Height;

            Reposition();
        }
        public void SetBoundingBox(Point startingCoordinate)
        {
            Point _point = _scene._tileMap.GetTileMapCenter(ref zoom);
            Rectangle _rectangle = _scene._tileMap.GetTileMapBounding(ref zoom);
            if (_rectangle.Width <= (cameraPosition.Width / zoom.X))
            {
                movementAllowedHorizontal = false;
                cameraCenter.X = _point.X;
            }
            else
            {
                movementAllowedHorizontal = true;
                cameraCenter.X = startingCoordinate.X;
            }
            boundingBox.X = _rectangle.X;
            boundingBox.Width = _rectangle.Width;

            if (_rectangle.Height <= (cameraPosition.Height / zoom.Y))
            {
                movementAllowedVertical = false;
                cameraCenter.Y = _point.Y;
            }
            else
            {
                movementAllowedVertical = true;
                cameraCenter.Y = startingCoordinate.Y;
            }
            boundingBox.Y = _rectangle.Y;
            boundingBox.Height = _rectangle.Height;

            Reposition();
        }
        public bool PointInBoundingBox(Point point)
        {
            if ((boundingBox.X >= point.X && boundingBox.X - boundingBox.Width <= point.X) && (boundingBox.Y >= point.Y && boundingBox.Y - boundingBox.Height <= point.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}