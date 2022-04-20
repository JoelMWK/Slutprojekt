using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;


public class MainEnemy
{

    public static Rectangle enemyRect = new Rectangle(600, 680, 50, 75);
    public Texture2D enemyTexture = Raylib.LoadTexture("enemyTexture.png");

    Texture2D[] enemyRotation = {
    Raylib.LoadTexture("enemyTexture.png"),
    Raylib.LoadTexture("enemyTextureR.png")
    };


    public void EnemyMovement(Rectangle playerRect, int gravity, int enemyHp)
    {
        enemyRect.y += gravity;

        if (enemyRect.y + Main.height >= Main.ground)
        {
            enemyRect.y = Main.ground - Main.height;
        }

        if (enemyRect.x >= playerRect.x) { enemyRect.x -= 2; enemyTexture = enemyRotation[0]; }
        else if (enemyRect.x <= playerRect.x) { enemyRect.x += 2; enemyTexture = enemyRotation[1]; }

        if (enemyHp <= 0)
        {
            enemyRect.x = 1000;
        }
    }
}
