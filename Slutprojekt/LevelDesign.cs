using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;

public class LevelDesign
{
    public static List<Rectangle> Levels()
    {
        List<Rectangle> platform = new List<Rectangle>();

        int[,] level = new int[8, 32]{
        {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {2,0,0,0,0,0,0,0,3,3,3,3,3,3,3,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {2,0,0,0,0,1,1,0,1,1,1,1,2,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {2,0,0,1,0,0,0,0,0,0,0,0,2,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {2,0,0,0,0,1,1,1,0,0,0,0,2,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {1,0,0,0,0,0,0,0,0,1,1,1,2,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,1,1,1,1,0,0,0,0,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };

        int size = 100;
        int sizeY = 20;
        int sizeX = 20;

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
                    platform.Add(new Rectangle(x * size, y * size, size, size));
                }
            }
        }
        return platform;
    }

}
