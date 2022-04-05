using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;


Raylib.InitWindow(800, 800, "Slutprojekt");
Raylib.SetTargetFPS(60);

Texture2D backdrop = Raylib.LoadTexture("ShrekSwamp.png");
List<Rectangle> platform = LevelDesign.Levels();
Main player = new Main();


while (!Raylib.WindowShouldClose())
{
    Raylib.DrawTexture(backdrop, 0, 0, Color.WHITE);
    loadObjects();

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.SKYBLUE);

    Raylib.DrawTextureRec(Main.playerTexture, Main.textureCutter, player.aniVector, Color.WHITE);
    Raylib.DrawRectangleRec(player.enemyRect, Color.BLACK);
    Raylib.DrawTexture(Main.groundTexture, 0, Main.ground, Color.WHITE);

    Raylib.EndDrawing();
}


void loadObjects()
{
    player.Anim();
    player.Movement(Main.playerTexture);
    player.Collision(platform);
    player.Attack();
    player.UI();
    player.Damage();
}