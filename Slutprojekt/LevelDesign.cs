using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;

//skaper en class för levelDesign
public class LevelDesign
{
    public static int[,] level = new int[8, 16]; //tvådimensionell array -- 8 y-led, 16 x-led
    public static int levelSwitch = 1; //int för att byta case
    //metod för leveln
    public static List<Rectangle> Levels(List<Rectangle> point, List<Rectangle> key) //tar in listor
    {
        List<Rectangle> platform = new List<Rectangle>(); //skapar platform listan

        //variablar för storlek på platform, coin
        int size = 100;
        int sizeY = 80;
        int sizeX = 20;
        int coinSize = 30;

        switch (levelSwitch) //en mutiway statement -- som byter mellan cases "lite som en if stament till en else if stament"
        {
            case 1: //första casen (banan) har specifierade positioner av 1,2,3,4,0
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

            case 2: //andra casen (banan) har specifierade positioner av 1,2,3,4,0
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

        for (int y = 0; y < level.GetLength(0); y++) //for loop som läser in y-led i arrayen -- GetLength(0) tar i första dimensionen (y-led)
        {
            for (int x = 0; x < level.GetLength(1); x++)//for loop som läser in x-led i arrayen -- GetLength(1) tar i första dimensionen (x-led)
            {   //i = 1,2,3 eller 4
                //om "i" är i arrayen så ska en speciell rektangel läggas till i en lista
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
        return platform; //returnerar platformen
    }
}
