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

    Rectangle healthBarRect = new Rectangle(10, 10, 170, 13);
    Rectangle lostHealthRect = new Rectangle(180, 10, 0, 13);
    Texture2D healthBar = Raylib.LoadTexture("healthbar.png");


    Rectangle bullet = new Rectangle(0, 0, 40, 6);
    public static Texture2D groundTexture = Raylib.LoadTexture("floor.png");


    Texture2D[] playerDirection = {
    Raylib.LoadTexture("aniFront.png"),
    Raylib.LoadTexture("aniRight.png"),
    Raylib.LoadTexture("aniLeft.png")
    };

    Texture2D[] playerAction = {
    Raylib.LoadTexture("aniHitR.png"),
    Raylib.LoadTexture("aniHitL.png"),
    Raylib.LoadTexture("dead.png")
    };

    public static int ground = Raylib.GetScreenHeight() - groundTexture.height;
    bool inAir = false;
    public float gravity = 0;
    float speedY = 8.2f;
    float speedX = 4.2f;
    int hp = 10;
    public int enemyHp = 10;
    double damageImmune = 0;
    float timer = 0.0f;
    int currentFrame = 0;
    int totalFrames = (int)playerTexture.width / (int)textureCutter.width;

    public static int height = playerTexture.height;
    public static int width = playerTexture.width;

    public static bool enemyActive = true;
    bool right = false;
    bool left = false;

    public void Anim()
    {
        timer += Raylib.GetFrameTime();
        if (timer >= 0.16f)
        {
            timer = 0.0f;
            currentFrame++;
        }
        if (currentFrame > totalFrames) currentFrame = 0;

        textureCutter.x = currentFrame * textureCutter.width;

        //Change texture on walk
        if (right == true) { playerTexture = playerDirection[1]; }
        if (left == true) { playerTexture = playerDirection[2]; }
        if (!left && !right) { playerTexture = playerDirection[0]; }
    }


    public void UI()
    {
        Raylib.DrawRectangleRec(healthBarRect, Color.GREEN);
        Raylib.DrawRectangleRec(lostHealthRect, Color.RED);
        Raylib.DrawTexture(healthBar, 0, 0, Color.WHITE);
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

        if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) { playerRect.x -= speedX; left = true; }
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_A)) left = false;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) { playerRect.x += speedX; right = true; }
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_D)) right = false;

        aniVector.X = playerRect.x;
        aniVector.Y = playerRect.y;
    }


    public void Collision(List<Rectangle> platform)
    {
        bool collisionX = false;
        bool collisionY = false;

        //Wall collision on right and left side
        if (playerRect.x <= 0) playerRect.x = 0;

        if (playerRect.x + playerTexture.width / 6 >= Raylib.GetScreenWidth() * 4)
        {
            playerRect.x = Raylib.GetScreenWidth() * 4 - width / 6;
        }

        //X and Y collsion on the game platforms
        foreach (Rectangle floor in platform)
        {
            collisionY = Raylib.CheckCollisionRecs(playerRect, floor);
            if (collisionY)
            {
                if (playerRect.y + playerTexture.height < floor.y + floor.height)
                {
                    gravity = 0;
                    playerRect.y = floor.y - playerTexture.height;
                    inAir = false;
                }
                else if (playerRect.y > floor.y - floor.height) playerRect.y += speedY;
            }

            collisionX = Raylib.CheckCollisionRecs(playerRect, floor);
            if (collisionX)
            {
                if (playerRect.x <= floor.x) playerRect.x -= speedX;
                else if (playerRect.x > floor.x) playerRect.x += speedX;

            }

            if (Raylib.CheckCollisionRecs(MainEnemy.enemyRect, floor) && enemyActive == true)
            {
                if (MainEnemy.enemyRect.x <= floor.x)
                {
                    MainEnemy.enemyRect.x -= 2;
                    MainEnemy.enemyRect.y -= 8.2f;
                }
                else if (MainEnemy.enemyRect.x > floor.x)
                {
                    MainEnemy.enemyRect.x += 2;
                    MainEnemy.enemyRect.y -= 8.2f;
                }
            }
        }

        if (Raylib.CheckCollisionRecs(playerRect, MainEnemy.enemyRect) && enemyActive == true)
        {
            int lostWidth = 17;
            if (Raylib.GetTime() - damageImmune >= 1)
            {
                damageImmune = Raylib.GetTime();
                hp--;

                healthBarRect.width -= lostWidth;
                lostHealthRect.width += lostWidth;
                lostHealthRect.x -= lostWidth;
            }
        }
    }

    public void Damage()
    {
        if (hp <= 0)
        {
            playerTexture = playerAction[2];
            playerRect.x = 0;
            gravity = speedY;
            Raylib.DrawText("YOU DIED!", 300, 400, 50, Color.RED);
        }
    }

    public bool Attack(Rectangle enemyRect)
    {
        if (enemyActive) Raylib.DrawText("" + enemyHp, (int)enemyRect.x + 15, (int)enemyRect.y - 30, 20, Color.WHITE);

        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            playerTexture = playerAction[0];

            bullet.x = playerRect.x + 50;
            bullet.y = playerRect.y + 30;
            Raylib.DrawRectangleRec(bullet, Color.WHITE);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            playerTexture = playerAction[1];

            bullet.x = playerRect.x - 40;
            bullet.y = playerRect.y + 30;
            Raylib.DrawRectangleRec(bullet, Color.WHITE);
        }
        else return false;


        if (Raylib.CheckCollisionRecs(bullet, enemyRect))
        {
            if (Raylib.GetTime() - damageImmune >= 0.4f)
            {
                damageImmune = Raylib.GetTime();
                enemyHp--;
            }
        }
        return true;
    }
}

