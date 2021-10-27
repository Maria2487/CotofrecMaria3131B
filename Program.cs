using OpenTK;
using OpenTK.Graphics.OpenGL;
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
        private int redC = 0, greenC = 0, blueC = 0;
        private const String FileName = @"E:\An3\CotofrecMaria3131B\triangle.txt";
        private const float rotation_speed = 180.0f;
        private float angle;
        private bool showCube = true;
        private KeyboardState lastKeyPress;
        private bool moveObjectRight, moveObjectLeft, moveMouseRight, moveMouseLeft;
        private int[] SeePort = new int[3];

        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;
            KeyDown += Keyboard_KeyDown;
            SeePort[0] = SeePort[1] = SeePort[2] = 10;
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
            //GL.Enable(EnableCap.DepthTest);
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
            moveObjectRight = false;
            moveObjectLeft = false;

            #region Modificare culoar RGB Laborator 3

            if (keyboard[OpenTK.Input.Key.G])
            {
                if (keyboard[OpenTK.Input.Key.Up])
                {
                    if (CheckIfInRangeColor(greenC))
                    {
                        greenC++;
                        Console.WriteLine("R: " + redC + " G: " + greenC + " B: " + blueC);
                    }
                }
                else if (keyboard[OpenTK.Input.Key.Down])
                {
                    if (CheckIfInRangeColor(greenC - 1))
                    {
                        greenC--;
                        Console.WriteLine("R: " + redC + " G: " + greenC + " B: " + blueC);
                    }
                }
            }

            if (keyboard[OpenTK.Input.Key.R])
            {
                if (keyboard[OpenTK.Input.Key.Up])
                {
                    if (CheckIfInRangeColor(redC))
                    {
                        redC++;
                        Console.WriteLine("R: " + redC + " G: " + greenC + " B: " + blueC);
                    }
                }
                else if (keyboard[OpenTK.Input.Key.Down])
                {
                    if (CheckIfInRangeColor(redC - 1))
                    {
                        redC--;
                        Console.WriteLine("R: " + redC + " G: " + greenC + " B: " + blueC);
                    }
                }
            }

            if (keyboard[OpenTK.Input.Key.B])
            {
                if (keyboard[OpenTK.Input.Key.Up])
                {
                    if (CheckIfInRangeColor(blueC))
                    {
                        blueC++;
                        Console.WriteLine("R: " + redC + " G: " + greenC + " B: " + blueC);
                    }
                }
                else if (keyboard[OpenTK.Input.Key.Down])
                {
                    if (CheckIfInRangeColor(blueC - 1))
                    {
                        blueC--;
                        Console.WriteLine("R: " + redC + " G: " + greenC + " B: " + blueC);
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

            if (keyboard[OpenTK.Input.Key.Left])
            {
                moveObjectLeft = true;
            }
            if (keyboard[OpenTK.Input.Key.Right])
            {
                moveObjectRight = true;
            }
            lastKeyPress = keyboard;

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
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Matrix4 lookat = Matrix4.LookAt(6, 6, 10, 0, 0, 0, 0, 1, 0);
            Matrix4 lookat = Matrix4.LookAt(SeePort[0], SeePort[1], SeePort[2], 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            DrawAxes();

            MyTriangle trg1 = MyTriangle.ReadCoordonates(FileName); //////////// Triunghi
            trg1.DrawMe(redC, greenC, blueC);

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
                DrawCube();
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

        private void DrawAxes()///////////////////// Axe de coordonate
        {
            //GL.LineWidth(3.0f);

            // Desenează axa Ox (cu roșu).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(XYZ_SIZE, 0, 0);
            GL.End();

            // Desenează axa Oy (cu galben).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, XYZ_SIZE, 0); ;
            GL.End();

            // Desenează axa Oz (cu verde).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, XYZ_SIZE);
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