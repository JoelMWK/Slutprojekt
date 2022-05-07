using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

//Skapar en class som ska innehålla saker om enemy
public class MainEnemy
{
    //enemy rektangel -- [public static] -- för att använas överallt
    public static Rectangle enemyRect = new Rectangle(1300, 445, 50, 75);
    //texur för enemy
    public Texture2D enemyTexture = Raylib.LoadTexture("enemyTextureR.png");

    //array av texture2D som innehåller olika texturer för enemy
    Texture2D[] enemyRotation = {
    Raylib.LoadTexture("enemyTexture.png"),
    Raylib.LoadTexture("enemyTextureR.png")
    };
    //hur snabbt enemy ska förflytta sig
    int speedEnemy = 2;
    //metod för enemymovement
    public void EnemyMovement()
    {
        //hur mycket enemy x-led förflyttar sig med för varje frame
        enemyRect.x += speedEnemy;
        //if stament som bestämmer vart och hur den ska åka samt vilken textur den ska ha
        if (enemyRect.x <= 1100) { speedEnemy = 2; enemyTexture = enemyRotation[1]; } //ändra på speed så att den ändrar håll samt får annan textur
        else if (enemyRect.x >= 1550) { speedEnemy = -2; enemyTexture = enemyRotation[0]; }
    }
    //metod för om enemy är vid liv -- tar in variabeln enemyHp
    public void EnemyAlive(int enemyHp)
    {
        if (Main.enemyActive) Raylib.DrawText("" + enemyHp, (int)enemyRect.x + 15, (int)enemyRect.y - 30, 20, Color.WHITE); //enemyhp counter som visas över enemy och den är vid liv
        if (LevelDesign.levelSwitch == 2) Main.enemyActive = true; //om det är andra level då ska den finnas

        if (enemyHp <= 0) Main.enemyActive = false; //om hp går under 0 då dör den
        else if (Main.enemyActive) Raylib.DrawTexture(enemyTexture, (int)enemyRect.x, (int)enemyRect.y, Color.WHITE); //ritas bara ut om den är aktiv
    }
}
