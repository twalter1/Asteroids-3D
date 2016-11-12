using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


//The basis of the this camera class came from https://developer.xamarin.com/guides/cross-platform/game_development/monogame/3d/part3/ 
//under the heading Moving the Camera with Input.

namespace GameSceneTest
{

    class Camera
    {

        //public Quaternion cameraRotation = Quaternion.Identity;
        public Matrix ViewMatrix;

        GraphicsDevice graphicsDevice;
        //Vector3 position = new Vector3(0, 20, 10);
        //Vector3 position = new Vector3(0.0f, 15.0f, 10.0f);
        Matrix worldMatrix;
        float angleZ;
        float angleX;

        /*public Matrix ViewMatrix
        {

            get
            {

                position = Vector3.Transform(position, Matrix.CreateFromQuaternion(Robot.shipRotation));

                //worldMatrix = Matrix.CreateFromQuaternion(cameraRotation) * Matrix.CreateTranslation(position);
                //return worldMatrix;

                /*Vector3 lookAtVector = new Vector3(0, -1, 0);
                Matrix rotationMatrixZ = Matrix.CreateRotationZ(angleZ);
                Matrix rotationMatrixX = Matrix.CreateRotationX(angleX);
                lookAtVector = Vector3.Transform(lookAtVector, rotationMatrixZ);
                lookAtVector = Vector3.Transform(lookAtVector, rotationMatrixX);
                lookAtVector += position;
                Vector3 upVector = Vector3.UnitZ;

                return Matrix.CreateLookAt(position, lookAtVector, upVector);

                /*Quaternion lookAt = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, -1), angleZ) * Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), angleX);
                Vector3 lookAtVector = new Vector3(lookAt.X, lookAt.Y, lookAt.Z);
                lookAtVector += position;
                Vector3 upVector = Vector3.UnitZ;

                return Matrix.CreateLookAt(position, lookAtVector, upVector);

            }

        }*/

        public Matrix ProjectionMatrix
        {

            get
            {

                //Setting up the field of view, clipping planes, and aspect ratio
                float fieldOfView = MathHelper.PiOver4;
                //Anything closer than this will not be drawn
                float nearClipPlane = 1;
                //Anything farther than this will not be drawn
                float farClipPlane = 50000;
                float aspectRatio = graphicsDevice.Viewport.Width / (float)graphicsDevice.Viewport.Height;

                return Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);

            }

        }

        public Camera(GraphicsDevice graphicsDevice)
        {

            this.graphicsDevice = graphicsDevice;

        }

        //The update method was derived from http://www.riemers.net/eng/Tutorials/XNA/Csharp/Series2/Quaternions.php
        public void Update(GameTime gameTime, Robot robot)
        {

            Vector3 position = new Vector3(0.0f, -30.0f, 7.0f);
            position = Vector3.Transform(position, Matrix.CreateFromQuaternion(robot.shipRotation));
            position += robot.shipPosition;
            Vector3 upVector = new Vector3(0.0f, 0.0f, 1.0f);
            upVector = Vector3.Transform(upVector, Matrix.CreateFromQuaternion(robot.shipRotation));
            ViewMatrix = Matrix.CreateLookAt(position, robot.shipPosition, upVector);

            /*GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            bool directionForward = false;
            bool directionUp = false;
            bool directionRight = false;

            if(gamePadState.IsConnected)
            {

                if(gamePadState.ThumbSticks.Left.X < 0)
                {

                    //angleZ += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    directionRight = false;
                    moveCameraRight(gameTime, directionRight);

                }
                if(gamePadState.ThumbSticks.Left.Y < 0)
                {

                    directionForward = false;
                    moveCameraForward(gameTime, directionForward);
                    
                }
                if(gamePadState.ThumbSticks.Left.Y > 0)
                {

                    directionForward = true;
                    moveCameraForward(gameTime, directionForward);
                   
                }
                if(gamePadState.ThumbSticks.Left.X > 0)
                {

                    //angleZ -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    directionRight = true;
                    moveCameraRight(gameTime, directionRight);

                }
                if(gamePadState.ThumbSticks.Right.Y > 0)
                {

                    if(gamePadState.ThumbSticks.Left.Y == 0)
                    {

                        angleX -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                        //angleZ -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                    }
                    else
                    {

                        directionUp = false;
                        angleX -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                        moveCameraUp(gameTime, directionUp, directionForward);

                    }

                }
                if(gamePadState.ThumbSticks.Right.Y < 0)
                {

                    if (gamePadState.ThumbSticks.Left.Y == 0)
                    {

                        angleX += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        //angleZ += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    }
                    else
                    {

                        directionUp = true;
                        angleX += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        moveCameraUp(gameTime, directionUp, directionForward);

                    }
                    
                }
                if(gamePadState.ThumbSticks.Right.X > 0)
                {

                    /*directionRight = true;
                    moveCameraRight(gameTime, directionRight);
                    angleZ -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                }
                if(gamePadState.ThumbSticks.Right.X < 0)
                {

                    /*directionRight = false;
                    moveCameraRight(gameTime, directionRight);
                    angleZ += (float)gameTime.ElapsedGameTime.TotalSeconds;

                }

            }*/

        }

        /*void moveCameraForward(GameTime gameTime, bool forward)
        {

            Vector3 moveCamera;
            if (forward)
            {

                moveCamera = new Vector3(0, -1, 0);

            }
            else
            {

                moveCamera = new Vector3(0, 1, 0);

            }
            Matrix rotationMatrixZ = Matrix.CreateRotationZ(angleZ);
            Matrix rotationMatrixX = Matrix.CreateRotationX(angleX);
            moveCamera = Vector3.Transform(moveCamera, rotationMatrixZ);
            moveCamera = Vector3.Transform(moveCamera, rotationMatrixX);

            const float unitsPerSecond = 50;

            this.position += moveCamera * unitsPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;

        }

        void moveCameraUp(GameTime gameTime, bool up, bool forward)
        {

            Vector3 moveCamera;
            if (forward && !up)
            {

                moveCamera = new Vector3(0, -1, -1);

            }
            else if (!forward && !up)
            {

                moveCamera = new Vector3(0, 1, 1);

            }
            else if (forward && up)
            {

                moveCamera = new Vector3(0, -1, 1);

            }
            else
            {

                moveCamera = new Vector3(0, 1, -1);

            }
            Matrix rotationMatrixZ = Matrix.CreateRotationZ(angleZ);
            Matrix rotationMatrixX = Matrix.CreateRotationX(angleX);
            moveCamera = Vector3.Transform(moveCamera, rotationMatrixZ);
            moveCamera = Vector3.Transform(moveCamera, rotationMatrixX);

            const float unitsPerSecond = 50;

            this.position += moveCamera * unitsPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;

        }

        void moveCameraRight(GameTime gameTime, bool right)
        {

            Vector3 moveCamera;
            if (right)
            {

                moveCamera = new Vector3(-1, 0, 0);

            }
            else
            {

                moveCamera = new Vector3(1, 0, 0);

            }
            Matrix rotationMatrixZ = Matrix.CreateRotationZ(angleZ);
            Matrix rotationMatrixX = Matrix.CreateRotationX(angleX);
            moveCamera = Vector3.Transform(moveCamera, rotationMatrixZ);
            moveCamera = Vector3.Transform(moveCamera, rotationMatrixX);

            const float unitsPerSecond = 50;

            this.position += moveCamera * unitsPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;

        }*/

    }

}
