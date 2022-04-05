using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

public class Main
{
    public Rectangle playerRect = new Rectangle(40, 700, 50, 75);
    public Vector2 aniVector = new Vector2();
    public static Texture2D playerTexture = Raylib.LoadTexture("aniFront.png");
    public static Rectangle textureCutter = new Rectangle(0, 0, 50, playerTexture.height);
    public Rectangle bullet = new Rectangle(50, 50, 42, 15);
    Texture2D bulletTexture = Raylib.LoadTexture("bullet.png");
    Texture2D[] playerDirection = {
    Raylib.LoadTexture("aniFront.png"),
    Raylib.LoadTexture("aniRight.png"),
    Raylib.LoadTexture("aniLeft.png"),
    Raylib.LoadTexture("aniHitR.png"),
    Raylib.LoadTexture("aniHitL.png")
    };

    int ground = 800;
    bool inAir = false;
    float gravity = 0;
    float speedY = 8.2f;
    float speedX = 4.2f;
    float timer = 0.0f;
    int currentFrame = 0;
    int totalFrames = (int)playerTexture.width / (int)textureCutter.width;

    int height = playerTexture.height;
    int width = playerTexture.width;

    bool right = false;
    bool left = false;
    string hitDirection = "";

    public struct playerAttack
    {
        Vector2 bulletSpeed;
        bool bulletActive;
    }

    public void Anim()
    {
        timer += Raylib.GetFrameTime();
        if (timer >= 0.2f)
        {
            timer = 0.0f;
            currentFrame++;
        }
        if (currentFrame > totalFrames) currentFrame = 0;

        textureCutter.x = currentFrame * textureCutter.width;

        //Change texture on walk
        if (right == true) { playerTexture = playerDirection[1]; hitDirection = "right"; }
        if (left == true) { playerTexture = playerDirection[2]; hitDirection = "left"; }
        if (!left && !right) { playerTexture = playerDirection[0]; hitDirection = ""; }
    }

    public void Movement(Texture2D playerTexture)
    {

        playerRect.y += gravity;
        gravity += 0.3f;

        if (playerRect.y + height >= ground)
        {
            playerRect.y = ground - height;
            gravity = 0;
            inAir = false;
        }


        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && !inAir)
        {
            inAir = true;
        }

        if (inAir == true)
        {
            playerRect.y -= speedY;
        }


        if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) { playerRect.x -= speedX; left = true; }
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_A) || Raylib.IsKeyReleased(KeyboardKey.KEY_LEFT)) left = false;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) { playerRect.x += speedX; right = true; }
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_D) || Raylib.IsKeyReleased(KeyboardKey.KEY_RIGHT)) right = false;


        aniVector.X = playerRect.x;
        aniVector.Y = playerRect.y;
    }

    public void Collision(List<Rectangle> platform)
    {
        bool collisionX = false;
        bool collisionY = false;

        //Wall collision on right and left side
        if (playerRect.x <= 0)
        {
            playerRect.x = 0;
        }

        if (playerRect.x + playerTexture.width / 7 >= Raylib.GetScreenWidth())
        {
            playerRect.x = Raylib.GetScreenWidth() - width / 7;
        }

        //X and Y collsion on the game platforms
        foreach (Rectangle floor in platform)
        {
            Raylib.DrawRectangleRec(floor, Color.BROWN);

            collisionY = Raylib.CheckCollisionRecs(playerRect, floor);
            if (collisionY)
            {
                gravity = 0;
                inAir = false;
            }

            collisionX = Raylib.CheckCollisionRecs(playerRect, floor);
            if (collisionX)
            {
                if (playerRect.x <= floor.x) playerRect.x -= speedX;
                else if (playerRect.x > floor.x) playerRect.x += speedX;
            }

        }
    }
    public void Attack()
    {

        for (int i = 0; i < 3; i++)
        {

        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            if (hitDirection == "right" || hitDirection == "") playerTexture = playerDirection[3];
            else if (hitDirection == "left") playerTexture = playerDirection[4];
        }
    }
}
