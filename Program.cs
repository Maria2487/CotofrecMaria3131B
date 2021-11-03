using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.Input;
using System;
using System.Drawing;

namespace CotofrecMaria3131B
{
    internal class SimpleWindow3D : GameWindow
    {
        private const int XYZ_SIZE = 75;
        private const int colorMax = 255;
        private const int colorMin = 0;
        private int redColor = 0, greenColor = 0, blueColor = 0;
        private int[] redColorVec = new int[3], greenColorVec = new int[3], blueColorVec = new int[3];
        private const String triangleFileName = @"E:\An3\CotofrecMaria3131B\TriangleCoord.txt";
        private const String cubeFileName = @"E:\An3\CotofrecMaria3131B\CubeCoord.txt";
        private const float rotation_speed = 180.0f;
        private float angle;
        private bool showCube = true;
        private KeyboardState lastKeyPress;
        private bool moveObjectRight, moveObjectLeft, moveMouseRight, moveMouseLeft;
        private int[] SeePort = new int[3];//// Valori pentru Matrix.LookAt();

        ///

        private int Increment = 5;

        private Color[] colorsCube;

        ///private readonly Color DEFAULT_BKG_COLOR = Color.FromArgb(49, 50, 51);


        ///
        
        public SimpleWindow3D() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;
            KeyDown += Keyboard_KeyDown;
            SeePort[0] = SeePort[1] = SeePort[2] = 10;
        }

        private void MyHelp()
        {
            Console.WriteLine("\n      MENIU");
            Console.WriteLine(" (H) - revenire la meniu");
            Console.WriteLine(" (ESC) - iesire din aplicatie");
            Console.WriteLine(" (F11) - redimensionare fereastra");
            Console.WriteLine(" (Z) - schimbare vizibilitate sistem de axe");
            Console.WriteLine(" (X) - schimbare vizibilitate Triunghi");
            Console.WriteLine(" (C) - schimbare vizibilitate Cub");
            Console.WriteLine(" (B) - schimbare culoare de fundal");
            Console.WriteLine(" (J,K,L) - schimba culoare triunghi (mareste)");
            Console.WriteLine(" (U,I,O) - schimba culoare triunghi (scade)");
            Console.WriteLine(" (1,2,3,4,5,6) - schimba culoare fata cub");
            Console.WriteLine(" (W,A,S,D) - deplasare camera (izometric)");
        }

        private void DisplayColors()
        {
            Console.WriteLine("Setul A :" + redColorVec[0].ToString() + " - " + blueColorVec[0].ToString() + " - " + greenColorVec[0].ToString());
            Console.WriteLine("Setul B :" + redColorVec[1].ToString() + " - " + blueColorVec[1].ToString() + " - " + greenColorVec[1].ToString());
            Console.WriteLine("Setul C :" + redColorVec[2].ToString() + " - " + blueColorVec[2].ToString() + " - " + greenColorVec[2].ToString() + "\n");
        }

        public bool CheckIfInRangeColor(int color)
        {
            if (color >= colorMin && color < colorMax)
                return true;
            return false;
        }

        private void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                if (this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.PaleTurquoise);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            base.OnUpdateFrame(e);

            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetState();

            if (keyboard[OpenTK.Input.Key.H] && !keyboard.Equals(lastKeyPress))
            {
                MyHelp();// Afisare meniu
            }

            #region Modificare culoare RGB Laborator 3

            if (keyboard[OpenTK.Input.Key.G])
            {
                if (keyboard[OpenTK.Input.Key.Up])
                {
                    if (CheckIfInRangeColor(greenColor))
                    {
                        greenColor++;
                        Console.WriteLine("R: " + redColor + " G: " + greenColor + " B: " + blueColor);
                    }
                }
                else if (keyboard[OpenTK.Input.Key.Down])
                {
                    if (CheckIfInRangeColor(greenColor - 1))
                    {
                        greenColor--;
                        Console.WriteLine("R: " + redColor + " G: " + greenColor + " B: " + blueColor);
                    }
                }
            }

            if (keyboard[OpenTK.Input.Key.R])
            {
                if (keyboard[OpenTK.Input.Key.Up])
                {
                    if (CheckIfInRangeColor(redColor))
                    {
                        redColor++;
                        Console.WriteLine("R: " + redColor + " G: " + greenColor + " B: " + blueColor);
                    }
                }
                else if (keyboard[OpenTK.Input.Key.Down])
                {
                    if (CheckIfInRangeColor(redColor - 1))
                    {
                        redColor--;
                        Console.WriteLine("R: " + redColor + " G: " + greenColor + " B: " + blueColor);
                    }
                }
            }

            if (keyboard[OpenTK.Input.Key.B])
            {
                if (keyboard[OpenTK.Input.Key.Up])
                {
                    if (CheckIfInRangeColor(blueColor))
                    {
                        blueColor++;
                        Console.WriteLine("R: " + redColor + " G: " + greenColor + " B: " + blueColor);
                    }
                }
                else if (keyboard[OpenTK.Input.Key.Down])
                {
                    if (CheckIfInRangeColor(blueColor - 1))
                    {
                        blueColor--;
                        Console.WriteLine("R: " + redColor + " G: " + greenColor + " B: " + blueColor);
                    }
                }
            }

            #endregion Modificare culoar RGB Laborator 3

            // Se utilizeaza mecanismul de control input oferit de OpenTK (include perifcerice multiple, mai ales pentru gaming - gamepads, joysticks, etc.).
            if (keyboard[OpenTK.Input.Key.Escape])
            {
                Exit();
                return;
            }
            else if (keyboard[OpenTK.Input.Key.P] && !keyboard.Equals(lastKeyPress))
            {
                // Ascundere comandată, prin apăsarea unei taste - cu verificare de remanență! Timpul de reacțieuman << calculator.
                if (showCube == true)
                {
                    showCube = false;
                }
                else
                {
                    showCube = true;
                }
            }

            #region Rotire obiect (taste) Laborator 2

            moveObjectRight = false;
            moveObjectLeft = false;

            if (keyboard[OpenTK.Input.Key.Left])
            {
                moveObjectLeft = true;
            }
            if (keyboard[OpenTK.Input.Key.Right])
            {
                moveObjectRight = true;
            }
            #endregion Rotire obiect (taste) Laborator 2

            #region Schimbare unghi camera (mouse) Laborator 3

            if (mouse.X < -50)
            {
                moveMouseLeft = true;
                if (SeePort[0] > -10)
                    SeePort[0]--;
            }
            else if (mouse.X > 100)
            {
                moveMouseRight = true;
                if (SeePort[0] < 20)
                    SeePort[0]++;
            }
            if (mouse.Y < -50)
            {
                moveMouseLeft = true;
                if (SeePort[1] > -10)
                    SeePort[1]--;
            }
            else if (mouse.Y > 100)
            {
                moveMouseRight = true;
                if (SeePort[1] < 20)
                    SeePort[1]++;
            }

            #endregion Schimbare unghi camera (mouse) Laborator 3


            #region Schimbarea culorii fetelor cubului Laborator 4
            if (keyboard[OpenTK.Input.Key.ShiftLeft] || keyboard[OpenTK.Input.Key.ShiftRight])
            {
                Randomizer faceColor = new Randomizer();
                if (keyboard[OpenTK.Input.Key.Number1] && !keyboard.Equals(lastKeyPress))
                {
                    colorsCube[0] = faceColor.RandomColor();
                }
                if (keyboard[OpenTK.Input.Key.Number2] && !keyboard.Equals(lastKeyPress))
                {
                    colorsCube[1] = faceColor.RandomColor();
                }
                if (keyboard[OpenTK.Input.Key.Number3] && !keyboard.Equals(lastKeyPress))
                {
                    colorsCube[2] = faceColor.RandomColor();
                }
                if (keyboard[OpenTK.Input.Key.Number4] && !keyboard.Equals(lastKeyPress))
                {
                    colorsCube[3] = faceColor.RandomColor();
                }
                if (keyboard[OpenTK.Input.Key.Number5] && !keyboard.Equals(lastKeyPress))
                {
                    colorsCube[4] = faceColor.RandomColor();
                }
                if (keyboard[OpenTK.Input.Key.Number6] && !keyboard.Equals(lastKeyPress))
                {
                    colorsCube[5] = faceColor.RandomColor();
                }
            }
            #endregion

            lastKeyPress = keyboard;

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Matrix4 lookat = Matrix4.LookAt(6, 6, 10, 0, 0, 0, 0, 1, 0);
            Matrix4 lookat = Matrix4.LookAt(SeePort[0], SeePort[1], SeePort[2], 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            MyAxis.DrawAxis();

            MyTriangle trg1 = MyTriangle.ReadCoordonatesTriangle(triangleFileName); //////////// Triunghi
            trg1.DrawMe();


            MyCube cube = new MyCube(cubeFileName); ///////////// Cub
            cube.DrawMe(colorsCube);

            #region Rotire obiect Laborator 2

            if (moveObjectRight)
            {
                angle += rotation_speed * (float)e.Time;
                GL.Rotate(angle, 0.0f, 1.0f, 0.0f);
            }
            if (moveObjectLeft)
            {
                angle += rotation_speed * (float)e.Time;
                GL.Rotate(angle, 0.0f, -1.0f, 0.0f);
            }

            #endregion Rotire obiect Laborator 2

            if (showCube == true)
            {
                //DrawCube();
            }

            SwapBuffers();
            //Thread.Sleep(1);
        }

        private void DrawCube()////////////////////// Cub
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.DarkViolet);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.DarkSeaGreen);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.DarkRed);

            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.DarkTurquoise);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.Magenta);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }

        private static void Main(string[] args)
        {
            using (SimpleWindow3D example = new SimpleWindow3D())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}