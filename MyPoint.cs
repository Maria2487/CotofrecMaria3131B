using System.Drawing;

namespace CotofrecMaria3131B
{
    internal class MyPoint
    {
        private float X;
        private float Y;
        private float Z;
        private Color pointColor = Color.Black;

        #region Constructori

        public MyPoint()
        {
        }

        public MyPoint(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public MyPoint(float x, float y, float z, Color color)
        {
            X = x;
            Y = y;
            Z = z;
            pointColor = color;
        }

        #endregion Constructori

        #region Setter

        public void setColor(Color color)
        {
            pointColor = color;
        }

        public void setX(float x)
        {
            X = x;
        }

        public void setY(float y)
        {
            Y = y;
        }

        public void setZ(float z)
        {
            Z = z;
        }

        #endregion Setter

        #region Getter

        public Color getColor()
        {
            return pointColor;
        }

        public float getX()
        {
            return X;
        }

        public float getY()
        {
            return Y;
        }

        public float getZ()
        {
            return Z;
        }

        #endregion Getter
    }
}