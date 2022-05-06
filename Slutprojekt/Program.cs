using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;


Raylib.InitWindow(800, 800, "Slutprojekt");
Raylib.SetTargetFPS(60);

int score = 0;
bool keyTaken = false;

Texture2D close = Raylib.LoadTexture("close.png");
Texture2D mid = Raylib.LoadTexture("mid.png");


Texture2D backdrop = Raylib.LoadTexture("ShrekSwamp.png");
Texture2D keyTexture = Raylib.LoadTexture("key.png");

Rectangle pointRect = new Rectangle();
Rectangle doorRect = new Rectangle(200, 700, 60, 100);

List<Rectangle> point = new List<Rectangle>();
List<Rectangle> key = new List<Rectangle>();
List<Rectangle> platform = LevelDesign.Levels(point, key);
Main player = new Main();
MainEnemy enemy = new MainEnemy();

Camera2D camera = new Camera2D()
{
    target = new Vector2(),
    offset = new Vector2(Raylib.GetScreenWidth() / 3, Raylib.GetScreenHeight() / 1.5f),
    rotation = 0.0f,
    zoom = 1.0f,
};
int currentPage = 1;
int bX = 0, bX2 = 0;

while (!Raylib.WindowShouldClose())
{
    if (currentPage == 1)
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.SKYBLUE);

        Raylib.DrawText("Platformer Game", 180, 100, 50, Color.BLACK);
        Raylib.DrawText("Press Enter To Start Game", 220, 400, 25, Color.BLACK);

        Raylib.DrawText("Controls", 350, 500, 25, Color.BLACK);
        Raylib.DrawText("Movement: A and D", 300, 550, 25, Color.WHITE);
        Raylib.DrawText("Jump: Spacebar", 300, 600, 25, Color.WHITE);
        Raylib.DrawText("Attack: <- and ->", 300, 650, 25, Color.WHITE);

        Raylib.EndDrawing();

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) currentPage = 2;

    }

    else if (currentPage == 2)
    {
        if (keyTaken && Raylib.CheckCollisionRecs(player.playerRect, doorRect))
        {
            LevelDesign.levelSwitch = 2;
            UpdateMap();
            keyTaken = false;
        }
        camera.target = new Vector2(player.playerRect.x + player.playerRect.width / 2, player.playerRect.y + player.playerRect.height / 2);

        DrawBackground();

        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.SKYBLUE);
        Raylib.BeginMode2D(camera);
        LoadObjects();

        DrawGame();
        DrawKey();
        DrawPoints();

        Raylib.EndMode2D();
        player.UI(score);
        Raylib.EndDrawing();
    }
}



void DrawGame()
{
    Raylib.DrawRectangleRec(doorRect, Color.GREEN);

    Raylib.DrawTextureRec(Main.playerTexture, Main.textureCutter, player.aniVector, Color.WHITE);

    Raylib.DrawRectangle(0, 800, Raylib.GetScreenWidth() * 2, 400, Color.DARKBROWN);
    for (int i = 0; i < Raylib.GetScreenWidth() * 2; i += Main.groundTexture.width)
    {
        Raylib.DrawTexture(Main.groundTexture, i, Main.ground, Color.WHITE);
    }
    foreach (Rectangle floor in platform)
    {
        Raylib.DrawRectangleRec(floor, Color.GRAY);
    }
}

void DrawPoints()
{
    for (int i = 0; i < point.Count; i++)
    {
        pointRect = point[i];
        Raylib.DrawRectangleRounded(pointRect, 1, 0, Color.YELLOW);
        if (Raylib.CheckCollisionRecs(player.playerRect, pointRect))
        {
            point.RemoveAt(i);
            score++;
        }
    }
}

void DrawKey()
{
    for (int j = 0; j < key.Count; j++)
    {
        Rectangle keyRect = new Rectangle();
        keyRect = key[j];

        Raylib.DrawTexture(keyTexture, (int)keyRect.x, (int)keyRect.y, Color.WHITE);
        if (Raylib.CheckCollisionRecs(keyRect, player.playerRect))
        {
            key.RemoveAt(j);
            keyTaken = true;
        }
    }
}

void DrawBackground()
{
    Raylib.DrawTexture(mid, bX2, 0, Color.WHITE);
    Raylib.DrawTexture(mid, bX2 + mid.width, 0, Color.WHITE);
    Raylib.DrawTexture(close, bX, 0, Color.WHITE);
    Raylib.DrawTexture(close, bX + close.width, 0, Color.WHITE);

    bX -= 1;
    bX2 -= 2;

    if (bX <= -close.width) bX = 0;
    if (bX2 <= -mid.width) bX2 = 0;
}

void LoadObjects()
{
    player.Anim();
    enemy.EnemyAlive(player.enemyHp);

    player.Movement(Main.playerTexture);
    enemy.EnemyMovement(player.playerRect, (int)player.gravity, platform);

    player.Collision(platform);

    player.Attack(MainEnemy.enemyRect);
    player.Damage();
}

void UpdateMap()
{
    platform = LevelDesign.Levels(point, key);
}