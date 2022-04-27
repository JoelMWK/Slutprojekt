using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;


public class MainEnemy
{
    public static Rectangle enemyRect = new Rectangle(1300, 445, 50, 75);
    public Texture2D enemyTexture = Raylib.LoadTexture("enemyTextureR.png");

    Texture2D[] enemyRotation = {
    Raylib.LoadTexture("enemyTexture.png"),
    Raylib.LoadTexture("enemyTextureR.png")
    };

    int speedEnemy = 2;

    public void EnemyMovement(Rectangle playerRect, int gravity, List<Rectangle> platform)
    {
        enemyRect.x += speedEnemy;

        if (enemyRect.x <= 1100) { speedEnemy = 2; enemyTexture = enemyRotation[1]; }
        else if (enemyRect.x >= 1550) { speedEnemy = -2; enemyTexture = enemyRotation[0]; }
    }

    public void EnemyAlive(int enemyHp)
    {
        if (enemyHp <= 0) Main.enemyActive = false;
        else Raylib.DrawTexture(enemyTexture, (int)enemyRect.x, (int)enemyRect.y, Color.WHITE);
    }
}
