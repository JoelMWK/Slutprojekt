using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;

//hur stor viewporten ska vara och hur många frames per sekund spelet ska ha
Raylib.InitWindow(800, 800, "Slutprojekt");
Raylib.SetTargetFPS(60);

//variablar for spelet
int score = 0;
bool keyTaken = false;
int currentPage = 1;
int bX = 0, bX2 = 0;

//laddar in textur för bakgrunden
Texture2D close = Raylib.LoadTexture("close.png");
Texture2D mid = Raylib.LoadTexture("mid.png");

//laddar in textur för nyckeln
Texture2D keyTexture = Raylib.LoadTexture("key.png");

//skapar rectanglar för poäng och dörren
Rectangle pointRect = new Rectangle();
Rectangle doorRect = new Rectangle(200, 700, 60, 100);

//listor för mapDesign, point och key. Listorna innehåller rektanglar
//platform laddar in LevelDeign.Level
List<Rectangle> point = new List<Rectangle>();
List<Rectangle> key = new List<Rectangle>();
List<Rectangle> platform = LevelDesign.Levels(point, key);

//skaper ett objekt från classen Main och MainEnemy
Main player = new Main();
MainEnemy enemy = new MainEnemy();

//skapar en Camera2D som innehåller olika variablar,
//som definerar hur kameran kommer att se ut vid start
Camera2D camera = new Camera2D()
{
    target = new Vector2(),
    offset = new Vector2(Raylib.GetScreenWidth() / 3, Raylib.GetScreenHeight() / 1.5f),
    rotation = 0.0f,
    zoom = 1.0f,
};

//While loop som kommer spelas tills man trycker på ESC
while (!Raylib.WindowShouldClose())
{
    //If statement för startscreen
    if (currentPage == 1)
    {
        //startar drawing
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.SKYBLUE);

        //Skriver ut text på skärmen för controls
        Raylib.DrawText("Platformer Game", 180, 100, 50, Color.BLACK);
        Raylib.DrawText("Press Enter To Start Game", 220, 400, 25, Color.BLACK);
        Raylib.DrawText("Controls", 350, 500, 25, Color.BLACK);
        Raylib.DrawText("Movement: A and D", 300, 550, 25, Color.WHITE);
        Raylib.DrawText("Jump: Spacebar", 300, 600, 25, Color.WHITE);
        Raylib.DrawText("Attack: <- and ->", 300, 650, 25, Color.WHITE);

        //slutar drawing
        Raylib.EndDrawing();
        //If statment för keypress som byter sida -- till gamepage
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) currentPage = 2;
    }
    //else if statment för gamepage -- defineras om startscreen inte är aktiv
    else if (currentPage == 2)
    {
        //Definerar kamerans target (hur den ska förblytta sig) -- uppdateras varje frame
        camera.target = new Vector2(player.playerRect.x + player.playerRect.width / 2, player.playerRect.y + player.playerRect.height / 2);

        //checkar om man har tagit upp nyckeln och om man nuddar dörren
        if (keyTaken && Raylib.CheckCollisionRecs(player.playerRect, doorRect))
        {
            //uppdaterar mappen och ger false för keyTaken då den användes
            LevelDesign.levelSwitch = 2;
            UpdateMap();
            keyTaken = false;
        }
        //Ritar ut spelet
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.SKYBLUE);
        DrawBackground();
        Raylib.BeginMode2D(camera); //startar camera trackingen efter karaktären
        LoadObjects();

        DrawGame();
        DrawKey();
        DrawPoints();

        Raylib.EndMode2D(); //slutar camera
        //dessa är utanför camera eftersom jag vill att det ska ha samma position hela tiden
        //och inte flyttas med kameran
        player.UI(score);
        if (!Main.enemyActive && point.Count == 0) //vad som händer när man dödat enemy och tagit alla poäng
        {
            Raylib.DrawText("YOU WON!", 400, 400, 50, Color.BLACK);
        }
        Raylib.EndDrawing(); //slutar drawing
    }
}


//metod för spelets texturer samt spelaren
void DrawGame()
{
    //dörr
    Raylib.DrawRectangleRec(doorRect, Color.GREEN);
    //player
    Raylib.DrawTextureRec(Main.playerTexture, Main.textureCutter, player.aniVector, Color.WHITE);

    Raylib.DrawRectangle(0, 800, Raylib.GetScreenWidth() * 2, 400, Color.DARKBROWN);
    //for loop för att loopa om texturen för golvet så att den ska täcka hela mappen
    //(två viewport width)
    for (int i = 0; i < Raylib.GetScreenWidth() * 2; i += Main.groundTexture.width)
    {
        Raylib.DrawTexture(Main.groundTexture, i, Main.ground, Color.WHITE);
    }
    //foreach loop som tar in platform listan som innehålle olika positioner
    //definerad i LevelDesign
    foreach (Rectangle floor in platform)
    {
        Raylib.DrawRectangleRec(floor, Color.GRAY);
    }
}

//metod för att rita ut poängen
void DrawPoints()
{
    //for loop som ritar ut poängen för definerade positionen
    for (int i = 0; i < point.Count; i++) //definerar hur lång loopen är (point.Count)
    {
        pointRect = point[i]; //säger att varje point[i] position ska defineras till en Rektangle (pointRect)
        Raylib.DrawRectangleRounded(pointRect, 1, 0, Color.YELLOW); //ritar ut
        if (Raylib.CheckCollisionRecs(player.playerRect, pointRect)) //collision
        {//om man kolliderar så tar point(i) bort från mappen
            point.RemoveAt(i);
            score++; //poäng går upp
        }
    }
}

//metod för nyckeln
void DrawKey()
{
    //samma princip som för poängen
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

//metod för den animerade bakgrunden
void DrawBackground()
{
    //ritar ut texturen
    Raylib.DrawTexture(mid, bX2, 0, Color.WHITE);
    Raylib.DrawTexture(mid, bX2 + mid.width, 0, Color.WHITE);
    Raylib.DrawTexture(close, bX, 0, Color.WHITE);
    Raylib.DrawTexture(close, bX + close.width, 0, Color.WHITE);

    //hur mycket bakgrunden ska ändras med för varje frame
    bX -= 1;
    bX2 -= 2;

    //detta gör så att den loopas perfekt och det ser ut som en lång bild
    if (bX <= -close.width) bX = 0;
    if (bX2 <= -mid.width) bX2 = 0;
}

//metod för att ladda in alla objekt från klasserna
void LoadObjects()
{
    player.Anim();
    enemy.EnemyAlive(player.enemyHp);

    player.Movement(Main.playerTexture);
    enemy.EnemyMovement();

    player.Collision(platform);

    player.Attack(MainEnemy.enemyRect);
    player.Damage();
}
//metod för att uppdatera mappen
void UpdateMap()
{
    platform = LevelDesign.Levels(point, key);
}