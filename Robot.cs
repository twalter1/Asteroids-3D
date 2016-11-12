using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameSceneTest
{

    class Robot
    {

        public Vector3 shipPosition = new Vector3(0.0f, -7000.0f, 0.0f);
        public Quaternion shipRotation = Quaternion.Identity;
        public BoundingSphere shipBoundingSphere;
        public int Lives;

        Model model;
        float moveSpeed;
        //float angle;

        public void Initialize(ContentManager contentManager)
        {

            //model = contentManager.Load<Model>("robot");
            model = contentManager.Load<Model>("karenspaceship");
            Lives = 3;
            moveSpeed = -5;
            shipBoundingSphere = new BoundingSphere(shipPosition, 10f);
            /*for (int i = 0; i < model.Meshes.Count; i++)
            {

                boundingSphere = model.Meshes[i].BoundingSphere;

            }*/

        }

        //The update method was derived from http://www.riemers.net/eng/Tutorials/XNA/Csharp/Series2/Flight_kinematics.php
        public void Update(GameTime gameTime)
        {

            //angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
            float leftRightRotation = 0;
            float upDownRotation = 0;
            float turningSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            Quaternion additionalRotation;
            shipBoundingSphere = new BoundingSphere(shipPosition, 10f);

            if (gamePadState.IsConnected)
            {

                //This block of if statements just checks to see if ship needs to be rotated and if so by how much.
                if (gamePadState.ThumbSticks.Right.X < 0)
                {

                    leftRightRotation -= turningSpeed;

                }
                if (gamePadState.ThumbSticks.Right.X > 0)
                {

                    leftRightRotation += turningSpeed;

                }
                if (gamePadState.ThumbSticks.Right.Y < 0)
                {

                    upDownRotation -= turningSpeed;

                }
                if (gamePadState.ThumbSticks.Right.Y > 0)
                {

                    upDownRotation += turningSpeed;

                }

                //This block of if statements are there to change the speed at which the ship is traveling.
                if (gamePadState.ThumbSticks.Left.Y < 0)
                {

                    moveSpeed = -1;

                }
                if (gamePadState.ThumbSticks.Left.Y > 0)
                {

                    moveSpeed = -10;

                }
                if (gamePadState.ThumbSticks.Left.Y == 0)
                {

                    moveSpeed = -5;

                }

                //This is what actually applies the rotation values to the ship.
                additionalRotation = Quaternion.CreateFromAxisAngle(new Vector3(0.0f, -1.0f, 0.0f), leftRightRotation) * Quaternion.CreateFromAxisAngle(new Vector3(1.0f, 0.0f, 0.0f), upDownRotation);
                shipRotation *= additionalRotation;
                moveForward(ref shipPosition, shipRotation, moveSpeed);

            }

        }

        public void Draw(Camera camera)
        {

            foreach(var mesh in model.Meshes)
            {

                foreach(BasicEffect effect in mesh.Effects)
                {

                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    //These next few lines are used to place, rotate, and scale the model.
                    //effect.World = getWorldMatrix();

                    //This puts the model at the origin (0, 0, 0) with no rotation and no scale.
                    //effect.World = Matrix.Identity;

                    //this puts the model 1 unit above the origin (0, 0, 0).
                    //effect.World = Matrix.CreateTranslation(0.0f, 0.0f, 5.0f);
                    effect.World = Matrix.CreateFromQuaternion(shipRotation) * Matrix.CreateTranslation(shipPosition);

                    //This rotates the model 180 degrees.
                    //effect.World = Matrix.CreateRotationZ(MathHelper.Pi);

                    //This will scale the model by x.
                    //effect.World = Matrix.CreateScale(0.5f);

                    effect.View = camera.ViewMatrix;

                    effect.Projection = camera.ProjectionMatrix;

                }
                mesh.Draw();

            }

        }

        //moves the ship forward
        private void moveForward(ref Vector3 position, Quaternion rotationQuat, float speed)
        {

            Vector3 addVector = Vector3.Transform(new Vector3(0.0f, -1.0f, 0.0f), rotationQuat);
            position += addVector * speed;

        }

        /*Matrix getWorldMatrix()
        {

            const float circleRadius = 8;
            const float heightOffGround = 3;

            Matrix translationMatrix = Matrix.CreateTranslation(circleRadius, 0, heightOffGround);
            Matrix rotationMatrix = Matrix.CreateRotationZ(-angle);
            Matrix combined = translationMatrix * rotationMatrix;

            return combined;

        }*/

    }

}
