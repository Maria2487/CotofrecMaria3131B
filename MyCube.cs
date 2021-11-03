using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace CotofrecMaria3131B
{
    class MyCube : MyPoint
    {
        public MyPoint[] cube;
        private bool axisVisibility;

        #region Constructor
        public MyCube()
        {
            axisVisibility = true;
        }

        public MyCube(string FileName)
        {
            axisVisibility = true;
            this.cube = ReadCoordonatesCube(FileName);
        }

        public MyCube(MyPoint[] _cube)
        {
            axisVisibility = true;
            this.cube = _cube;
        }
        #endregion

        #region ON/OFF
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
        #endregion

        public void DrawMe()
        {
            int j = 0;
            if (axisVisibility)
            {
                for (int i = 0; i < 6; i++)
                {
                    GL.Begin(PrimitiveType.Quads);
                    GL.Vertex3(cube[j].getX(), cube[j].getY(), cube[j++].getZ());
                    GL.Vertex3(cube[j].getX(), cube[j].getY(), cube[j++].getZ());
                    GL.Vertex3(cube[j].getX(), cube[j].getY(), cube[j++].getZ());
                    GL.Vertex3(cube[j].getX(), cube[j].getY(), cube[j++].getZ());
                }
                GL.End();
            }
        }

        public void DrawMe(Color[] color)
        {
            int j = 0;
            if (axisVisibility)
            {
                GL.Begin(PrimitiveType.Quads);
                for (int i = 0; i < 6; i++)
                {
                    GL.Color3(color[i]);
                    
                    GL.Vertex3(cube[j].getX(), cube[j].getY(), cube[j++].getZ());
                    GL.Vertex3(cube[j].getX(), cube[j].getY(), cube[j++].getZ());
                    GL.Vertex3(cube[j].getX(), cube[j].getY(), cube[j++].getZ());
                    GL.Vertex3(cube[j].getX(), cube[j].getY(), cube[j++].getZ());
                }
                GL.End();
            }
        }

        //private MyPoint[] ReadCoordonates(string fileName)
        //{
        //    string[] lines = File.ReadAllLines(fileName);
        //    string[] result;
        //    double[] numbers = new double[3];
        //    MyPoint[] vertex = new MyPoint[24];

        //    int j = 0;

        //    foreach (string line in lines)
        //    {
        //        int i = 0;
        //        result = line.Split(' ');
        //        foreach (string ch in result)
        //        {
        //            numbers[i] = Convert.ToDouble(ch);
        //            i++;
        //        }
        //        //vertex[j] = new MyPoint(numbers[0], numbers[1], numbers[2]);
        //        j++;
        //    }
        //    return vertex;

        //}

        private MyPoint[] ReadCoordonatesCube(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                string[] infoCord;
                MyPoint[] vectorList = new MyPoint[24];
                int i = 0;
                foreach (string line in lines)
                {
                    infoCord = line.Split(' ');
                    vectorList[i] = new MyPoint((int)Convert.ToDouble(infoCord[0]), (int)Convert.ToDouble(infoCord[1]), (int)Convert.ToDouble(infoCord[2]));
                    i++;
                }

                return vectorList;
            }
            return null;
        }
    }
}
