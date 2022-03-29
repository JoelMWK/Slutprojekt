using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;

Raylib.InitWindow(800, 800, "Slutprojekt");
Raylib.SetTargetFPS(60);


List<Rectangle> platform = Level();
Vector2 playerVector = new Vector2();
Texture2D playerTexture = Raylib.LoadTexture("aniFront.png");
Rectangle playerRect = new Rectangle(40, 700, 50, 84);

Texture2D[] playerDirection = {
    Raylib.LoadTexture("aniFront.png"),
    Raylib.LoadTexture("aniRight.png"),
    Raylib.LoadTexture("aniLeft.png"),
    };

Rectangle textureCutter = new Rectangle(0, 0, 50, playerTexture.height);


bool inAir = false;
float gravity = 0;
float speedY = 9.2f;
float speedX = 3.6f;
float timer = 0.0f;
int currentFrame = 0;
int totalFrames = (int)playerTexture.width / (int)textureCutter.width;


while (!Raylib.WindowShouldClose())
{


    playerVector.X = playerRect.x;
    playerVector.Y = playerRect.y;

    timer += Raylib.GetFrameTime();
    if (timer >= 0.1f)
    {
        timer = 0.0f;
        currentFrame++;
    }
    if (currentFrame > totalFrames) currentFrame = 0;


    textureCutter.x = currentFrame * textureCutter.width;


    playerRect.y += gravity;
    playerVector.Y += gravity;
    gravity += 0.3f;

    if (playerRect.y + playerTexture.height >= Raylib.GetScreenHeight())
    {
        gravity = 0;
        inAir = false;
    }


    if (playerRect.x <= 0)
    {
        playerRect.x = 0;
    }
    else if (playerRect.x + playerTexture.width / 4 >= Raylib.GetScreenWidth())
    {
        playerRect.x = Raylib.GetScreenWidth() - playerTexture.width / 4;
    }


    if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && !inAir)
    {
        inAir = true;
    }

    if (inAir == true)
    {
        playerRect.y -= speedY;
        playerVector.Y -= speedY;
    }





    foreach (Rectangle floor in platform)
    {
        Raylib.DrawRectangle((int)floor.x, (int)floor.y, 100, 20, Color.WHITE);
        //Raylib.DrawRectangleRec(floor, Color.BLACK);


        if (Raylib.CheckCollisionRecs(playerRect, floor) && floor.y - playerTexture.height <= playerRect.y &&
        floor.y + floor.height >= playerRect.y + playerTexture.height)
        {
            gravity = 0;
            inAir = false;
        }


        /* if (floor.x - 25 <= playerVector.X &&
       floor.x + floor.width >= playerVector.X &&
       floor.y - playerTexture.height <= playerVector.Y &&
       floor.y + floor.height >= playerVector.Y + playerTexture.height)
         {
             Raylib.DrawText("DAWDAWD", 200, 200, 80, Color.BLACK);
             gravity = 0;
             inAir = false;
         }*/

    }


    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.SKYBLUE);

    Raylib.DrawTextureRec(playerTexture, textureCutter, playerVector, Color.WHITE);

    //Raylib.DrawRectangleRec(playerRect, Color.WHITE);

    if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) { playerRect.x -= speedX; playerTexture = playerDirection[2]; }
    if (Raylib.IsKeyReleased(KeyboardKey.KEY_A) || Raylib.IsKeyReleased(KeyboardKey.KEY_LEFT)) playerTexture = playerDirection[0];
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) { playerRect.x += speedX; playerTexture = playerDirection[1]; }
    if (Raylib.IsKeyReleased(KeyboardKey.KEY_D) || Raylib.IsKeyReleased(KeyboardKey.KEY_RIGHT)) playerTexture = playerDirection[0];

    Raylib.EndDrawing();


}


static List<Rectangle> Level()
{
    int[,] level = new int[8, 8]{
            {0,0,0,1,1,0,0,0},
            {1,0,0,0,0,0,0,0},
            {0,0,1,0,0,1,0,0},
            {0,0,0,0,0,0,1,0},
            {0,0,0,1,1,0,0,0},
            {1,1,0,0,0,0,1,1},
            {0,0,1,0,0,1,0,0},
            {1,0,0,0,0,0,0,1},
        };
    List<Rectangle> platform = new List<Rectangle>();
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

