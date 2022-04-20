using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;


Raylib.InitWindow(800, 800, "Slutprojekt");
Raylib.SetTargetFPS(60);

Texture2D backdrop = Raylib.LoadTexture("ShrekSwamp.png");
List<Rectangle> platform = LevelDesign.Levels();
Main player = new Main();
MainEnemy enemy = new MainEnemy();

Camera2D camera = new Camera2D()
{
    target = new Vector2(),
    offset = new Vector2(Raylib.GetScreenWidth() / 2, 730),
    rotation = 0.0f,
    zoom = 1.0f,
};


while (!Raylib.WindowShouldClose())
{
    camera.target = new Vector2(player.playerRect.x + player.playerRect.width / 2, player.playerRect.y + player.playerRect.height / 2);

    Raylib.DrawTexture(backdrop, 0, 0, Color.WHITE);
    loadObjects();

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.SKYBLUE);
    Raylib.BeginMode2D(camera);

    Raylib.DrawTexture(enemy.enemyTexture, (int)MainEnemy.enemyRect.x, (int)MainEnemy.enemyRect.y, Color.WHITE);
    Raylib.DrawTextureRec(Main.playerTexture, Main.textureCutter, player.aniVector, Color.WHITE);

    for (int i = -400; i < 3000; i++)
    {
        Raylib.DrawTexture(Main.groundTexture, i, Main.ground, Color.WHITE);
    }
    foreach (Rectangle floor in platform)
    {
        Raylib.DrawRectangleRec(floor, Color.BROWN);
    }

    Raylib.EndMode2D();
    Raylib.EndDrawing();
}


void loadObjects()
{
    player.Anim();

    player.Movement(Main.playerTexture);
    enemy.EnemyMovement(player.playerRect, (int)player.gravity, player.enemyHp);

    player.Collision(platform);

    player.Attack(MainEnemy.enemyRect);
    player.UI();
    player.Damage();
}