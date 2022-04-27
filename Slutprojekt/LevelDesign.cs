using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;

public class LevelDesign
{
    public static int[,] level = new int[8, 32]{
        {0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,3,3,3,3,3,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,4,3,3,0,0,0,0,0,4,0,4,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,4,0,0,0,0,0,0,3,3,3,3,3,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,4,4,4,0,3,0,0,4,4,4,4,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };
    public static List<Rectangle> Levels(List<Rectangle> point)
    {
        List<Rectangle> platform = new List<Rectangle>();


        int size = 100;
        int sizeY = 20;
        int sizeX = 20;
        int coinSize = 30;

        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 32; x++)
            {
                if (level[y, x] == 1)
                {
                    platform.Add(new Rectangle(x * size, y * size, size, sizeY));
                }
                else if (level[y, x] == 2)
                {
                    platform.Add(new Rectangle(x * size, y * size, sizeX, size));
                }
                else if (level[y, x] == 3)
                {
                    platform.Add(new Rectangle(x * size, y * size, size, 80));
                }
                else if (level[y, x] == 4)
                {
                    point.Add(new Rectangle(x * size + coinSize, y * size + coinSize, coinSize, coinSize));
                }
            }
        }
        return platform;
    }

}
