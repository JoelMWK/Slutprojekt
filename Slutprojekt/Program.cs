using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;

Raylib.InitWindow(1000, 1000, "Slutprojekt");

Rectangle playerRect = new Rectangle(0, 1000 - 60, 53, 60);
Vector2 playerVector = new Vector2(40,40);
Texture2D playerTexture = Raylib.LoadTexture("aniFront.png");
Texture2D playerTexture2 = Raylib.LoadTexture("aniLeft.png");
Texture2D playerTexture3 = Raylib.LoadTexture("aniRight.png");

Rectangle textureCutter = new Rectangle(0,0,40, playerTexture.height);

int currentFrame = 0;
int totalFrames = (int) playerTexture.width / (int) textureCutter.width;
bool leftOrRight = false;


Raylib.SetTargetFPS(30);

while(!Raylib.WindowShouldClose()){

Raylib.BeginDrawing();

Raylib.ClearBackground(Color.SKYBLUE);

textureCutter.x = currentFrame * textureCutter.width;
if(!leftOrRight)Raylib.DrawTextureRec(playerTexture, textureCutter, playerVector, Color.WHITE);
currentFrame++;
if (currentFrame > totalFrames) currentFrame = 0;

if(Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)){ playerVector.X -= 10; Raylib.DrawTextureRec(playerTexture2, textureCutter, playerVector, Color.WHITE); leftOrRight = true;}
if(Raylib.IsKeyReleased(KeyboardKey.KEY_A)) leftOrRight = false;
if(Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)){ playerVector.X += 10; Raylib.DrawTextureRec(playerTexture3, textureCutter, playerVector, Color.WHITE); leftOrRight = true;}
if(Raylib.IsKeyReleased(KeyboardKey.KEY_D)) leftOrRight = false;
if(Raylib.IsKeyDown(KeyboardKey.KEY_W) || Raylib.IsKeyDown(KeyboardKey.KEY_UP)) playerVector.Y -= 10;
if(Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_DOWN)) playerVector.Y += 10;


 //Raylib.DrawTexture(playerTexture, (int)playerRect.x, (int)playerRect.y, Color.WHITE);

Raylib.EndDrawing();

}

