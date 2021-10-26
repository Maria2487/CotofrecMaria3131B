using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
namespace CotofrecMaria3131B
{
    class MyPoint
    {
        private int X;
        private int Y;
        private int Z;
        private Color pointColor = Color.Black;

        public MyPoint()
        {

        }
        public MyPoint(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public MyPoint(int x, int y, int z, Color color)
        {
            X = x;
            Y = y;
            Z = z;
            pointColor = color;
        }

        public void setColor(Color color)
        {
            pointColor = color;
        }

        public void setX(int x)
        {
            X = x;
        }

        public void setY(int y)
        {
            Y = y;
        }

        public void setZ(int z)
        {
            Z = z;
        }

        public Color getColor()
        {
            return pointColor;
        }

        public int getX()
        {
            return X;
        }

        public int getY()
        {
            return Y;
        }

        public int getZ()
        {
            return Z;
        }
    }
}
