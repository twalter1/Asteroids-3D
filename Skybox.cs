using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

//I downloaded the images to the skybox at: https://www.assetstore.unity3d.com/en/#!/content/25117

namespace GameSceneTest
{

    class Skybox
    {

        GraphicsDevice graphicsDevice;
        Camera boxCam;
        VertexPositionTexture[] bottomVerts;
        VertexPositionTexture[] topVerts;
        VertexPositionTexture[] frontVerts;
        VertexPositionTexture[] backVerts;
        VertexPositionTexture[] leftVerts;
        VertexPositionTexture[] rightVerts;
        Texture2D bottomTexture;
        Texture2D topTexture;
        Texture2D frontTexture;
        Texture2D backTexture;
        Texture2D leftTexture;
        Texture2D rightTexture;
        BasicEffect effect;

        public Skybox(GraphicsDevice graphics, Camera boxCam)
        {

            this.graphicsDevice = graphics;
            this.boxCam = boxCam;

        }

        public void Initialize()
        {
            bottomVerts = new VertexPositionTexture[6];
            topVerts = new VertexPositionTexture[6];
            frontVerts = new VertexPositionTexture[6]; 
            backVerts = new VertexPositionTexture[6];
            leftVerts = new VertexPositionTexture[6];
            rightVerts = new VertexPositionTexture[6];

            //Set the position of the bottom plane
            bottomVerts[0].Position = new Vector3(-2000, -2000, -2000);
            bottomVerts[1].Position = new Vector3(-2000, 2000, -2000);
            bottomVerts[2].Position = new Vector3(2000, -2000, -2000);
            bottomVerts[3].Position = bottomVerts[1].Position;
            bottomVerts[4].Position = new Vector3(2000, 2000, -2000);
            bottomVerts[5].Position = bottomVerts[2].Position;

            //Set the position of the top plane
            /*topVerts[0].Position = new Vector3(-2000, -2000, 2000);
            topVerts[1].Position = new Vector3(2000, -2000, 2000);
            topVerts[2].Position = new Vector3(-2000, 2000, 2000);
            topVerts[3].Position = topVerts[1].Position;
            topVerts[4].Position = new Vector3(2000, 2000, 2000);
            topVerts[5].Position = topVerts[2].Position;*/

            topVerts[0].Position = new Vector3(-2000, 2000, 2000);
            topVerts[1].Position = new Vector3(-2000, -2000, 2000);
            topVerts[2].Position = new Vector3(2000, 2000, 2000);
            topVerts[3].Position = topVerts[1].Position;
            topVerts[4].Position = new Vector3(2000, -2000, 2000);
            topVerts[5].Position = topVerts[2].Position;

            //Set the position of the front plane
            /*frontVerts[0].Position = new Vector3(-2000, -2000,-2000);
            frontVerts[1].Position = new Vector3(2000, -2000, -2000);
            frontVerts[2].Position = new Vector3(-2000, -2000, 2000);
            frontVerts[3].Position = frontVerts[1].Position;
            frontVerts[4].Position = new Vector3(2000, -2000, 2000);
            frontVerts[5].Position = frontVerts[2].Position;*/

            frontVerts[0].Position = new Vector3(-2000, -2000, 2000);
            frontVerts[1].Position = new Vector3(-2000, -2000, -2000);
            frontVerts[2].Position = new Vector3(2000, -2000, 2000);
            frontVerts[3].Position = frontVerts[1].Position;
            frontVerts[4].Position = new Vector3(2000, -2000, -2000);
            frontVerts[5].Position = frontVerts[2].Position;

            //Set the position of the back plane
            /*backVerts[0].Position = new Vector3(-2000, 2000, -2000);
            backVerts[1].Position = new Vector3(-2000, 2000, 2000);
            backVerts[2].Position = new Vector3(2000, 2000, -2000);
            backVerts[3].Position = backVerts[1].Position;
            backVerts[4].Position = new Vector3(2000, 2000, 2000);
            backVerts[5].Position = backVerts[2].Position;*/

            backVerts[0].Position = new Vector3(-2000, 2000, 2000);
            backVerts[1].Position = new Vector3(-2000, 2000, -2000);
            backVerts[2].Position = new Vector3(2000, 2000, 2000);
            backVerts[3].Position = backVerts[1].Position;
            //backVerts[3].Position = new Vector3(2000, 2000, 2000);
            backVerts[4].Position = new Vector3(2000, 2000, -2000);
            //backVerts[4].Position = backVerts[1].Position;
            backVerts[5].Position = backVerts[2].Position;
            //backVerts[5].Position = new Vector3(2000, 2000, 2000);

            //Set the position of the left plane
            /*leftVerts[0].Position = new Vector3(2000, 2000, 2000);
            leftVerts[1].Position = new Vector3(2000, -2000, 2000);
            leftVerts[2].Position = new Vector3(2000, 2000, -2000);
            leftVerts[3].Position = leftVerts[1].Position;
            leftVerts[4].Position = new Vector3(2000, -2000, -2000);
            leftVerts[5].Position = leftVerts[2].Position;*/

            leftVerts[0].Position = new Vector3(2000, -2000, -2000);
            leftVerts[1].Position = new Vector3(2000, 2000, -2000);
            leftVerts[2].Position = new Vector3(2000, -2000, 2000);
            leftVerts[3].Position = leftVerts[1].Position;
            leftVerts[4].Position = new Vector3(2000, 2000, 2000);
            leftVerts[5].Position = leftVerts[2].Position;

            //Set the position of the right plane
            /*rightVerts[0].Position = new Vector3(-2000, -2000, 2000);
            rightVerts[1].Position = new Vector3(-2000, 2000, 2000);
            rightVerts[2].Position = new Vector3(-2000, -2000, -2000);
            rightVerts[3].Position = rightVerts[1].Position;
            rightVerts[4].Position = new Vector3(-2000, 2000, -2000);
            rightVerts[5].Position = rightVerts[2].Position;*/

            rightVerts[0].Position = new Vector3(-2000, -2000, 2000);
            rightVerts[1].Position = new Vector3(-2000, 2000, 2000);
            rightVerts[2].Position = new Vector3(-2000, -2000, -2000);
            rightVerts[3].Position = rightVerts[1].Position;
            rightVerts[4].Position = new Vector3(-2000, 2000, -2000);
            rightVerts[5].Position = rightVerts[2].Position;

            //keeps track of how many times the image should be repeated.
            int repititions = 1;

            //set the texture coordinates of the bottom plane
            bottomVerts[0].TextureCoordinate = new Vector2(0, 0);
            bottomVerts[1].TextureCoordinate = new Vector2(0, repititions);
            bottomVerts[2].TextureCoordinate = new Vector2(repititions, 0);
            bottomVerts[3].TextureCoordinate = bottomVerts[1].TextureCoordinate;
            bottomVerts[4].TextureCoordinate = new Vector2(repititions, repititions);
            bottomVerts[5].TextureCoordinate = bottomVerts[2].TextureCoordinate;

            //Set the texture coordinates of the top plane
            topVerts[0].TextureCoordinate = new Vector2(0, 0);
            topVerts[1].TextureCoordinate = new Vector2(0, repititions);
            topVerts[2].TextureCoordinate = new Vector2(repititions, 0);
            topVerts[3].TextureCoordinate = topVerts[1].TextureCoordinate;
            topVerts[4].TextureCoordinate = new Vector2(repititions, repititions);
            topVerts[5].TextureCoordinate = topVerts[2].TextureCoordinate;

            //Set the texture coordinates of the front plane
            frontVerts[0].TextureCoordinate = new Vector2(0, 0);
            frontVerts[1].TextureCoordinate = new Vector2(0, repititions);
            frontVerts[2].TextureCoordinate = new Vector2(repititions, 0);
            frontVerts[3].TextureCoordinate = frontVerts[1].TextureCoordinate;
            frontVerts[4].TextureCoordinate = new Vector2(repititions, repititions);
            frontVerts[5].TextureCoordinate = frontVerts[2].TextureCoordinate;

            //Set the texture coordinates of the back plane
            backVerts[0].TextureCoordinate = new Vector2(0, 0);
            backVerts[1].TextureCoordinate = new Vector2(0, repititions);
            backVerts[2].TextureCoordinate = new Vector2(repititions, 0);
            backVerts[3].TextureCoordinate = backVerts[1].TextureCoordinate;
            backVerts[4].TextureCoordinate = new Vector2(repititions, repititions);
            backVerts[5].TextureCoordinate = backVerts[2].TextureCoordinate;

            //Set the texture coordinates of the left plane
            leftVerts[0].TextureCoordinate = new Vector2(0, 0);
            leftVerts[1].TextureCoordinate = new Vector2(0, repititions);
            leftVerts[2].TextureCoordinate = new Vector2(repititions, 0);
            leftVerts[3].TextureCoordinate = leftVerts[1].TextureCoordinate;
            leftVerts[4].TextureCoordinate = new Vector2(repititions, repititions);
            leftVerts[5].TextureCoordinate = leftVerts[2].TextureCoordinate;

            //set the texture coordinates of the right plane
            rightVerts[0].TextureCoordinate = new Vector2(0, 0);
            rightVerts[1].TextureCoordinate = new Vector2(0, repititions);
            rightVerts[2].TextureCoordinate = new Vector2(repititions, 0);
            rightVerts[3].TextureCoordinate = rightVerts[1].TextureCoordinate;
            rightVerts[4].TextureCoordinate = new Vector2(repititions, repititions);
            rightVerts[5].TextureCoordinate = rightVerts[2].TextureCoordinate;

            effect = new BasicEffect(graphicsDevice);

        }

        //These images were downloaded from https://www.assetstore.unity3d.com/en/#!/content/25117
        public void loadContent()
        {

            //Loading the bottom texture image
            //using (var stream = TitleContainer.OpenStream("Content/Skybox/SpaceSkyBox2_bottom4.png"))
            using (var stream = TitleContainer.OpenStream("Content/Skybox/Back_MauveSpaceBox.png"))
            {

                bottomTexture = Texture2D.FromStream(this.graphicsDevice, stream);

            }

            //Loading the top texture image
            //using (var stream = TitleContainer.OpenStream("Content/Skybox/SpaceSkyBox2_top3.png"))
            using (var stream = TitleContainer.OpenStream("Content/Skybox/Front_MauveSpaceBox.png"))
            {

                topTexture = Texture2D.FromStream(this.graphicsDevice, stream);

            }

            //Loading the front texture image
            //using (var stream = TitleContainer.OpenStream("Content/Skybox/SpaceSkyBox2_front5.png"))
            using (var stream = TitleContainer.OpenStream("Content/Skybox/Down_MauveSpaceBox.png"))
            {

                frontTexture = Texture2D.FromStream(this.graphicsDevice, stream);

            }

            //Loading the back texture image
            //using (var stream = TitleContainer.OpenStream("Content/Skybox/SpaceSkyBox2_back6.png"))
            using (var stream = TitleContainer.OpenStream("Content/Skybox/Down_MauveSpaceBox.png"))
            {

                backTexture = Texture2D.FromStream(this.graphicsDevice, stream);

            }

            //Loading the left texture image
            //using (var stream = TitleContainer.OpenStream("Content/Skybox/SpaceSkyBox2_left2.png"))
            using (var stream = TitleContainer.OpenStream("Content/Skybox/Right_MauveSpaceBox.png"))
            {

                leftTexture = Texture2D.FromStream(this.graphicsDevice, stream);

            }

            //Loading the right texture image
            //using (var stream = TitleContainer.OpenStream("Content/Skybox/SpaceSkyBox2_right1.png"))
            using (var stream = TitleContainer.OpenStream("Content/Skybox/Left_MauveSpaceBox.png"))
            {

                rightTexture = Texture2D.FromStream(this.graphicsDevice, stream);

            }

        }

        public void DrawBox()
        {

            effect.View = boxCam.ViewMatrix;
            effect.Projection = boxCam.ProjectionMatrix;

            effect.TextureEnabled = true;
            effect.Texture = bottomTexture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {

                pass.Apply();
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, bottomVerts, 0, 2);

            }

            effect.Texture = topTexture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {

                pass.Apply();
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, topVerts, 0, 2);
                
            }

            effect.Texture = frontTexture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {

                pass.Apply();
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, frontVerts, 0, 2);
                
            }

            effect.Texture = backTexture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {

                pass.Apply();
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, backVerts, 0, 2);
                
            }
            effect.Texture = leftTexture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {

                pass.Apply();
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, leftVerts, 0, 2);

            }

            effect.Texture = rightTexture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {

                pass.Apply();
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, rightVerts, 0, 2);

            }

        }

    }

}
