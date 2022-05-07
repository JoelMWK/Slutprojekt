using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

//skaper en class som innehåller playern
public class Main
{
    //Skapar rektanglar, vector2 och textur för player -- [public...] för att det ska kunnas användas utanför classen
    public Rectangle playerRect = new Rectangle(40, 700, 50, 75);
    public Vector2 aniVector = new Vector2();
    public static Texture2D playerTexture = Raylib.LoadTexture("aniFront.png");
    public static Rectangle textureCutter = new Rectangle(0, 0, 50, playerTexture.height); //rektangle som definerar hur texturen ska delas för animationen

    //Rectangle/textur för healthbar
    Rectangle healthBarRect = new Rectangle(10, 10, 170, 13);
    Rectangle lostHealthRect = new Rectangle(180, 10, 0, 13);
    Texture2D healthBar = Raylib.LoadTexture("healthbar.png");

    //rectangle för bullet
    Rectangle bullet = new Rectangle(0, 0, 40, 6);
    //texturen för marken
    public static Texture2D groundTexture = Raylib.LoadTexture("floor.png");

    //array av texturer för player
    //idle, walk right och walk left
    Texture2D[] playerDirection = {
    Raylib.LoadTexture("aniFront.png"),
    Raylib.LoadTexture("aniRight.png"),
    Raylib.LoadTexture("aniLeft.png")
    };
    //textur för hur den ser ut när man attakerar åt höger/vänster
    //textur för när man dör
    Texture2D[] playerAction = {
    Raylib.LoadTexture("aniHitR.png"),
    Raylib.LoadTexture("aniHitL.png"),
    Raylib.LoadTexture("dead.png")
    };
    //variablar för spelet
    public static int ground = 800; //vart marken befinner sig
    bool inAir = false; //bool för om man är i luften eller inte
    public float gravity = 0; //gravity för spelet för hopp
    float speedY = 11.2f; //med vilken kraft man hoppar med y-led
    float speedX = 4.2f; //med vilken kraft man går med i x-led
    int hp = 10; //hp för player
    public int enemyHp = 10; //hp för enemy
    double damageImmune = 0; //double som används för att man inte ska kunna attackera för varje frame
    float timer = 0.0f; //för animation av player texture
    int currentFrame = 0;//tar reda på vilken frame som sk användas
    int totalFrames = (int)playerTexture.width / (int)textureCutter.width; //definerar hur många frames det finns av player animationen (sprite sheet delas upp med 50 width)

    public static int height = playerTexture.height; //variabler för höjden av playertexturen
    public static int width = playerTexture.width; //variabel för width av playertexuren

    public static bool enemyActive = false; //bool som säger om enemy är aktiv eller inte
    bool right = false; //bool som kollar om man går åt höger
    bool left = false; //bool som kollar om man går åt vänster

    //metod för animation av player texturen
    public void Anim()
    {
        //tar reda på hur länge det har gått mellan varje frame
        //lägger på det till timer för varje frame
        timer += Raylib.GetFrameTime();
        if (timer >= 0.16f) //spelas upp varje 160ms av en sekund
        {
            timer = 0.0f; //resetar timer för så att den ska kunna spelas igen
            currentFrame++; //currentframe ökar med 1 och det gör så att nästa bild i sprite sheeten kommer att visas
        }
        if (currentFrame > totalFrames) currentFrame = 0; //om framen som visas är sista framen så resetas den

        //x positionen av textureCutter kommer ändras för varje frame t.ex första är 0 andra 50. tredje 100 osv -- tar reda på vilken del som ska visas
        textureCutter.x = currentFrame * textureCutter.width;

        //Ändrar textur vid gång
        if (right == true) { playerTexture = playerDirection[1]; }
        if (left == true) { playerTexture = playerDirection[2]; }
        if (!left && !right) { playerTexture = playerDirection[0]; }
    }

    //metod för game user interface
    public void UI(int score)
    {
        //Ritar ut healthbar rekanglar och score text
        Raylib.DrawRectangleRec(healthBarRect, Color.GREEN);
        Raylib.DrawRectangleRec(lostHealthRect, Color.RED);
        Raylib.DrawTexture(healthBar, 0, 0, Color.WHITE);
        Raylib.DrawText("Score:" + score, Raylib.GetScreenWidth() - 150, 20, 30, Color.WHITE);
    }

    //metod för playerMovement
    public void Movement(Texture2D playerTexture)
    {
        playerRect.y += gravity; //player y-led påverkas av gravitationen för varje frame
        gravity += 0.3f; //ökar varje frame som gör så att player.y ökar i hastigheten vid fallet

        if (playerRect.y + height >= ground) // checkar om player.y är vid markens y kordinat
        {
            playerRect.y = ground - height; //sätter player på marken
            gravity = 0; //gravitationen blir 0 -- eftersom player är inte i ett hopp
            inAir = false; //bool för hopp -- player är inte i ett hopp
        }

        //Om man trycker på spacebar så ska man hoppa
        //Om inAir är falskt då ska man kunna trycka på spacebar (så att man inte kan hoppa flera gånger i luften)
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && !inAir)
        {
            inAir = true; //boolen är true
        }

        if (inAir == true) //om man är i ett hopp så ska player.y åka upp med speedY
        {
            playerRect.y -= speedY;
        }

        //när man trycker på A eller D så går player med en viss hastigheten i x-led samt ändrar textur med hjälp av en bool
        //när man släpper knappen ändras boolen till false som gör så att terxturen blir idle
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) { playerRect.x -= speedX; left = true; }
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_A)) left = false;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) { playerRect.x += speedX; right = true; }
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_D)) right = false;

        //säger att vector2 x och y position är pkayerRect x och y position
        //används för att underlätta kollision senare
        aniVector.X = playerRect.x;
        aniVector.Y = playerRect.y;
    }

    //metod för kollision
    public void Collision(List<Rectangle> platform)
    {
        //delar upp x och y kollisionen
        bool collisionX = false;
        bool collisionY = false;

        //Kollisionen på väggen till vänster och höger
        if (playerRect.x <= 0) playerRect.x = 0;

        if (playerRect.x + width / 6 >= Raylib.GetScreenWidth() * 2)
        {
            playerRect.x = Raylib.GetScreenWidth() * 2 - width / 6;
        }

        //x och y kollision på spelets platformar
        foreach (Rectangle floor in platform) //tar in positionen av platform listan
        {
            collisionY = Raylib.CheckCollisionRecs(playerRect, floor);
            if (collisionY) // y kollision
            {
                if (playerRect.y + height <= floor.y + floor.height) //körs om player.y är på platformens ovansida
                {
                    //samma princip som golvet
                    gravity = 0;
                    playerRect.y = floor.y - height;
                    inAir = false;
                }
                else if (playerRect.y > floor.y - floor.height) //om man hoppar up på platformen undersida så går man minus det man hoppa med
                {
                    playerRect.y += speedY;
                }
            }

            collisionX = Raylib.CheckCollisionRecs(playerRect, floor);
            if (collisionX) // x kollision
            {
                if (playerRect.x <= floor.x) playerRect.x -= speedX; //om man går in på vänstra sidan så går man minus det man gick in med
                else if (playerRect.x > floor.x) playerRect.x += speedX; //om man går in på högra sidan går man plus det man gick in med
            }
        }

        if (Raylib.CheckCollisionRecs(playerRect, MainEnemy.enemyRect) && enemyActive == true) //kollsionen mellan player och enemy
        {
            int lostWidth = 17; //variabel för hur mycket rekanglarna för UI ska gå ned med
            if (Raylib.GetTime() - damageImmune >= 0.3f) //timer som ser till att man inte kan ta skada varje frame och istället vid 300ms av en sekund
            {
                damageImmune = Raylib.GetTime();
                hp--;

                healthBarRect.width -= lostWidth;
                lostHealthRect.width += lostWidth;
                lostHealthRect.x -= lostWidth;
            }
        }
    }
    //metod för vad som händer när man tagit för mycket damage
    public void Damage()
    {
        if (hp <= 0) //om man har 0 hp
        {
            playerTexture = playerAction[2]; //döds textur
            playerRect.x = 0; //flyttar x positionen av player
            gravity = speedY; //för att stoppa player
            Raylib.DrawText("YOU DIED!", 300, 400, 50, Color.RED);
        }
    }
    //metod för attack
    public bool Attack(Rectangle enemyRect)
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) //om man trycker right arrow så får man en viss textur och bullet visas höger om karaktären
        {
            playerTexture = playerAction[0];

            bullet.x = playerRect.x + 50;
            bullet.y = playerRect.y + 30;
            Raylib.DrawRectangleRec(bullet, Color.WHITE);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))//om man trycker left arrow så får man en viss textur och bullet visas vänster om karaktären
        {
            playerTexture = playerAction[1];

            bullet.x = playerRect.x - 40;
            bullet.y = playerRect.y + 30;
            Raylib.DrawRectangleRec(bullet, Color.WHITE);
        }
        else return false; //returnerar false om man inte attackerar -- för att bulleten inte ska kunna göra damage om man släpper


        if (Raylib.CheckCollisionRecs(bullet, enemyRect)) //timer som kollar efter hur lång tid man ska kunna attackera -- 800ms av en sekund
        {
            if (Raylib.GetTime() - damageImmune >= 0.8f)
            {
                damageImmune = Raylib.GetTime();
                enemyHp--;
            }
        }
        return true;
    }
}

