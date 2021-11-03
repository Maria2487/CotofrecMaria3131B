using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;


namespace CotofrecMaria3131B
{
    class MyAxis
    {
        private bool axisVisibility;

        private const int AXIS_LENGTH = 75;

        public MyAxis()
        {
            axisVisibility = true;
        }

        public void Show()
        {
            axisVisibility = true;
        }

        public void Hide()
        {
            axisVisibility = false;
        }

        public void ToggleVisibility()
        {
            axisVisibility = !axisVisibility;
        }

        public static void DrawAxis()///////////////////// Axele de coordonate
        {
            GL.LineWidth(1.0f);

            // Desenează axa Ox (cu roșu).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(AXIS_LENGTH, 0, 0);

            // Desenează axa Oy (cu galben).
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, AXIS_LENGTH, 0); ;

            // Desenează axa Oz (cu verde).
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, AXIS_LENGTH);
            GL.End();
        }
    }
}
