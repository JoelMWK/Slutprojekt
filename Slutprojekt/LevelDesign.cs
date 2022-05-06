using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;

public class LevelDesign
{
    public static int[,] level = new int[8, 16];
    public static int levelSwitch = 1;
    public static List<Rectangle> Levels(List<Rectangle> point, List<Rectangle> key)
    {
        List<Rectangle> platform = new List<Rectangle>();

        int size = 100;
        int sizeY = 80;
        int sizeX = 20;
        int coinSize = 30;

        switch (levelSwitch)
        {
            case 1:
                level = new int[8, 16]{
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,3,0,0,4,0,0,0,0,0},
        {0,0,0,0,0,0,3,0,1,1,1,1,0,0,0,0},
        {0,0,0,3,3,0,0,0,0,0,0,0,0,3,3,3},
        };
                break;

            case 2:
                level = new int[8, 16]{
        {0,0,0,0,0,0,0,2,3,3,2,0,0,0,0,0},
        {0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0},
        {0,0,3,0,2,0,0,0,0,0,0,0,0,0,0,0},
        {0,1,1,1,2,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,3,1,1,0,0,0,0,0,3,0,3},
        {2,0,0,0,3,0,0,0,0,0,0,1,1,1,1,1},
        {1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,3,3,3,0,1,0,0,3,3,3,3},
        };
                break;
        }

        for (int y = 0; y < level.GetLength(0); y++)
        {
            for (int x = 0; x < level.GetLength(1); x++)
            {
                if (level[y, x] == 1)
                {
                    platform.Add(new Rectangle(x * size, y * size + 20, size, sizeY));
                }
                else if (level[y, x] == 2)
                {
                    platform.Add(new Rectangle(x * size, y * size, sizeX, size));
                }
                else if (level[y, x] == 3)
                {
                    point.Add(new Rectangle(x * size + coinSize, y * size + coinSize, coinSize, coinSize));
                }
                else if (level[y, x] == 4)
                {
                    key.Add(new Rectangle(x * size + coinSize, y * size + coinSize, coinSize, coinSize));
                }
            }
        }
        return platform;
    }
}
