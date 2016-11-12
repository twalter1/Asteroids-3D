using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameSceneTest
{

    class Asteroid
    {

        //int difficulty;
        //int count;
        /*int numModel1;
        int numModel2;
        int numModel3;*/
        /*int modelIndex;
        int scaleIndex;
        int translationIndex;
        Random rand;
        Random randomPosition;*/
        /*Model model1;
        Model model2;
        Model model3;*/
        Model model;
        public Vector3 position;
        public BoundingSphere boundingSphere;
        /*Model[] modelList;
        Vector3[] initialPosition;
        int[] translationDirection;
        float[] initialScale;*/
        float angle;
        float angleX;
        float angleY;
        float angleZ;
        public float scale;
        /*float positionX;
        float positionY;
        float positionZ;*/
        bool hitXBoundry;
        bool hitYBoundry;
        bool hitZBoundry;
        int modelNumber;
        int sizeOfPlayArea;

        public void Initialize(ContentManager contentManager, int modelNumber, int scaleIndex, int sizeOfPlayArea, float XPosition, float YPosition, float ZPosition)
        {

            //Choosing which model to use.
            //angle = 1;
            this.modelNumber = modelNumber;
            this.sizeOfPlayArea = sizeOfPlayArea;
            if (modelNumber == 0)
            {

                this.model = contentManager.Load<Model>("AsteroidModelCloud");

            }
            else if (modelNumber == 1)
            {

                this.model = contentManager.Load<Model>("AsteroidModelDistortedNoise");

            }
            else if (modelNumber == 2)
            {

                this.model = contentManager.Load<Model>("AsteroidModelNoise");

            }

            //Choosing what the scale for this particular asteroid should be.
            if (scaleIndex == 0)
            {

                this.scale = 10;

            }
            else if (scaleIndex == 1)
            {

                this.scale = 20;

            }
            else if (scaleIndex == 2)
            {

                this.scale = 40;

            }

            //Getting the position information of where this particular asteroid is placed.
            this.position = new Vector3(XPosition, YPosition, ZPosition);
            Debug.WriteLine("This is the starting position of my Asteroids: " + position);
            boundingSphere = new BoundingSphere(position, scale);
            
        }

        public Vector3 getPostion()
        {

            return position;

        }

        public void Update(GameTime gameTime)
        {

            boundingSphere = new BoundingSphere(this.position, this.scale);
            angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!hitXBoundry)
            {

                angleX += (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            else
            {

                angleX -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            if (!hitYBoundry)
            {

                angleY += (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            else
            {

                angleY -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            if (!hitZBoundry)
            {

                angleZ += (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            else
            {

                angleZ -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            }

        }

        public void Draw(Camera camera, Vector3 modelPosition)
        {

            foreach (var mesh in model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {

                    modelNumber = 0;
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    //These next few lines are used to place, rotate, and scale the model.
                    effect.World = getWorldMatrix(scale, this.position.X, this.position.Y, this.position.Z, modelNumber);
                    //effect.World = getWorldMatrix(scale, modelPosition.X, modelPosition.Y, modelPosition.Z, modelNumber);
                    //Debug.WriteLine("This is the world matrix: " + effect.World);
                    //effect.World = Matrix.Identity;
                    //effect.World = Matrix.CreateScale(initialScale[i]) * Matrix.CreateRotationX(-angle) * Matrix.CreateTranslation(initialPosition[i].X, initialPosition[i].Y, initialPosition[i].Z);

                    effect.View = camera.ViewMatrix;

                    effect.Projection = camera.ProjectionMatrix;

                }
                mesh.Draw();

            }

        }

        public void isCollision(Asteroid asteroid1, Asteroid asteroid2)
        {

            if(asteroid1.scale == 40)
            {

                asteroid1.scale = 20;

            }
            else if(asteroid1.scale == 20)
            {

                asteroid1.scale = 10;

            }
            else if(asteroid1.scale == 10)
            {

                asteroid1.scale = 0;

            }
            if(asteroid2.scale == 40)
            {

                asteroid2.scale = 20;

            }
            else if(asteroid2.scale == 20)
            {

                asteroid2.scale = 10;

            }
            else if (asteroid2.scale == 10)
            {

                asteroid2.scale = 0;

            }

        }

        Matrix getWorldMatrix(float currentScale, float currentX, float currentY, float currentZ, int currentModel)
        {

            Matrix rotationMatrix;
            Matrix translationMatrix;
            Matrix scaleMatrix = Matrix.CreateScale(currentScale);

            float xTranslation = currentX * angleX;
            float yTranslation = currentY * angleY;
            float zTranslation = currentZ * angleZ;

            //Debug.WriteLine("This is the xTranslation: " + xTranslation);
            //Debug.WriteLine("This is the yTranslation: " + yTranslation);
            //Debug.WriteLine("This is the zTranslation: " + zTranslation);

            if (xTranslation >= sizeOfPlayArea || xTranslation <= -sizeOfPlayArea)
            {

                if(hitXBoundry)
                {

                    hitXBoundry = false;

                }
                else
                {

                    hitXBoundry = true;

                }

            }
            if (yTranslation >= sizeOfPlayArea || yTranslation <= -sizeOfPlayArea)
            {

                if (hitYBoundry)
                {

                    hitYBoundry = false;

                }
                else
                {

                    hitYBoundry = true;

                }

            }
            if (zTranslation >= sizeOfPlayArea || zTranslation <= -sizeOfPlayArea)
            {

                if (hitZBoundry)
                {

                    hitZBoundry = false;

                }
                else
                {

                    hitZBoundry = true;

                }

            }

            translationMatrix = Matrix.CreateTranslation(xTranslation, yTranslation, zTranslation);

            //Debug.WriteLine("This is the value of angle: " + -angle);
            if(currentModel == 0)
            {

                rotationMatrix = Matrix.CreateRotationX(-angle);

            }
            else if(currentModel == 1)
            {

                rotationMatrix = Matrix.CreateRotationY(-angle);

            }
            else
            {

                rotationMatrix = Matrix.CreateRotationZ(-angle);

            }

            //Debug.WriteLine("This is the value of the translation matrix: " + translationMatrix);
            //Debug.WriteLine("This is the value of the rotation matrix: " + rotationMatrix);
            //Debug.WriteLine("This is the value of the scale matrix: " + scaleMatrix);
            Matrix combined = scaleMatrix * rotationMatrix * translationMatrix;
            //Debug.WriteLine("This is the combined Matrix: " + combined);

            return combined;

        }

    }

}
