using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

public class Main
{
    public Rectangle playerRect = new Rectangle(40, 700, 50, 84);
    public Vector2 aniVector = new Vector2();
    public static Texture2D playerTexture = Raylib.LoadTexture("aniFront.png");
    public static Rectangle textureCutter = new Rectangle(0, 0, 50, playerTexture.height);

    Texture2D[] playerDirection = {
    Raylib.LoadTexture("aniFront.png"),
    Raylib.LoadTexture("aniRight.png"),
    Raylib.LoadTexture("aniLeft.png"),
    };

    bool inAir = false;
    float gravity = 0;
    float speedY = 8.2f;
    float speedX = 3.6f;
    float timer = 0.0f;
    int currentFrame = 0;
    int totalFrames = (int)playerTexture.width / (int)textureCutter.width;


    public void Anim()
    {
        timer += Raylib.GetFrameTime();
        if (timer >= 0.1f)
        {
            timer = 0.0f;
            currentFrame++;
        }
        if (currentFrame > totalFrames) currentFrame = 0;

        textureCutter.x = currentFrame * textureCutter.width;
    }

    public void Movement(Texture2D playerTexture)
    {

        playerRect.y += gravity;
        gravity += 0.3f;

        if (playerRect.y + playerTexture.height > Raylib.GetScreenHeight())
        {
            gravity = 0;
            inAir = false;
        }


        if (playerRect.x <= 0)
        {
            playerRect.x = 0;
        }

        if (playerRect.x + playerTexture.width / 4 >= Raylib.GetScreenWidth())
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
        }



        if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) { playerRect.x -= speedX; playerTexture = playerDirection[2]; }
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_A) || Raylib.IsKeyReleased(KeyboardKey.KEY_LEFT)) playerTexture = playerDirection[0];
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) { playerRect.x += speedX; playerTexture = playerDirection[1]; }
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_D) || Raylib.IsKeyReleased(KeyboardKey.KEY_RIGHT)) playerTexture = playerDirection[0];


        aniVector.X = playerRect.x;
        aniVector.Y = playerRect.y;
    }

    public void playerCollision(List<Rectangle> platform)
    {
        foreach (Rectangle floor in platform)
        {
            Raylib.DrawRectangleRec(floor, Color.BROWN);

            if (Raylib.CheckCollisionRecs(playerRect, floor) && playerRect.x >= floor.x - playerTexture.width)
            {
                gravity = 0;
                inAir = false;
            }

        }
    }
}
