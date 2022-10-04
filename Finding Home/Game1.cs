using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Finding_Home
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D cat;
        Vector2 catPosition = new Vector2(0, 440);
        Vector2 velocity;
        Texture2D floorsummer;
        Vector2 floorsummerPosition = new Vector2(0, 500);
        Texture2D wall;
        Vector2 wallPosition = new Vector2(0, 300);
        Texture2D bg;
        Vector2 bgPos = Vector2.Zero;
        Texture2D housesummer;
        Texture2D tree_summer;
        Texture2D bin3;

        //
        Texture2D Dog;
        Vector2 DogPos = new Vector2(200, 430);

        int direction;
        int speed = 3;

        int frame;
        int Totalframe;
        float totalElapsed;
        float timePerFrame;
        int framePerSec;

        KeyboardState keyboardState;


        Vector2 cameraPos = Vector2.Zero;

        Vector2 scroll_factor = new Vector2(1.0f, 1);


        bool hasjump;
        bool CatHit;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            hasjump = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bg = Content.Load<Texture2D>("summer_em");
            floorsummer = Content.Load<Texture2D>("flor2");
            wall = Content.Load<Texture2D>("wall");
            cat = Content.Load<Texture2D>("CatS3");
            Dog = Content.Load<Texture2D>("Dog");
            housesummer = Content.Load<Texture2D>("house_summer");
            tree_summer = Content.Load<Texture2D>("tree_summer");
            bin3 = Content.Load<Texture2D>("bin3");
            framePerSec = 20;
            timePerFrame = (float)1 / framePerSec;
            frame = 0;
            totalElapsed = 0;
            Totalframe = 11;




            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            GraphicsDevice device = _graphics.GraphicsDevice;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            catPosition += velocity;
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left) && catPosition.X >= 0)
            {
                direction = 1;
                //catPosition.X = catPosition.X - speed;
                cameraPos += new Vector2(-1, 0);

                catPosition += new Vector2(-2, 0);
                
            }
            if (keyboardState.IsKeyDown(Keys.Right)&& catPosition.X < _graphics.GraphicsDevice.Viewport.Width - 100)
            {
                direction = 0;
                //catPosition.X = catPosition.X + speed;
                cameraPos += new Vector2(1, 0);
                catPosition += new Vector2(2, 0);
                

            }
            if (keyboardState.IsKeyDown(Keys.Space) && hasjump == false)
            {
                catPosition.Y -= 8f;
                velocity.Y = -8f;
                hasjump = true;
            }
            if (hasjump == true)
            {
                float i = 1;
                velocity.Y += 0.16f * i;
            }

            if (catPosition.Y + cat.Height >= 300 )//แมวยืนบนกำแพง
            {
                hasjump = false;
            }
               
            
            if (catPosition.Y + cat.Height <= 500)
               hasjump = false;
            //if (catPosition.Y + cat.Height <= 400)
           //     hasjump = true;
            if (catPosition.Y + cat.Height <= 364)// กระโดด
                hasjump = true;


            if (catPosition.Y >= 440)
                catPosition.Y = 440;


            if (hasjump == false)
                velocity.Y = 0f;

            if (keyboardState.IsKeyDown(Keys.Down) && hasjump == false)
            {
                catPosition.Y -= -8f;
                velocity.Y = 8f;
                hasjump = true;
            }

            System.Console.WriteLine("player pos (x,y)" + catPosition);
            System.Console.WriteLine("camera player pos (X,Y) " + (catPosition - cameraPos));
            UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);

            Rectangle CatRectangle = new Rectangle((int)catPosition.X,(int) catPosition.Y, 100, 64);
            Rectangle DogRectangle = new Rectangle((int)DogPos.X, (int)DogPos.Y, 72, 72);
            if (CatRectangle.Intersects(DogRectangle)==true)
            {
                CatHit = true;
            }
            else if (CatRectangle.Intersects(DogRectangle)==false)
            {
                CatHit = false;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice device = _graphics.GraphicsDevice;
            if (CatHit == true)
            {
                device.Clear(Color.Red);
            }
            else 
            {
                device.Clear(Color.CornflowerBlue);
            }
           
            _spriteBatch.Begin();

            //_spriteBatch.Draw(bg, bgPos * scroll_factor, Color.White);
             _spriteBatch.Draw(housesummer, new Vector2(70,150), Color.White);
            _spriteBatch.Draw(housesummer, new Vector2(450, 150), Color.White);
            _spriteBatch.Draw(tree_summer, new Vector2(200, 150), Color.White);
            _spriteBatch.Draw(tree_summer, new Vector2(590, 150), Color.White);
            _spriteBatch.Draw(wall, wallPosition , Color.White);
            _spriteBatch.Draw(wall, wallPosition  + new Vector2(_graphics.GraphicsDevice.Viewport.Width, 0), Color.White);
            _spriteBatch.Draw(bin3, new Vector2(390, 440), Color.White);
           

            //_spriteBatch.Draw(bg, bgPos  * scroll_factor + new Vector2(_graphics.GraphicsDevice.Viewport.Width, 0), Color.White);
            
            
            _spriteBatch.Draw(Dog,DogPos,new Rectangle(0 ,0,72,72),Color.White);
            _spriteBatch.Draw(floorsummer, (floorsummerPosition ) * scroll_factor, Color.White);
            _spriteBatch.Draw(cat, catPosition, new Rectangle(frame * 100, 64 * direction, 100, 64), Color.White);
            //_spriteBatch.Draw(floorsummer, (floorsummerPosition - cameraPos) * scroll_factor + new Vector2(_graphics.GraphicsDevice.Viewport.Width, 0), Color.White);
    
            
            

            
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        void UpdateFrame(float elapsed)
        {
            totalElapsed += elapsed;
            if (totalElapsed > timePerFrame)
            {
                frame = (frame + 1) % Totalframe;
                totalElapsed -= timePerFrame;
            }
        }
    }
}