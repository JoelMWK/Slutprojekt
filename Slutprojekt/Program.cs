using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;

Raylib.InitWindow(800, 800, "Slutprojekt");
Raylib.SetTargetFPS(60);

Rectangle floor = new Rectangle(200, 700, 200, 30);
Vector2 playerVector = new Vector2(40, 700);
Texture2D playerTexture = Raylib.LoadTexture("aniFront.png");

Texture2D[] playerDirection = {
    Raylib.LoadTexture("aniFront.png"),
    Raylib.LoadTexture("aniRight.png"),
    Raylib.LoadTexture("aniLeft.png"),
    };

Rectangle textureCutter = new Rectangle(0, 0, 50, playerTexture.height);

bool inAir = false;
float gravity = 0;
float speedY = 8.2f;
float speedX = 3.6f;
float timer = 0.0f;
int currentFrame = 0;
int totalFrames = (int)playerTexture.width / (int)textureCutter.width;


while (!Raylib.WindowShouldClose())
{
    timer += Raylib.GetFrameTime();
    if (timer >= 0.1f)
    {
        timer = 0.0f;
        currentFrame++;
    }
    if (currentFrame > totalFrames) currentFrame = 0;


    textureCutter.x = currentFrame * textureCutter.width;



    playerVector.Y += gravity;
    gravity += 0.3f;

    if (playerVector.Y + playerTexture.height > Raylib.GetScreenHeight())
    {
        gravity = 0;
        inAir = false;
    }


    if (playerVector.X <= 0)
    {
        playerVector.X = 0;
    }


    if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && !inAir)
    {
        inAir = true;
    }

    if (inAir == true)
    {
        playerVector.Y -= speedY;
    }

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.SKYBLUE);

    Raylib.DrawTextureRec(playerTexture, textureCutter, playerVector, Color.WHITE);
    Raylib.DrawRectangleRec(floor, Color.BLACK);

    if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) { playerVector.X -= speedX; playerTexture = playerDirection[2]; }
    if (Raylib.IsKeyReleased(KeyboardKey.KEY_A) || Raylib.IsKeyReleased(KeyboardKey.KEY_LEFT)) playerTexture = playerDirection[0];
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) { playerVector.X += speedX; playerTexture = playerDirection[1]; }
    if (Raylib.IsKeyReleased(KeyboardKey.KEY_D) || Raylib.IsKeyReleased(KeyboardKey.KEY_RIGHT)) playerTexture = playerDirection[0];

    Raylib.EndDrawing();


}