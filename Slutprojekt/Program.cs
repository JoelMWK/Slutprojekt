using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;


Raylib.InitWindow(800, 800, "Slutprojekt");
Raylib.SetTargetFPS(60);

List<Rectangle> platform = LevelDesign.Levels();
Main player = new Main();


while (!Raylib.WindowShouldClose())
{
    loadObjects();

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.SKYBLUE);

    Raylib.DrawTextureRec(Main.playerTexture, Main.textureCutter, player.aniVector, Color.WHITE);
    Raylib.DrawRectangleRec(player.playerRect, Color.WHITE);

    Raylib.EndDrawing();
}


void loadObjects()
{
    player.Anim();
    player.Movement(Main.playerTexture);
    player.playerCollision(platform);
}