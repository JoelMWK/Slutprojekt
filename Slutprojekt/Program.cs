using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;


Raylib.InitWindow(800, 800, "Slutprojekt");
Raylib.SetTargetFPS(60);

Texture2D backdrop = Raylib.LoadTexture("ShrekSwamp.png");
Rectangle pointRect = new Rectangle();
int score = 0;
List<Rectangle> point = new List<Rectangle>();
List<Rectangle> platform = LevelDesign.Levels(point);
Main player = new Main();
MainEnemy enemy = new MainEnemy();

Camera2D camera = new Camera2D()
{
    target = new Vector2(),
    offset = new Vector2(Raylib.GetScreenWidth() / 3, Raylib.GetScreenHeight() / 1.5f),
    rotation = 0.0f,
    zoom = 1.0f,
};

while (!Raylib.WindowShouldClose())
{
    camera.target = new Vector2(player.playerRect.x + player.playerRect.width / 2, player.playerRect.y + player.playerRect.height / 2);
    Raylib.DrawTexture(backdrop, 0, 0, Color.WHITE);

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.SKYBLUE);
    Raylib.BeginMode2D(camera);
    loadObjects();

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

    Raylib.EndMode2D();
    player.UI(score);
    Raylib.EndDrawing();
}


void loadObjects()
{
    player.Anim();
    enemy.EnemyAlive(player.enemyHp);

    player.Movement(Main.playerTexture);
    enemy.EnemyMovement(player.playerRect, (int)player.gravity, platform);

    player.Collision(platform);

    player.Attack(MainEnemy.enemyRect);
    player.Damage();
}