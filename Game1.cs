using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameSceneTest
{

     public class Game1 : Game
     {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector3 cameraPosition = new Vector3(15, 10, 10);
        Robot robot;
        //Asteroid[] asteroids;
        List<Asteroid> asteroids;
        Camera camera;
        Skybox skybox;
        Random rand;
        Random randomPosition;
        SpriteFont LivesRemaining;
        int difficulty;
        int numAsteroids;
        int size;
        int sizeOfUniverse;
        int modelIndex;
        int scaleIndex;
        float totalGameTime;
        int displayTime;
        

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            //graphics.PreferredBackBufferWidth = 1920;
            //graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
            displayTime = 90;

        }

        protected override void Initialize()
        {

            asteroids = new List<Asteroid>();
            rand = new Random();
            randomPosition = new Random();
            difficulty = 3;
            size = 1;

            //Setting up the number of asteroids that will be in the playable universe
            if(difficulty == 1)
            {

                numAsteroids = 3;
                //asteroids = new Asteroid[numAsteroids];

            }
            else if(difficulty == 2)
            {

                numAsteroids = 100;
                //asteroids = new Asteroid[numAsteroids];

            }
            else if(difficulty == 3)
            {

                numAsteroids = 200;
                //asteroids = new Asteroid[numAsteroids];

            }

            //Setting up the size of the playable universe
            if(size == 1)
            {

                sizeOfUniverse = 500;

            }
            else if(size == 2)
            {

                sizeOfUniverse = 1000;

            }
            else if(size == 3)
            {

                sizeOfUniverse = 2000;

            }

            for(int i = 0; i < numAsteroids; i++)
            {

                float positionX;
                float positionY;
                float positionZ;
                //randomizing what model to draw, how big is should be, and where it's initial position is
                modelIndex = rand.Next(3);
                scaleIndex = rand.Next(3);
                positionX = randomPosition.Next(sizeOfUniverse + 1);
                positionY = randomPosition.Next(sizeOfUniverse + 1);
                positionZ = randomPosition.Next(sizeOfUniverse + 1);
                /*Debug.WriteLine("This is the size of the universe: " + sizeOfUniverse);
                Debug.WriteLine("This is the positionX: " + positionX);
                Debug.WriteLine("This is the positiony: " + positionY);
                Debug.WriteLine("This is the positionz: " + positionZ);*/


                if (positionX > (sizeOfUniverse / 2))
                {

                    positionX = (sizeOfUniverse / 2) - positionX;

                }
                if (positionY > (sizeOfUniverse / 2))
                {

                    positionY = (sizeOfUniverse / 2) - positionY;

                }
                if (positionZ > (sizeOfUniverse / 2))
                {

                    positionZ = (sizeOfUniverse / 2) - positionZ;

                }

                //asteroids[i] = new Asteroid(Content, modelIndex, scaleIndex, sizeOfUniverse, positionX, positionY, positionZ);
                Asteroid temp = new Asteroid();
                temp.Initialize(Content, modelIndex, scaleIndex, sizeOfUniverse, positionX, positionY, positionZ);
                asteroids.Add(temp);
                //asteroids.Add(new Asteroid(Content, modelIndex, scaleIndex, sizeOfUniverse, positionX, positionY, positionZ));

            }

            

            robot = new Robot();
            robot.Initialize(Content);

            //asteroid = new Asteroid(difficulty);
            //asteroid.Initialize(Content);

            camera = new Camera(graphics.GraphicsDevice);

            skybox = new Skybox(graphics.GraphicsDevice, camera);
            skybox.Initialize();

            base.Initialize();

        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            LivesRemaining = Content.Load<SpriteFont>("Fonts/RemainingLivesFont");
            skybox.loadContent();

        }

        protected override void UnloadContent()
        {

            // TODO: Unload any non ContentManager content here

        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            totalGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(totalGameTime >= 1)
            {

                displayTime--;
                totalGameTime = 0;
                if(displayTime == 0)
                {

                    Exit();

                }

            }

            robot.Update(gameTime);

            for(int i = 0; i < asteroids.Count; i++)
            {

                if (asteroids[i].scale == 0)
                {

                    asteroids.Remove(asteroids[i]);
                    //Debug.WriteLine("Removed an asteroid the currentnumber of Asteroids is: " + asteroids.Count);
                    break;

                }
                //Debug.WriteLine("Checking for collisions");
                if (robot.shipBoundingSphere.Intersects(asteroids[i].boundingSphere))
                {

                    Debug.WriteLine("I just hit an asteroid");
                    robot.Lives--;
                    if(robot.Lives == 0)
                    {

                        Exit();

                    }
                    robot.shipPosition = new Vector3(0.0f, -5000.0f, 0.0f);
                    robot.shipRotation = Quaternion.Identity;
                    camera.Update(gameTime, robot);

                }
                for(int j = 0; j < asteroids.Count; j++)
                {

                    if(j == i)
                    {

                        j++;

                    }
                    if(j < asteroids.Count)
                    {

                        if (asteroids[i].boundingSphere.Intersects(asteroids[j].boundingSphere))
                        {

                            asteroids[i].isCollision(asteroids[i], asteroids[j]);
                            
                        }

                    }
                    
                }
                asteroids[i].Update(gameTime);

            }

            camera.Update(gameTime, robot);

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            skybox.DrawBox();

            float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
            robot.Draw(camera);

            for(int i = 0; i < asteroids.Count; i++)
            {

                //Debug.WriteLine("This is the position of each asteroid in the Draw Function: " + asteroids[i].position);
                asteroids[i].Draw(camera, asteroids[i].position);

            }

            spriteBatch.Begin();
            spriteBatch.DrawString(LivesRemaining, "Lives:  " + robot.Lives, new Vector2(20, 40), Color.White);
            if(displayTime >= 60)
            {

                if((displayTime - 60) >= 10)
                {

                    spriteBatch.DrawString(LivesRemaining, "Time: 1:" + (displayTime - 60), new Vector2(680, 40), Color.White);

                }
                else
                {

                    spriteBatch.DrawString(LivesRemaining, "Time: 1:0" + (displayTime - 60), new Vector2(680, 40), Color.White);

                }

            }
            else
            {

                if(displayTime >= 10)
                {

                    spriteBatch.DrawString(LivesRemaining, "Time: 0:" + displayTime, new Vector2(680, 40), Color.White);

                }
                else
                {

                    spriteBatch.DrawString(LivesRemaining, "Time: 0:0" + displayTime, new Vector2(680, 40), Color.White);

                }

            }
            spriteBatch.End();



            base.Draw(gameTime);

        }

    }

}
