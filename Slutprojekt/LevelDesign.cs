using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;

public class LevelDesign
{
    public static List<Rectangle> Levels()
    {
        List<Rectangle> platform = new List<Rectangle>();

        int[,] level = new int[8, 8]{
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,1,0,0,0,0},
        };

        int size = 100;
        int sizeY = 20;

        for (int y = 0; y < level.GetLength(1); y++)
        {
            for (int x = 0; x < level.GetLength(0); x++)
            {
                if (level[y, x] == 1)
                {
                    platform.Add(new Rectangle(x * size, y * size, size, sizeY));
                }
            }

        }
        return platform;
    }

}
