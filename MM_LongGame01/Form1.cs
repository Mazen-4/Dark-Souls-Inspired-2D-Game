using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using NAudio.Wave;

namespace MM_LongGame01
{
    public partial class Form1: Form
    {
        bool isPaused = false; // Flag to track pause state
        public AudioFileReader backgroundMusic;
        public WaveOutEvent backgroundMusicOutput;
        public void PlayBackgroundMusic(string audioFilePath)
        {
            // Load the background music audio file 
            backgroundMusic = new AudioFileReader(audioFilePath);
            // Create a wave output device to play the background music 
            backgroundMusicOutput = new WaveOutEvent();
            // Set the output device to use the background music as its input 
            backgroundMusicOutput.Init(backgroundMusic);
            // Start playing the background music on a loop 
            if (!isPaused)
                backgroundMusicOutput.Play();
            backgroundMusic.Volume = 0.9f; // Set the volume to 50% 
            backgroundMusicOutput.Volume = 0.9f; // Set the volume to 50% 
        }
        public AudioFileReader soundEffect;
        public WaveOutEvent soundEffectOutput;
        public void PlaySoundEffect(string audioFilePath)
        {
            isPaused = true;
            soundEffect = new AudioFileReader(audioFilePath);
            // Create a wave output device to play the sound effect 
            soundEffectOutput = new WaveOutEvent();
            // Set the output device to use the sound effect as its input 
            soundEffectOutput.Init(soundEffect);
            // Play the sound effect 
            soundEffectOutput.Play();
            soundEffect.Volume = 0.8f;
            soundEffectOutput.Volume = 0.8f;
        }


        string ost = "";
        Bitmap off;

        List<CAdvImgActor> lHero = new List<CAdvImgActor>();
        List<CAdvImgActor> lOrc = new List<CAdvImgActor>();
        List<CAdvImgActor> lSkeleton = new List<CAdvImgActor>();

        List<CAdvImgActor> Bullets = new List<CAdvImgActor>();
        List<CAdvImgActor> lspear = new List<CAdvImgActor>();
        List<CAdvImgActor> lLadder = new List<CAdvImgActor>();
        List<CAdvImgActor> lLaser = new List<CAdvImgActor>();
        List<CAdvImgActor> lchest = new List<CAdvImgActor>();
        List<CAdvImgActor> lcoin = new List<CAdvImgActor>();
        List<CAdvImgActor> lapple = new List<CAdvImgActor>();
        List<CAdvImgActor> ldoor = new List<CAdvImgActor>();

        List<CAdvImgActor> Tiles = new List<CAdvImgActor>();
        List <Bitmap> idleFrames = new List<Bitmap>();
        List<Bitmap> walkFrames = new List<Bitmap>();
        List<Bitmap> Run = new List<Bitmap>();
        List<Bitmap> HurtFrames = new List<Bitmap>();
        List<Bitmap> attackFrames1 = new List<Bitmap>();
        List<Bitmap> attackFrames2 = new List<Bitmap>();
        List<Bitmap> jumpFrames = new List<Bitmap>();
        List<Bitmap> DieFrames = new List<Bitmap>();
        List<Bitmap> ClimbFrames = new List<Bitmap>();
        List<Bitmap> ShieldFrames = new List<Bitmap>();
        List<Bitmap> chestFrames = new List<Bitmap>();
        List<Bitmap> coinFrames = new List<Bitmap>();

        List<Bitmap> orcWalkFrames = new List<Bitmap>();
        List<Bitmap> orcAttackFrames = new List<Bitmap>();
        List<Bitmap> orcHurtFrames = new List<Bitmap>();
        List<Bitmap> orcDieFrames = new List<Bitmap>();

        List<Bitmap> skeletonWalkFrames = new List<Bitmap>();
        List<Bitmap> skeletonAttackFrames = new List<Bitmap>();
        List<Bitmap> skeletonHurtFrames = new List<Bitmap>(); 
        List<Bitmap> skeletonThrowFrames = new List<Bitmap>();
        List<Bitmap> skeletonDieFrames = new List<Bitmap>();

        List<Bitmap> HeroBullets = new List<Bitmap>();
        List<CAdvImgActor> lCannon = new List<CAdvImgActor>();
        List<CAdvImgActor> lsensor = new List<CAdvImgActor>();
        //List<CAdvImgActor> lcoins = new List<CAdvImgActor>();

        int HeroDir = 0; // 1: right, -1: left
        Timer tt = new Timer();
        int ctTick = 0;
        Bitmap iarena = new Bitmap("pics/Cartoon_Medieval_Armory_Level_Set_Background - Layer 00.png");
        Bitmap iarena2 = new Bitmap("pics/Cartoon_Medieval_Armory_Level_Set_Background - Layer 01.png");
        Bitmap clone = new Bitmap("pics/Cartoon_Medieval_Armory_Level_Set_Background - Layer 00.png");
        Bitmap clone2 = new Bitmap("pics/Cartoon_Medieval_Armory_Level_Set_Background - Layer 01.png");
        //Bitmap backG = new Bitmap("pics/magic-cliffs-PREVIEWx1.png");

        Bitmap groundTile = new Bitmap("pics/grass tileset/01_grass tileset.png");
        Bitmap groundTile2 = new Bitmap("pics/grass tileset/08_grass tileset.png");
        Bitmap ladder = new Bitmap("pics/Ladder.png");
        Bitmap Laser = new Bitmap("pics/Laser.png");
        Bitmap cannon = new Bitmap("pics/Cannon.png");
        Bitmap sensor = new Bitmap("pics/sensor.png");

        Bitmap chestClosed = new Bitmap("pics/collectableItems/Chest_01_Locked.png");
        Bitmap chestOpened = new Bitmap("pics/collectableItems/Chest_01_Unlocked.png");
        Bitmap key = new Bitmap("pics/collectableItems/Key_01.png");
        Bitmap life = new Bitmap("pics/collectableItems/life.png");
        Bitmap Die = new Bitmap("pics/Die.jpg");
        Bitmap spear = new Bitmap("pics/Spear.png");
        Bitmap apple = new Bitmap("pics/collectableItems/Apple.png");
        Bitmap door = new Bitmap("pics/Door.png");
        Bitmap win = new Bitmap("pics/win.jpg");

        const int HeroDrawWidth = 2; // Since you're scaling by 2 when drawing
        const int OrcDrawScale = 5;  // Since you're dividing by 5
        const int SkeletonDrawScale = 5; // Since you're dividing by 5

        const int HeroSpeed = 20;
        const int HeroRun = 40;
        int facing = 1; // 1: right, -1: left
        int scrollX = 0;

        bool rightHeld = false;
        bool leftHeld = false;
        bool shiftHeld = false;
        bool run = false;
        bool isAttacking = false;
        bool OrcHurting = false; 
        bool SkeletonHurting = false; 
        bool isJumping = false;
        bool prv = false; // for animation flip x
        bool isDead = false;
        bool GameOver = false;
        bool onLadderPlatform;
        bool KeySpawn;
        bool checkPickup;
        bool isLanded = true;
        bool onElevator = false;
        bool isWin = false;

        int patrolLeft = 1200;
        int patrolRight = 1500;
        int keyTmpx = 0;

        int jumpVelocity = 0;
        const int JumpStrength = -50; // negative for upward movement
        const int Gravity = 25;
        int groundY; // will be set in Form1_Load or Drawscene
        int jumpDX = 0; // horizontal speed during jump
        int animTick = 0; // Counts ticks for animation frame change
        const int animDelay = 2; // Change frame every 2 ticks

        int heroWorldX ;
        int heroW ; // Hero is drawn scaled by 2
        int orcWorldX ;
        int orcW ; // Orc is drawn scaled by 1/5
        int skeletonWorldX;
        int skeletonW; // Orc is drawn scaled by 1/5

        int platformLeft = 0;
        int platformRight = 0;
        int platform2Left = 0;
        int platform2Right = 0;
        int elevatorLeft = 0;
        int elevatorRight = 0;
        int groundY1;

        int sensorY;
        int coinCT;

        int HeroHealth = 5;
        const int herobullets = 5;

        int elevatorTileStartIndex = 0;
        CAdvImgActor Elevator = new CAdvImgActor();

        public Form1()
        {
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
            this.WindowState = FormWindowState.Maximized;
            tt.Tick += Tt_Tick;
            tt.Start();

            this.KeyPreview = true;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isAttacking || isJumping)
                return;


            if (lHero[0].stat.state != "idle")
            {
                lHero[0].stat.state = "idle";
                lHero[0].nFr = idleFrames.Count;
                lHero[0].iFr = 0;
            }

            rightHeld = false;
            leftHeld = false;
        }

        void fireBullet()
        {            
            for (int i=0; i < Bullets.Count; i++)
            {
                if (!Bullets[i].isFire) // Find the first inactive bullet
                {
                    CAdvImgActor bullet = Bullets[i];
                    bullet.isFire = true;

                    if (bullet.stat == null)
                        bullet.stat = new states();

                    if (facing == 1)
                    {
                        bullet.stat.state = "right";
                        bullet.x = lHero[0].x + scrollX + lHero[0].img.Width;
                        if (bullet.stat.state == "right" && bullet.isFlipped)
                        {
                            Bitmap flipped = (Bitmap)bullet.img.Clone();
                            flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            bullet.img = flipped;
                            bullet.isFlipped = false;
                        }
                    }
                    else if (facing == -1)
                    {
                        bullet.stat.state = "left";
                        bullet.x = lHero[0].x + scrollX;
                        if (bullet.stat.state == "left" && !bullet.isFlipped)
                        {
                            Bitmap flipped = (Bitmap)bullet.img.Clone();
                            flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            bullet.img = flipped;
                            bullet.isFlipped = true;
                        }
                    }

                    bullet.y = lHero[0].y + (lHero[0].img.Height * HeroDrawWidth) / 2; // Reset vertical position
                    break;
                }
            }
         
        }

        void throwSpear()
        {
            for (int i = 0; i < lspear.Count; i++)
            {
                if (!lspear[i].isFire) // Find the first inactive spear
                {
                    CAdvImgActor spear = lspear[i];
                    spear.isFire = true;

                    spear.x = lSkeleton[0].x ;
                    spear.y = lSkeleton[0].y + (lSkeleton[0].img.Height / 5) / 2; 

                    if (lSkeleton[0].dir == 1)
                        spear.stat.state = "right";
                    else if (lSkeleton[0].dir == -1)
                        spear.stat.state = "left";

                    if (lSkeleton[0].dir == 1)
                    {
                        spear.stat.state = "right";
                        //spear.x = lSkeleton[0].x + lSkeleton[0].img.Width/5;
                        if (!spear.isFlipped)
                        {
                            Bitmap flipped = (Bitmap)spear.img.Clone();
                            flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            spear.img = flipped;
                            spear.isFlipped = true;
                        }
                    }
                    else if (lSkeleton[0].dir == -1)
                    {
                        spear.stat.state = "left";
                        if (spear.isFlipped)
                        {
                            Bitmap flipped = (Bitmap)spear.img.Clone();
                            flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            spear.img = flipped;
                            spear.isFlipped = false;
                        }
                    }

                    break;
                }
            }

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (isAttacking) return;

            rightHeld = false;
            leftHeld = false;

            if (e.Button == MouseButtons.Left)
            {
                lHero[0].stat.state = "attack1";
                lHero[0].nFr = attackFrames1.Count;
                lHero[0].iFr = 0;
                isAttacking = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                lHero[0].stat.state = "attack2";
                lHero[0].nFr = attackFrames2.Count;
                lHero[0].iFr = 0;
                isAttacking = true;
                fireBullet(); 
            }
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            int groundY1 = this.ClientSize.Height - 220;
            // prevent sink in ground
            if (lHero[0].y + lHero[0].img.Height * 2 > groundY1 && !isJumping)
            {
                lHero[0].y = groundY1 - lHero[0].img.Height * 2;
            }
            else if (lHero[0].y + lHero[0].img.Height * 2 < lLadder[0].y && !isJumping)
            {
                lHero[0].y = lLadder[0].y - lHero[0].img.Height * 2;
            }

            // Elevator Y fix
            if (lHero[0].y + lHero[0].img.Height * 2 > lLadder[0].y && !onElevator)
            {
                Elevator.y = groundY1 - 40; 
            }
            else if (lHero[0].y + lHero[0].img.Height * 2 <= lLadder[0].y && !onElevator)
            {
                Elevator.y = lLadder[0].y;
            }

            lHero[0].CurrHlth = HeroHealth;

            // Collect coins
            for (int i = lcoin.Count - 1; i >= 0; i--)
            {
                CAdvImgActor coin = lcoin[i];
                if (lHero[0].x + lHero[0].img.Width + scrollX > lcoin[i].x && lHero[0].x + scrollX < lcoin[i].x + lcoin[i].img.Width/3
                    && lHero[0].y < lcoin[i].y && lHero[0].y + lHero[0].img.Height*2 > lcoin[i].y + lcoin[i].img.Height/3)
                {
                    lcoin.RemoveAt(i);
                    coinCT++;
                }
            }

            // collect apple      
            for (int i = lapple.Count - 1; i >= 0; i--)
            {
                CAdvImgActor apple = lapple[i];
                if (lHero[0].x + lHero[0].img.Width + scrollX > lapple[i].x && lHero[0].x < lapple[i].x + lapple[i].img.Width / 3
                    && lHero[0].y < lapple[i].y && lHero[0].y + lHero[0].img.Height * 2 > lapple[i].y + lapple[i].img.Height / 3)
                {
                    lapple.RemoveAt(i);

                    HeroHealth++;
                }
            }

            // hero should not move while attacking
            if (isAttacking || lHero[0].stat.state == "attack1")
            {
                rightHeld = false;
                leftHeld = false;
            }

            // ---- Coins Frame Update ----
            for (int i = 0; i < lcoin.Count; i++)
            {
                CAdvImgActor coin = lcoin[i];
                coin.iFr++;
                if (coin.iFr >= coin.nFr)
                {
                    coin.iFr = 0;
                }
            }

            // --- HERO JUMPING ---
            if (isJumping)
            {
                isLanded = false; 
                lHero[0].y += jumpVelocity;
                lHero[0].x += jumpDX * 2;
                jumpVelocity += Gravity / 2;

                // Advance jump animation frame every animDelay ticks
                animTick++;
                if (animTick >= animDelay)
                {
                    if (lHero[0].iFr < lHero[0].nFr - 1 && lHero[0].stat.state != "climb")
                        lHero[0].iFr++;
                    animTick = 0;
                }

                // Check for landing - sort tiles by Y position (topmost first)
                List<CAdvImgActor> sortedTiles = new List<CAdvImgActor>(Tiles);
                // Bubble sort by Y position
                for (int i = 0; i < sortedTiles.Count - 1; i++)
                {
                    for (int j = 0; j < sortedTiles.Count - 1 - i; j++)
                    {
                        if (sortedTiles[j].y > sortedTiles[j + 1].y)
                        {
                            // Swap
                            CAdvImgActor temp = sortedTiles[j];
                            sortedTiles[j] = sortedTiles[j + 1];
                            sortedTiles[j + 1] = temp;
                        }
                    }
                }

                // Check for landing
                for (int i = 0; i < sortedTiles.Count; i++)
                {
                    // Calculate hero's world position
                    int heroWorldX = lHero[0].x + scrollX;
                    int heroRightEdge = heroWorldX + (lHero[0].img.Width * 2);
                    int heroBottom = lHero[0].y + (lHero[0].img.Height * 2);

                    // Use tile's world position (not screen position)
                    int tileWorldX = sortedTiles[i].x; // This should now be world position
                    int tileRight = tileWorldX + sortedTiles[i].img.Width;

                    // Check if hero overlaps horizontally with tile
                    bool horizontalOverlap = (heroRightEdge > sortedTiles[i].x && heroWorldX < sortedTiles[i].x + sortedTiles[i].img.Width);
                    bool VerticalOverlap = (lHero[0].y < sortedTiles[i].y && heroBottom >= sortedTiles[i].y);

                    // Check if hero is landing on top of tile (falling down onto it)
                    bool landingOnTile = (heroBottom >= sortedTiles[i].y && lHero[0].y < sortedTiles[i].y);

                    if (landingOnTile && isJumping && jumpVelocity >= 0 && VerticalOverlap)
                    {
                        isLanded = true;
                        lHero[0].y = sortedTiles[i].y - (lHero[0].img.Height * 2); // Set hero on top of tile
                        isJumping = false;
                        jumpVelocity = 0;
                        jumpDX = 0;

                        if (HeroDir == 0)
                        {
                            lHero[0].stat.state = "idle";
                            lHero[0].nFr = idleFrames.Count;
                            lHero[0].iFr = 0;
                        }
                        else if (run)
                        {
                            lHero[0].stat.state = "run";
                            lHero[0].nFr = Run.Count;
                            lHero[0].iFr = 0;
                        }
                        else
                        {
                            lHero[0].stat.state = "walk";
                            lHero[0].nFr = walkFrames.Count;
                            lHero[0].iFr = 0;
                        }
                        animTick = 0;
                        break;
                    }
                    
                }
            }
            else
            {
                isLanded = true; // Reset landing state when not jumping
                // --- HERO ANIMATION ---
                if (lHero[0].stat.state != "")
                {
                    animTick++;
                    if (animTick >= animDelay)
                    {
                        if (lHero[0].iFr + 1 < lHero[0].nFr)
                            lHero[0].iFr++;
                        else if (lHero[0].stat.state == "Dead")
                        {
                            GameOver = true; // Set game over if dead animation completes
                        }
                        else if (isDead && GameOver == false)
                        {
                            lHero[0].stat.state = "Dead";
                        }
                        else
                        {
                            lHero[0].iFr = 0;
                            if (lHero[0].stat.state == "attack1" || lHero[0].stat.state == "attack2")
                            {
                                isAttacking = false;
                                OrcHurting = false;       //allows multiple attacks
                                SkeletonHurting = false; // allows multiple attacks
                                lHero[0].stat.state = "idle"; 
                                lHero[0].iFr = 0;
                                lHero[0].nFr = idleFrames.Count;
                            }
                        }
                        animTick = 0;
                    }
                }
                
                if (lHero[0].stat.state != "Dead" && lHero[0].stat.state != "Hurt" &&
                    lHero[0].stat.state != "Shield" && lHero[0].stat.state != "attack1")
                {
                    // --- HERO MOVEMENT ---
                    if (HeroDir == 1 && run && rightHeld)
                    {
                        for (int i = 0; i < lHero.Count; i++)
                            lHero[i].x += HeroRun;
                    }
                    else if (HeroDir == 1 && rightHeld)
                    {
                        for (int i = 0; i < lHero.Count; i++)
                            lHero[i].x += HeroSpeed;
                    }
                    else if (HeroDir == -1 && run && leftHeld)
                    {
                        for (int i = 0; i < lHero.Count; i++)
                            lHero[i].x -= HeroRun;
                    }
                    else if (HeroDir == -1 && leftHeld)
                    {
                        for (int i = 0; i < lHero.Count; i++)
                            lHero[i].x -= HeroSpeed;
                    }
                }
            }
            
            // --- Gravity ---
            /*for (int i = 0; i < Tiles.Count; i++)
            {
                // Calculate hero's dimensions and position
                int heroLeft = lHero[0].x;
                int heroRight = lHero[0].x + (lHero[0].img.Width * 2); // Assuming you scale by 2
                int heroBottom = lHero[0].y + (lHero[0].img.Height * 2);

                // Calculate tile dimensions
                int tileLeft = Tiles[i].x;
                int tileRight = Tiles[i].x + Tiles[i].img.Width;
                int tileTop = Tiles[i].y;

                // Check if hero overlaps horizontally with tile
                bool horizontalOverlap = (heroRight > tileLeft && heroLeft < tileRight);

                // Check if hero is on top of or very close to tile (allowing small tolerance)
                bool onTopOfTile = (heroBottom >= tileTop && heroBottom <= tileTop + 10);

                if (horizontalOverlap && onTopOfTile)
                {
                    isLanded = true;
                    break;
                }
            }
            if (!isLanded)
            {
                isLanded = false; // Hero is not on any tile
            }*/

            bool onAir = ((lHero[0].x + lHero[0].img.Width > platform2Right && lHero[0].x + lHero[0].img.Width < elevatorLeft) ||
                (lHero[0].x + scrollX + lHero[0].img.Width > platformRight && lHero[0].x + lHero[0].img.Width + scrollX < platform2Left) || (lHero[0].x + scrollX < platformLeft) ||
                (lHero[0].x + scrollX > platform2Right));
            if (onAir)
                isLanded = false;

            onElevator = (lHero[0].x + lHero[0].img.Width + scrollX >= elevatorLeft && lHero[0].x + scrollX <= elevatorRight);

            heroWorldX = lHero[0].x + scrollX;
            onLadderPlatform = (heroWorldX + lHero[0].img.Width * 2 >= lLadder[0].x && heroWorldX + lHero[0].img.Width <= 2450) && lHero[0].y < lLadder[0].y;

            if (lHero[0].y < groundY && !isJumping && lHero[0].stat.state != "climb" && !onLadderPlatform && !isLanded && onAir && !onElevator)
            {
                lHero[0].y += Gravity; // Apply gravity
            }

            // --- HERO BOUNDARIES ---
            if (lHero[0].x < 10)
            {
                lHero[0].x = 10;
            }
            else if (heroWorldX + (lHero[0].img.Width * HeroDrawWidth) >= (this.ClientSize.Width * 3) - 15)
            {
                lHero[0].x = this.ClientSize.Width - (lHero[0].img.Width * HeroDrawWidth) - 10;
            }

            // --- BACKGROUND SCROLLING (always runs) ---
            int scrollThresholdRight = this.ClientSize.Width * 2 / 3;  
            int scrollThresholdLeft = this.ClientSize.Width / 3;

            if (lHero[0].x >= scrollThresholdRight && facing == 1 && rightHeld && scrollX < 4075)
            {
                // Smooth scrolling speed
                int scrollSpeed;
                if (run)
                {
                    scrollSpeed = HeroRun;
                }
                else
                {
                    scrollSpeed = HeroSpeed;
                }
                lHero[0].x -= scrollSpeed;
                scrollX += scrollSpeed;
            }

            if (lHero[0].x + lHero[0].img.Width >= this.ClientSize.Width / 2 && facing == 1 && rightHeld && scrollX < 4075)
            {
                lHero[0].x -= HeroSpeed;
                scrollX += HeroSpeed;
            }
            else if (lHero[0].x <= this.ClientSize.Width / 2 && scrollX > HeroSpeed && facing == -1 && leftHeld)
            {
                lHero[0].x += HeroSpeed;
                scrollX -= HeroSpeed;
            }

            // --- HERO STATE MANAGEMENT ---
            if (!leftHeld && !rightHeld && !isJumping && !isAttacking && lHero[0].stat.state != "idle" && lHero[0].stat.state != "Dead"
                && lHero[0].iFr >= lHero[0].nFr - 1 && lHero[0].stat.state != "attack1" && lHero[0].stat.state != "attack2" && lHero[0].stat.state != "climb")
            {
                lHero[0].stat.state = "idle";
                lHero[0].nFr = idleFrames.Count;
                lHero[0].iFr = 0;
            }

            // --- Orc STATE MANAGEMENT ---
            if (lOrc.Count > 0)
            {
                for (int i = 0; i < lOrc.Count; i++)
                {
                    if (lOrc[i].stat.state != "walk" && lOrc[i].iFr >= lOrc[i].nFr - 1 && lOrc[i].stat.state != "Die")
                    {
                        lOrc[i].stat.state = "walk";
                        lOrc[i].nFr = idleFrames.Count;
                        lOrc[i].iFr = 0;
                    }
                }
                
            }
            
            // --- ORC MOVEMENT & ATTACK LOGIC ---
            for (int i = 0; i < lOrc.Count; i++)
            {
                CAdvImgActor orc = lOrc[i];
                int orcAnimTick = 0;

                patrolLeft = 1200;
                patrolRight = 1500;

                heroWorldX = lHero[0].x + scrollX;
                heroW = lHero[0].img.Width * 2; 
                orcWorldX = orc.x;
                orcW = orc.img.Width / 5; 

                bool heroInVision = (heroWorldX + heroW > patrolLeft && heroWorldX < patrolRight);

                int orcLeft = orcWorldX;
                int orcRight = orcWorldX + orcW;
                int heroLeft = heroWorldX;
                int heroRight = heroWorldX + heroW;

                int distToHero;
                if (orcRight < heroLeft)
                    distToHero = heroLeft - orcRight;
                else if (heroRight < orcLeft)
                    distToHero = orcLeft - heroRight;
                else
                    distToHero = 0; // Overlapping

                //Animation frame update
                if (orc.CurrHlth > 0 || orc.stat.state == "Die")
                    orc.iFr++;
                else
                {
                    orc.stat.state = "Die";
                    orc.nFr = orcDieFrames.Count;
                    orc.iFr = 0;
                }

                // Handle frame
                if (orc.iFr >= orc.nFr)
                {
                    if (orc.stat.state == "Die")
                    {
                       
                        lOrc.RemoveAt(i);
                        i--;
                        continue; 
                    }
                    else
                    {
                        orc.iFr = 0;

                        // USE WORLD COORDINATES FOR BOTH
                        int heroWorldX = lHero[0].x + scrollX;
                        int heroW = lHero[0].img.Width * HeroDrawWidth;
                        int orcWorldX = orc.x;
                        int orcW = orc.img.Width / OrcDrawScale;

                        orcLeft = orcWorldX;
                        orcRight = orcWorldX + orcW;
                        heroLeft = heroWorldX;
                        heroRight = heroWorldX + heroW;
                        //int distToHero;

                        if (orcRight < heroLeft)
                            distToHero = heroLeft - orcRight;
                        else if (heroRight < orcLeft)
                            distToHero = orcLeft - heroRight;
                        else
                            distToHero = 0; // Overlapping

                        if (orc.stat.state == "attack" && distToHero >= 45)
                        {
                            orc.stat.state = "walk";
                            orc.nFr = orcWalkFrames.Count;
                            orc.iFr = 0;
                        }

                    }
                }
                if (orc.iFr >= orc.nFr - 1)
                {
                    if (orc.stat.state == "attack" && distToHero < 45 &&
                        lHero[0].stat.state != "Shield" && lHero[0].stat.state != "Hurt" &&
                        !isAttacking && lHero[0].stat.state != "attack1" && lHero[0].stat.state != "attack2")
                    {
                        lHero[0].stat.state = "Hurt";
                        lHero[0].nFr = HurtFrames.Count;
                        lHero[0].iFr = 0;
                        HeroHealth--;
                        if (HeroHealth == 0)
                        {
                            isDead = true;
                            lHero[0].stat.state = "Dead";
                            lHero[0].nFr = DieFrames.Count;
                            lHero[0].iFr = 0;
                        }
                    }
                }

                if (orc.stat.state != "Die" && orc.stat.state != "Hurt")
                {
                    // --- ORC BEHAVIOR, VISION LOGIC ---
                    if (heroInVision || distToHero <= 10)
                    {
                        // If close enough to attack (using world coordinates)
                        if (distToHero < 10)
                        {
                            // Face the hero
                            if (orcWorldX > heroWorldX)
                                orc.dir = -1; // Face left
                            else if (orcWorldX < heroWorldX)
                                orc.dir = 1; // Face right

                            // Start attack animation if not attacking
                            if (orc.stat.state != "attack" && orc.stat.state != "Hurt" && orc.stat.state != "Die"
                                && lHero[0].stat.state != "attack1" && lHero[0].stat.state != "attack2")
                            {
                                orc.stat.state = "attack";
                                orc.nFr = orcAttackFrames.Count;
                                orc.iFr = 0;
                            }
                        }
                        else
                        {
                            // Approach hero
                            if (orc.stat.state != "walk")
                            {
                                orc.stat.state = "walk";
                                orc.nFr = orcWalkFrames.Count;
                                orc.iFr = 0;
                            }

                            if (orcWorldX < heroWorldX)
                            {
                                orc.dir = 1; 
                                             
                                if (heroWorldX - orcWorldX > 10)
                                    orc.x += 5;
                                else
                                    orc.x = heroWorldX - orcW - 10; 
                            }
                            else if (orcWorldX > heroWorldX)
                            {
                                orc.dir = -1; 
                                if (orcWorldX - heroWorldX > 10)
                                    orc.x -= 5;
                                else
                                    orc.x = heroWorldX + heroW + 10; 
                            }
                        }
                    }
                    else
                    {
                        // Hero not in vision 
                        if (orc.stat.state != "walk")
                        {
                            orc.stat.state = "walk";
                            orc.nFr = orcWalkFrames.Count;
                            orc.iFr = 0;
                        }

                        // Patrol 
                        if (orc.x + orcW >= patrolRight)
                            orc.dir = -1;
                        else if (orc.x <= patrolLeft)
                            orc.dir = 1;

                        orc.x += 5 * orc.dir;
                    }

                }




            }

            if (lHero[0].stat.state == "attack1" || lHero[0].stat.state == "attack2")
            {
                if (!OrcHurting && lHero[0].iFr == lHero[0].nFr - 1)
                {
                    for (int i = lOrc.Count - 1; i >= 0; i--) // Loop backwards 
                    {
                        CAdvImgActor orc = lOrc[i];

                        if (orc.stat.state == "Hurt" || orc.stat.state == "Die")
                            continue;

                        // USE WORLD COORDINATES FOR BOTH
                        int heroWorldX = lHero[0].x + scrollX;
                        int heroW = lHero[0].img.Width * HeroDrawWidth;
                        int orcWorldX = orc.x;
                        int orcW = orc.img.Width / OrcDrawScale;

                        int orcLeft = orcWorldX;
                        int orcRight = orcWorldX + orcW;
                        int heroLeft = heroWorldX;
                        int heroRight = heroWorldX + heroW;
                        int distToHero;

                        if (orcRight < heroLeft)
                            distToHero = heroLeft - orcRight;
                        else if (heroRight < orcLeft)
                            distToHero = orcLeft - heroRight;
                        else
                            distToHero = 0; // Overlapping

                        if (distToHero <= 45)
                        {
                            // Deal damage to the orc
                            OrcHurting = true;
                            orc.CurrHlth--;
                            orc.iFr = 0; 

                            if (orc.CurrHlth <= 0)
                            {
                                orc.stat.state = "Die";
                                orc.nFr = orcDieFrames.Count;
                            }
                            else
                            {
                                orc.stat.state = "Hurt";
                                orc.nFr = orcHurtFrames.Count;
                            }
                            break; // Only damage one orc per attack
                        }
                    }
                }
            }

            // --- Skeleton ---
            if (true)
            {
                // --- Skeleton STATE MANAGEMENT ---
                if (lSkeleton.Count > 0)
                {
                    if (lSkeleton[0].stat.state != "walk" && lSkeleton[0].iFr >= lSkeleton[0].nFr - 1 && lSkeleton[0].stat.state != "Die")
                    {
                        lSkeleton[0].stat.state = "walk";
                        lSkeleton[0].nFr = idleFrames.Count;
                        lSkeleton[0].iFr = 0;
                    }
                }

                // --- Skeleton MOVEMENT & ATTACK LOGIC ---
                for (int i = 0; i < lSkeleton.Count; i++)
                {
                    CAdvImgActor skeleton = lSkeleton[i];
                    //int skeletonAnimTick = 0;

                    // Define patrol boundaries
                    patrolLeft = 4500;
                    patrolRight = 5200;

                    // Get world positions (accounting for scrolling)
                    heroWorldX = lHero[0].x + scrollX;
                    heroW = lHero[0].img.Width * 2; // Hero is drawn scaled by 2
                    skeletonWorldX = skeleton.x;
                    skeletonW = skeleton.img.Width / 5; // Orc is drawn scaled by 1/5

                    // Vision check: hero overlaps with patrol area in world coordinates
                    bool heroInVision = (heroWorldX + heroW > patrolLeft && heroWorldX < patrolRight);

                    // Calculate distance between bounding boxes
                    int skeletonLeft = skeletonWorldX;
                    int skeletonRight = skeletonWorldX + skeletonW;
                    int heroLeft = heroWorldX;
                    int heroRight = heroWorldX + heroW;

                    int distToHero;
                    if (skeletonRight < heroLeft)
                        distToHero = heroLeft - skeletonRight;
                    else if (heroRight < skeletonLeft)
                        distToHero = skeletonLeft - heroRight;
                    else
                        distToHero = 0; // Overlapping

                    //Animation frame update
                    if (skeleton.CurrHlth > 0 || skeleton.stat.state == "Die")
                        skeleton.iFr++;
                    else
                    {
                        skeleton.stat.state = "Die";
                        skeleton.nFr = skeletonDieFrames.Count;
                        skeleton.iFr = 0;
                    }

                    // Handle frame wrapping and state transitions
                    if (skeleton.iFr >= skeleton.nFr)
                    {
                        if (skeleton.stat.state == "Die")
                        {
                            // Death animation completed, remove the skeleton
                            lSkeleton.RemoveAt(i);
                            i--; // Adjust index after removal
                            continue; // Skip the rest of the processing for this skeleton
                        }
                        else
                        {
                            skeleton.iFr = 0;

                            // USE WORLD COORDINATES FOR BOTH
                            int heroWorldX = lHero[0].x + scrollX;
                            int heroW = lHero[0].img.Width * HeroDrawWidth;
                            int skeletonWorldX = skeleton.x;
                            int skeletonW = skeleton.img.Width / OrcDrawScale;

                            skeletonLeft = skeletonWorldX;
                            skeletonRight = skeletonWorldX + skeletonW;
                            heroLeft = heroWorldX;
                            heroRight = heroWorldX + heroW;
                            //int distToHero;

                            if (skeletonRight < heroLeft)
                                distToHero = heroLeft - skeletonRight;
                            else if (heroRight < skeletonLeft)
                                distToHero = skeletonLeft - heroRight;
                            else
                                distToHero = 0; // Overlapping

                            // After attack animation completes, check if we should continue attacking
                            if (skeleton.stat.state == "attack" && distToHero >= 45)
                            {
                                skeleton.stat.state = "walk";
                                skeleton.nFr = skeletonWalkFrames.Count;
                                skeleton.iFr = 0;
                            }

                        }
                    }
                    if (skeleton.iFr >= skeleton.nFr - 1)
                    {
                        if (skeleton.stat.state == "attack" && distToHero < 65 &&
                            lHero[0].stat.state != "Shield" && lHero[0].stat.state != "Hurt" &&
                            !isAttacking && lHero[0].stat.state != "attack1" && lHero[0].stat.state != "attack2")
                        {
                            lHero[0].stat.state = "Hurt";
                            lHero[0].nFr = HurtFrames.Count;
                            lHero[0].iFr = 0;
                            HeroHealth--;
                            if (HeroHealth == 0)
                            {
                                isDead = true;
                                lHero[0].stat.state = "Dead";
                                lHero[0].nFr = DieFrames.Count;
                                lHero[0].iFr = 0;
                            }
                        }
                    }

                    if (skeleton.stat.state != "Die" && skeleton.stat.state != "Hurt")
                    {
                        // --- skeleton BEHAVIOR, VISION LOGIC ---
                        if (heroInVision || distToHero <= 25)
                        {
                            // If close enough to attack (using world coordinates)
                            if (distToHero < 25)
                            {
                                // Face the hero
                                if (skeletonWorldX > heroWorldX)
                                    skeleton.dir = -1; // Face left
                                else if (skeletonWorldX < heroWorldX)
                                    skeleton.dir = 1; // Face right
                                
                                // Start attack animation if not already attacking
                                if (skeleton.stat.state != "Throw" && skeleton.stat.state != "Hurt" && skeleton.stat.state != "Die"
                                    && lHero[0].stat.state != "attack1" && lHero[0].stat.state != "attack2")
                                {
                                    throwSpear(); // Fire spear
                                    skeleton.stat.state = "Throw";
                                    skeleton.nFr = skeletonThrowFrames.Count;
                                    skeleton.iFr = 0;
                                }
                            }
                            else
                            {
                                // Approach hero
                                if (skeleton.stat.state != "walk")
                                {
                                    skeleton.stat.state = "walk";
                                    skeleton.nFr = skeletonWalkFrames.Count;
                                    skeleton.iFr = 0;
                                }

                                // Move towards hero (using world coordinates for direction)
                                if (skeletonWorldX < heroWorldX)
                                {
                                    skeleton.dir = 1; // Face right
                                                 // Move only if we won't overshoot
                                    if (heroWorldX - skeletonWorldX > 10)
                                        skeleton.x += 5;
                                    else
                                        skeleton.x = heroWorldX - skeletonW - 10; // Stop just before hero
                                }
                                else if (skeletonWorldX > heroWorldX)
                                {
                                    skeleton.dir = -1; // Face left
                                    if (skeletonWorldX - heroWorldX > 10)
                                        skeleton.x -= 5;
                                    else
                                        skeleton.x = heroWorldX + heroW + 10; // Stop just after hero
                                }
                            }
                        }
                        else
                        {
                            // Hero not in vision - patrol behavior
                            if (skeleton.stat.state != "walk")
                            {
                                skeleton.stat.state = "walk";
                                skeleton.nFr = skeletonWalkFrames.Count;
                                skeleton.iFr = 0;
                            }

                            // Patrol between boundaries
                            if (skeleton.x + skeletonW >= patrolRight)
                                skeleton.dir = -1;
                            else if (skeleton.x <= patrolLeft)
                                skeleton.dir = 1;

                            skeleton.x += 5 * skeleton.dir;
                        }

                    }

                }

                if (lHero[0].stat.state == "attack1" || lHero[0].stat.state == "attack2")
                {
                    // Only deal damage on the last frame of attack animation and if not already hurting
                    if (!SkeletonHurting && lHero[0].iFr == lHero[0].nFr - 1)
                    {
                        for (int i = lSkeleton.Count - 1; i >= 0; i--) // Loop backwards to handle removal safely
                        {
                            CAdvImgActor skeleton = lSkeleton[i];

                            // Skip skeletons that are already in hurt or die state
                            if (skeleton.stat.state == "Hurt" || skeleton.stat.state == "Die")
                                continue;

                            // USE WORLD COORDINATES FOR BOTH
                            int heroWorldX = lHero[0].x + scrollX;
                            int heroW = lHero[0].img.Width * HeroDrawWidth;
                            int skeletonWorldX = skeleton.x;
                            int skeletonW = skeleton.img.Width / SkeletonDrawScale;

                            int skeletonLeft = skeletonWorldX;
                            int skeletonRight = skeletonWorldX + skeletonW;
                            int heroLeft = heroWorldX;
                            int heroRight = heroWorldX + heroW;
                            int distToHero;

                            if (skeletonRight < heroLeft)
                                distToHero = heroLeft - skeletonRight;
                            else if (heroRight < skeletonLeft)
                                distToHero = skeletonLeft - heroRight;
                            else
                                distToHero = 0; // Overlapping

                            // Check if the skeleton is within the attack range of the hero
                            if (distToHero <= 45)
                            {
                                // Deal damage to the skeleton
                                SkeletonHurting = true;
                                skeleton.CurrHlth--;
                                skeleton.iFr = 0; // Reset animation frame

                                if (skeleton.CurrHlth <= 0)
                                {
                                    skeleton.stat.state = "Die";
                                    skeleton.nFr = skeletonDieFrames.Count;
                                     
                                }
                                else
                                {
                                    skeleton.stat.state = "Hurt";
                                    skeleton.nFr = orcHurtFrames.Count;
                                }
                                break; // Only damage one orc per attack
                            }
                        }
                    }
                }
            }

            // --- BULLET LOGIC ---
            for (int i = Bullets.Count - 1; i >= 0; i--) // backwards 
            {
                CAdvImgActor bullet = Bullets[i];
                
                
                if (bullet.isFire)
                {
                    if (bullet.stat.state == "right")
                        bullet.x += HeroSpeed;
                    else if (bullet.stat.state == "left")
                        bullet.x -= HeroSpeed;

                    // Check if the bullet is out of bounds
                    if (bullet.x < 0 || bullet.x > this.ClientSize.Width * 3)
                    {
                        bullet.isFire = false; 
                        continue; // Skip to next iteration
                    }
                    // Check for collision with skeletons
                    for (int j = lOrc.Count - 1; j >= 0; j--)
                    {
                        CAdvImgActor orc = lOrc[j];

                        // Skip orcs that are already in hurt or die state
                        if (orc.stat.state == "Hurt" || orc.stat.state == "Die")
                            continue;

                        //int bulletWorldX = bullet.x; // Assuming bullet.x is already in world coordinates
                        int orcWorldX = orc.x;
                        int orcW = orc.img.Width / OrcDrawScale;
                        int orcLeft = orcWorldX;
                        int orcRight = orcWorldX + orcW;

                        if (bullet.x + bullet.img.Width > orcLeft && bullet.x < orcRight) // Check for collision
                        {
                            OrcHurting = true;
                            orc.CurrHlth--;
                            bullet.isFire = false; // Deactivate the bullet
                            orc.iFr = 0; // Reset animation frame
                            if (orc.CurrHlth <= 0)
                            {
                                orc.stat.state = "Die";                                
                                orc.nFr = orcDieFrames.Count;
                            }
                            else
                            {
                                orc.stat.state = "Hurt";
                                orc.nFr = orcHurtFrames.Count;
                            }
                            break; // Only damage one orc per bullet
                        }
                    }
                    for (int j = lSkeleton.Count - 1; j >= 0; j--)
                    {
                        CAdvImgActor skeleton = lSkeleton[j];

                        // Skip skeletons that are already in hurt or die state
                        if (skeleton.stat.state == "Hurt" || skeleton.stat.state == "Die")
                            continue;

                        //int bulletWorldX = bullet.x; // Assuming bullet.x is already in world coordinates
                        int skeletonWorldX = skeleton.x;
                        int skeletonW = skeleton.img.Width / SkeletonDrawScale;
                        int skeletonLeft = skeletonWorldX;
                        int skeletonRight = skeletonWorldX + skeletonW;

                        if (bullet.x + bullet.img.Width > skeletonLeft && bullet.x < skeletonRight) // Check for collision
                        {
                            SkeletonHurting = true;
                            skeleton.CurrHlth--;
                            bullet.isFire = false; // Deactivate the bullet
                            skeleton.iFr = 0; // Reset animation frame
                            if (skeleton.CurrHlth <= 0)
                            {
                                skeleton.stat.state = "Die";
                                skeleton.nFr = skeletonDieFrames.Count;
                            }
                            else
                            {
                                skeleton.stat.state = "Hurt";
                                skeleton.nFr = skeletonHurtFrames.Count;
                            }
                            break; // Only damage one skeleton per bullet
                        }
                    }
                }
                else
                {
                    // Initialize bullet when not fired
                    if (bullet.stat == null)
                        bullet.stat = new states();

                    if (facing == 1) 
                    {
                        bullet.stat.state = "right";
                        bullet.x = lHero[0].x + lHero[0].img.Width; 
                        if (bullet.stat.state == "right" && bullet.isFlipped)
                        {
                            Bitmap flipped = (Bitmap)bullet.img.Clone();
                            flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            bullet.img = flipped;
                            bullet.isFlipped = false; 
                        }
                    }
                    else if (facing == -1) 
                    {
                        bullet.stat.state = "left";
                        bullet.x = lHero[0].x; 
                                               
                        if (bullet.stat.state == "left" && !bullet.isFlipped) 
                        {
                            Bitmap flipped = (Bitmap)bullet.img.Clone();
                            flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            bullet.img = flipped;
                            bullet.isFlipped = true; 
                        }
                    }

                    bullet.y = lHero[0].y + (lHero[0].img.Height * HeroDrawWidth) / 2; // Reset vertical position
                }
                /*else
                {
                    bullet.x = lHero[0].x + (lHero[0].img.Width * HeroDrawWidth); // Reset position
                    bullet.y = lHero[0].y + (lHero[0].img.Height * HeroDrawWidth) / 2; // Reset vertical position
                }*/
            
            }

            // --- SPEAR LOGIC ---
            /*
            for (int i = lspear.Count - 1; i >= 0; i--) // backwards 
            {
                CAdvImgActor spear = lspear[i];


                if (spear.isFire)
                {
                    if (spear.stat.state == "right")
                        spear.x += HeroSpeed - 3;
                    else if (spear.stat.state == "left")
                        spear.x -= HeroSpeed - 3;

                    // Check if the spear is out of bounds
                    if (spear.x < 0 || spear.x > this.ClientSize.Width * 3)
                    {
                        spear.isFire = false;
                        continue;
                    }
                    // Check for collision with Hero
                    
                    CAdvImgActor hero = lHero[0];

                    if (hero.stat.state == "Hurt" || hero.stat.state == "Die")
                        continue;

                    int heroWorldX = hero.x;
                    int herocW = hero.img.Width;
                    int heroLeft = heroWorldX;
                    int heroRight = heroWorldX + heroW;

                    if (spear.x + spear.img.Width > heroLeft && spear.x < heroRight) // Check for collision
                    {
                        
                        HeroHlth--;
                        spear.isFire = false; // Deactivate the spear
                        hero.iFr = 0; // Reset animation frame
                        if (HeroHlth<= 0)
                        {
                            hero.stat.state = "Die";
                            hero.nFr = DieFrames.Count;
                        }
                        else
                        {
                            hero.stat.state = "Hurt";
                            hero.nFr = HurtFrames.Count;
                        }
                        break; // Only damage one orc per bullet
                    }
                    
                    
                }
                else
                {
                    // Initialize spear when not fired
                    if (spear.stat == null)
                        spear.stat = new states();

                    if (facing == 1)
                    {
                        spear.stat.state = "right";
                        spear.x = lHero[0].x + lHero[0].img.Width;
                        if (spear.stat.state == "right" && spear.isFlipped)
                        {
                            Bitmap flipped = (Bitmap)spear.img.Clone();
                            flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            spear.img = flipped;
                            spear.isFlipped = false;
                        }
                    }
                    else if (facing == -1)
                    {
                        spear.stat.state = "left";
                        spear.x = lHero[0].x;

                        if (spear.stat.state == "left" && !spear.isFlipped)
                        {
                            Bitmap flipped = (Bitmap)spear.img.Clone();
                            flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            spear.img = flipped;
                            spear.isFlipped = true;
                        }
                    }

                    //spear.y = lHero[0].y + (lHero[0].img.Height * HeroDrawWidth) / 2; // Reset vertical position
                }
                
            }
            */
            // --- FIXED SPEAR LOGIC ---
            for (int i = lspear.Count - 1; i >= 0; i--) // backwards 
            {
                CAdvImgActor spear = lspear[i];
                if (spear.isFire)
                {
                    // Move spear
                    if (spear.stat.state == "right")
                        spear.x += HeroSpeed - 3;
                    else if (spear.stat.state == "left")
                        spear.x -= HeroSpeed - 3;

                    // Check if the spear is out of bounds
                    if (spear.x < -100 || spear.x > this.ClientSize.Width * 3 + 100)
                    {
                        spear.isFire = false;
                        continue;
                    }

                    CAdvImgActor hero = lHero[0];

                    // Skip if hero is already hurt or dead
                    if (hero.stat.state == "Hurt" || hero.stat.state == "Die")
                        continue;

                    int heroWorldX = hero.x + scrollX;
                    int heroW = hero.img.Width/* 2*/; 
                    int heroWorldY = hero.y;
                    int heroH = hero.img.Height * 2;

                    // Get spear dimensions
                    int spearW = spear.img.Width / 2;
                    int spearH = spear.img.Height/2;

                    bool collisionX ;

                    //if (lHero[0].x <= lspear[i].x)
                    //    collisionX = (spear.x <= heroWorldX + heroW) && (spear.x + spearW <= heroWorldX);
                    //else
                    //    collisionX = (spear.x + spearW >= heroWorldX) && (spear.x <= heroWorldX + heroW);
                    
                    collisionX = (spear.x < heroWorldX + heroW) && (spear.x + spearW > heroWorldX);
                    bool collisionY = (spear.y + spearH > heroWorldY) && (spear.y < heroWorldY + heroH);

                    
                    if (collisionX && collisionY)
                    {
                        if (lHero[0].stat.state == "Shield")
                        {
                            spear.isFire = false; 
                            continue;
                        }
                        HeroHealth--;
                        spear.isFire = false; 
                        hero.iFr = 0; 


                        if (HeroHealth <= 0 && hero.stat.state != "Dead")
                        {
                            hero.stat.state = "Dead";
                            hero.nFr = DieFrames.Count;
                            hero.iFr = 0;
                            isDead = true;
                        }
                        else
                        {
                            hero.stat.state = "Hurt";
                            hero.nFr = HurtFrames.Count;
                        }

                        break; 
                    }
                }
                else
                {
                    // Initialize spear when not fired
                    if (spear.stat == null)
                        spear.stat = new states();

                }
            }
            // --- LASER LOGIC ---
            bool laserActive = lHero[0].y + lHero[0].img.Height * HeroDrawWidth >= sensorY &&
                    lHero[0].x + lHero[0].img.Width + scrollX - 30 >= lLadder[0].x + (ladder.Width / 2) && lHero[0].x + scrollX< lCannon[0].x;
            if (laserActive && lHero[0].stat.state != "Hurt" && ctTick % 15 == 0)
            {
                lHero[0].stat.state = "Hurt";
                lHero[0].nFr = HurtFrames.Count;
                lHero[0].iFr = 0;
                HeroHealth--;
            }

            // --- ELEVATOR LOGIC ---

            if (onElevator && Elevator.y < groundY1 - 40 && (Elevator.stat.state == "up" || Elevator.stat.state == "movingD"))
            {
                Elevator.stat.state = "movingD";
                Elevator.y += 10; 
                lHero[0].y = Elevator.y - (lHero[0].img.Height * HeroDrawWidth); 
            }
            if (onElevator && Elevator.y > lLadder[0].y && (Elevator.stat.state == "down" || Elevator.stat.state == "movingU"))
            {
                Elevator.stat.state = "movingU";
                Elevator.y -= 10;
                lHero[0].y = Elevator.y - (lHero[0].img.Height * HeroDrawWidth);
            }
            if (Elevator.y <= lLadder[0].y)
            {
                Elevator.stat.state = "up";
            }
            if (Elevator.y >= groundY1 - 40)
            {
                Elevator.stat.state = "down";
            }

            // win condition
            if (lHero[0].x + lHero[0].img.Width + scrollX >= ldoor[0].x)
            {
                isWin = true;
            }

            ctTick++;
            DrawDB(this.CreateGraphics());
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (isAttacking || isJumping)
                return;

            //lHero[0].iFr = 0;

            if (e.KeyCode == Keys.D)
            {
                rightHeld = false;
                if (leftHeld)
                {
                    if (lHero[0].stat.state != "walk")
                    {
                        HeroDir = -1;
                        lHero[0].stat.state = "walk";
                        lHero[0].nFr = walkFrames.Count;
                        lHero[0].iFr = 0;
                    }
                }
                else
                {
                    if (lHero[0].stat.state != "idle")
                    {
                        HeroDir = 0;
                        lHero[0].stat.state = "idle";
                        lHero[0].nFr = idleFrames.Count;
                        lHero[0].iFr = 0;
                    }
                }
            }
            else if (e.KeyCode == Keys.A)
            {
                leftHeld = false;
                if (rightHeld)
                {
                    if (lHero[0].stat.state != "walk")
                    {
                        HeroDir = 1;
                        lHero[0].stat.state = "walk";
                        lHero[0].nFr = 8;
                        lHero[0].iFr = 0;
                    }
                }
                else
                {
                    if (lHero[0].stat.state != "idle")
                    {
                        HeroDir = 0;
                        lHero[0].stat.state = "idle";
                        lHero[0].nFr = 4;
                        lHero[0].iFr = 0;
                    }
                }
            }
            if (e.KeyCode == Keys.S)
            {
                run = false;
                if (HeroDir != 0)
                {
                    if (lHero[0].stat.state != "walk")
                    {
                        lHero[0].stat.state = "walk";
                        lHero[0].nFr = 8;
                        lHero[0].iFr = 0;
                    }
                }
            }
            rightHeld = false;
            leftHeld = false;

        }
        void DrawDB(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            Drawscene(g2);
            g.DrawImage(off, 0, 0);
        }
        void createHero()
        {
            CAdvImgActor pnn = new CAdvImgActor();
            pnn.x = 300;
            //pnn.x = 1040;
            pnn.img = new Bitmap("pics/Knight_2/0_Idle.png");
            pnn.y = this.ClientSize.Height - (pnn.img.Height) - 350;
            pnn.stat = new states();
            pnn.stat.state = "idle";
            pnn.nFr = 4;
            pnn.iFr = 0;
            pnn.health = 5;
            pnn.CurrHlth = 5;
            lHero.Add(pnn);
        }       
        void createElevator()
        {
            CAdvImgActor elevatorpn = new CAdvImgActor();
            elevatorpn.stat = new states();
            elevatorpn.stat.state = "up";
            elevatorpn.x = elevatorLeft;
            elevatorpn.y = lLadder[0].y;
            Elevator = elevatorpn;
        }
        void createOrc()
        {
            CAdvImgActor pnn = new CAdvImgActor();
            pnn.img = orcWalkFrames[0];
            pnn.x = 1200;
            pnn.y = groundY - pnn.img.Height / 5 + 290;
            pnn.stat = new states();
            pnn.stat.state = "walk";
            pnn.nFr = orcWalkFrames.Count;
            pnn.iFr = 0;
            pnn.health = 3;
            pnn.CurrHlth = 3;
            lOrc.Add(pnn);
        }
        void createSkeleton()
        {
            CAdvImgActor pnn = new CAdvImgActor();
            pnn.img = skeletonWalkFrames[0];
            pnn.x = 4500;
            pnn.y = groundY - pnn.img.Height / 5 + 290;
            pnn.stat = new states();
            pnn.stat.state = "walk";
            pnn.nFr = skeletonWalkFrames.Count;
            pnn.iFr = 0;
            pnn.health = 3;
            pnn.CurrHlth = 3;
            lSkeleton.Add(pnn);
        }
        void createBullets()
        {
            CAdvImgActor pnn = new CAdvImgActor();
            pnn.img = HeroBullets[0];
            pnn.x = lHero[0].x + (lHero[0].img.Width);
            pnn.y = lHero[0].y + (lHero[0].img.Height * HeroDrawWidth) / 2;
            Bullets.Add(pnn);
        }
        void createSpears()
        {
            CAdvImgActor pnn = new CAdvImgActor();
            spear.MakeTransparent(spear.GetPixel(0, 0));
            pnn.img = spear;
            pnn.x = lSkeleton[0].x;
            pnn.y = lSkeleton[0].y;
            pnn.stat = new states();
            pnn.stat.state = "left"; 
            lspear.Add(pnn);
        }
        void createLadder()
        {
            CAdvImgActor ladderpn = new CAdvImgActor();
            ladderpn.img = ladder;
            ladderpn.x = 2000; 
            ladderpn.y = this.ClientSize.Height - (groundY + 10);

            // ladder width scale * 2
            // ladder height scale * 3
            lLadder.Add(ladderpn);
        }
        void createLaser()
        {
            CAdvImgActor laser = new CAdvImgActor();
            laser.img = Laser;
            laser.img.MakeTransparent(laser.img.GetPixel(0, 0));
            //Bitmap flipped = (Bitmap)laser.img.Clone();
            //flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
            //laser.img = flipped;

            laser.x = lLadder[0].x + ladder.Width/2 - 20;
            laser.y = this.ClientSize.Height - (groundY + 10) + 225; 

            lLaser.Add(laser);
        }
        void createCannon()
        {
            CAdvImgActor cannonpn = new CAdvImgActor();
            cannonpn.img = cannon;
            cannonpn.x = 3000;
            cannonpn.y = this.ClientSize.Height - (groundY + 10) + 130;
            lCannon.Add(cannonpn);
        }
        void createSensor()
        {
            CAdvImgActor sensorpn = new CAdvImgActor();
            sensorpn.img = sensor;
            sensorpn.x = 3055;
            sensorpn.y = this.ClientSize.Height - (groundY + 10) + 375;
            lsensor.Add(sensorpn);
        }
        void createTiles(int x, int y)
        {
            CAdvImgActor tile = new CAdvImgActor();
            tile.img = groundTile;
            tile.x = x;
            tile.y = y;            

            Tiles.Add(tile);
        }
        void createChest()
        {
            CAdvImgActor chestpn = new CAdvImgActor();
            chestpn.img = chestClosed;
            chestpn.x = 2190;
            chestpn.y = lLadder[0].y - chestpn.img.Height + 10;
            chestpn.stat = new states();
            chestpn.stat.state = "closed"; // Initial state of the chest
            lchest.Add(chestpn);
        }
        void createCoins(int x, int y)
        {
            CAdvImgActor coinpn = new CAdvImgActor();
            coinpn.img = coinFrames[0];
            coinpn.x = x;
            coinpn.y = y;
            coinpn.isCollected = false;
            coinpn.iFr = 0;
            coinpn.nFr = 6;
            lcoin.Add(coinpn);
        }
        void createApple()
        {
            CAdvImgActor applepn = new CAdvImgActor();
            applepn.img = apple;
            applepn.isCollected = false;
            lapple.Add(applepn);

        }
        void createDoor()
        {
            CAdvImgActor doorpn = new CAdvImgActor();
            doorpn.img = door;
            doorpn.x = 5550;
            doorpn.y = this.ClientSize.Height - 220 - door.Height;
            ldoor.Add(doorpn);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            
            if (isAttacking || isJumping || isDead) 
                return;
            

            if (e.KeyCode == Keys.D && !leftHeld && lHero[0].x + heroW < (this.ClientSize.Width * 3) - 10 && lHero[0].stat.state != "climb")
            {
                rightHeld = true;
                HeroDir = 1;
                facing = 1;
                if (prv == true)
                {
                    //lHero[0].x += lHero[0].img.Width; // adjust position for right-facing image
                }
                if (run)
                {
                    lHero[0].stat.state = "run";
                    lHero[0].nFr = 7;
                    //lHero[0].iFr = 0;
                }
                else
                {
                    lHero[0].stat.state = "walk";
                    lHero[0].nFr = 8;
                    //lHero[0].iFr = 0;
                }
                prv = false; // reset previous flip state

                
            }
            else if (e.KeyCode == Keys.A && !rightHeld && lHero[0].x > 20 && lHero[0].stat.state != "climb")
            {
                leftHeld = true;
                HeroDir = -1;
                facing = -1;
                if (prv == false)
                {
                    //lHero[0].x -= lHero[0].img.Width; // adjust position for right-facing image
                }

                if (run)
                {
                    lHero[0].stat.state = "run";
                    lHero[0].nFr = 7;
                    //lHero[0].iFr = 0;
                }
                else
                {
                    lHero[0].stat.state = "walk";
                    lHero[0].nFr = 8;
                    //lHero[0].iFr = 0;
                }
                prv = true; // reset previous flip state

            }
            else if (e.KeyCode == Keys.S && lHero[0].stat.state != "climb")
            {
                shiftHeld = true;
                run = true;
                if (HeroDir != 0)
                {
                    lHero[0].stat.state = "run";
                    lHero[0].nFr = 7;
                    //lHero[0].iFr = 0;
                }
            }
            else if (e.KeyCode == Keys.W && lLadder[0].x > (lHero[0].x + scrollX) && lLadder[0].x < (lHero[0].x + scrollX) + lHero[0].img.Width)
            {
                lHero[0].stat.state = "climb";
                //lHero[0].nFr = ClimbFrames.Count;
                //lHero[0].iFr++;
                if (lHero[0].iFr >= lHero[0].nFr)
                    lHero[0].iFr = 0; // Reset to first frame of climb animation

                //lHero[0].x = lLadder[0].x + (lLadder[0].img.Width / 2) - (lHero[0].img.Width * HeroDrawWidth / 2); // Center hero on ladder
                if (lHero[0].y + lHero[0].img.Height*2 > lLadder[0].y + 10)
                    lHero[0].y -= 20; 
            }
            else if (e.KeyCode == Keys.Space && lHero[0].stat.state != "climb")
            {
                if (!isJumping)
                {
                    isJumping = true;
                    lHero[0].stat.state = "jump";
                    lHero[0].nFr = jumpFrames.Count;
                    lHero[0].iFr = 0;
                    jumpVelocity = JumpStrength;

                    if (HeroDir == 1 || facing == 1)
                        jumpDX = HeroSpeed;
                    else if (HeroDir == -1 || facing == -1)
                        jumpDX = -HeroSpeed;
                    else
                        jumpDX = 0;
                }
            }
            else if (e.KeyCode == Keys.Q && lHero[0].stat.state != "climb")
            {
                //if (lHero[0].stat.state != "Shield")
                {
                    lHero[0].stat.state = "Shield";
                    lHero[0].nFr = ShieldFrames.Count;
                    //if (lHero[0].iFr >= lHero[0].nFr)
                    lHero[0].iFr = 0;

                }
            }
            else if (e.KeyCode == Keys.O && checkPickup)
            {
                lchest[0].stat.state = "open";
                coinCT += 20;
                checkPickup = false; 
            }
        }         
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDB(e.Graphics);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            ost = "sounds/One Last Try.mp3";
            PlayBackgroundMusic(ost);
            createHero();
            createApple();
            createDoor();
            groundY = this.ClientSize.Height - lHero[0].img.Height - 350;
            lHero[0].stat.state = "idle";
            createLadder();
            createChest();

            int tileWidth = groundTile.Width;
            int tileHeight = groundTile.Height;
            groundY1 = this.ClientSize.Height - 220;
            int startX = -(scrollX % tileWidth);


            int platform1TileCount = 0;
            int groundTileCount = 0;

            int ct = 0;
            // platform 1
            for (int x = lLadder[0].x; x < 2500; x += tileWidth)
            {
                ct++;
                createTiles(x - (ct * 16), lLadder[0].y);
                platform1TileCount++;
            }

            ct = 0;
            // Ground floor
            for (int x = startX; x < this.ClientSize.Width + (ct * 16); x += tileWidth)
            {
                ct++;
                createTiles(x - (ct * 16), groundY1);
                groundTileCount++;
            }

            elevatorTileStartIndex = platform1TileCount + groundTileCount;

            ct = 0;
            //platform2
            for (int x = lLadder[0].x + 600; x < 3500; x += tileWidth)
            {
                ct++;
                createTiles(x - (ct * 16) , lLadder[0].y);
            }

            //elevator
            ct = 0;
            for (int x = elevatorLeft; x < elevatorRight; x += tileWidth)
            {
                ct++;
                createTiles(x - (ct * 16), lLadder[0].y);
            }
            ct = 0;


            for (int i = 0; i < 4; i++) 
                idleFrames.Add(new Bitmap($"pics/Knight_2/{i}_Idle.png"));

            for (int i = 0; i < 8; i++)
                walkFrames.Add(new Bitmap($"pics/Knight_2/{i}_Walk.png"));

            for (int i = 0; i < 7; i++)
                Run.Add(new Bitmap($"pics/Knight_2/{i}_Run.png"));

            for (int i = 0; i < 5; i++) 
                attackFrames1.Add(new Bitmap($"pics/Knight_2/Attack 1/0{i}_Attack 1.png"));

            for (int i = 0; i < 4; i++)
                if  (i != 2)
                    attackFrames2.Add(new Bitmap($"pics/Knight_2/Attack 2/0{i}_Attack 2BB.png"));
                else
                    attackFrames2.Add(new Bitmap($"pics/Knight_2/Attack 2/0{i}_Attack 2.png"));

            for (int i = 0; i < 6; i++)
                        jumpFrames.Add(new Bitmap($"pics/Knight_2/Jump/0{i}_Jump.png"));

            for (int i = 0; i < 2; i++)
                HurtFrames.Add(new Bitmap($"pics/Knight_2/Hurt/0{i}_Hurt.png"));
            
            for (int i = 0; i < 6; i++)
                DieFrames.Add(new Bitmap($"pics/Knight_2/Dead/0{i}_Dead.png"));
            
            for (int i = 0; i < 5; i++)
                ShieldFrames.Add(new Bitmap($"pics/Knight_2/Defend/0{i}_Defend.png"));

            
            // read orc frames
            for (int i = 0; i < 14; i++)
                if (i < 10)
                    orcDieFrames.Add(new Bitmap($"pics/enemies/Orc/PNG/PNG Sequences/Dying/0_Orc_Dying_00{i}.png"));
                else
                    orcDieFrames.Add(new Bitmap($"pics/enemies/Orc/PNG/PNG Sequences/Dying/0_Orc_Dying_0{i}.png"));

            for (int i = 0; i < 11; i++)
                if (i < 10)
                    orcHurtFrames.Add(new Bitmap($"pics/enemies/Orc/PNG/PNG Sequences/Hurt/0_Orc_Hurt_00{i}.png"));
                else
                    orcHurtFrames.Add(new Bitmap($"pics/enemies/Orc/PNG/PNG Sequences/Hurt/0_Orc_Hurt_0{i}.png"));

            for (int i = 0; i < 24; i++)
                if (i < 10)
                    orcWalkFrames.Add(new Bitmap($"pics/enemies/Orc/PNG/PNG Sequences/Walking/0_Orc_Walking_00{i}.png"));
                else
                    orcWalkFrames.Add(new Bitmap($"pics/enemies/Orc/PNG/PNG Sequences/Walking/0_Orc_Walking_0{i}.png"));

            for (int i = 0; i < 12; i++)
                if (i < 10)
                    orcAttackFrames.Add(new Bitmap($"pics/enemies/Orc/PNG/PNG Sequences/Slashing/0_Orc_Slashing_00{i}.png"));
                else
                    orcAttackFrames.Add(new Bitmap($"pics/enemies/Orc/PNG/PNG Sequences/Slashing/0_Orc_Slashing_0{i}.png"));
            /////////////////////////////
            // read skeleton frames
            for (int i = 0; i < 14; i++)
                if (i < 10)
                    skeletonDieFrames.Add(new Bitmap($"pics/enemies/Skeleton_Warrior_1/PNG/PNG Sequences/Dying/0_Skeleton_Warrior_Dying_00{i}.png"));
                else
                    skeletonDieFrames.Add(new Bitmap($"pics/enemies/Skeleton_Warrior_1/PNG/PNG Sequences/Dying/0_Skeleton_Warrior_Dying_0{i}.png"));

            for (int i = 0; i < 11; i++)
                if (i < 10)
                    skeletonHurtFrames.Add(new Bitmap($"pics/enemies/Skeleton_Warrior_1/PNG/PNG Sequences/Hurt/0_Skeleton_Warrior_Hurt_00{i}.png"));
                else
                    skeletonHurtFrames.Add(new Bitmap($"pics/enemies/Skeleton_Warrior_1/PNG/PNG Sequences/Hurt/0_Skeleton_Warrior_Hurt_0{i}.png"));

            for (int i = 0; i < 24; i++)
                if (i < 10)
                    skeletonWalkFrames.Add(new Bitmap($"pics/enemies/Skeleton_Warrior_1/PNG/PNG Sequences/Walking/0_Skeleton_Warrior_Walking_00{i}.png"));
                else
                    skeletonWalkFrames.Add(new Bitmap($"pics/enemies/Skeleton_Warrior_1/PNG/PNG Sequences/Walking/0_Skeleton_Warrior_Walking_0{i}.png"));

            for (int i = 0; i < 12; i++)
                if (i < 10)
                    skeletonAttackFrames.Add(new Bitmap($"pics/enemies/Skeleton_Warrior_1/PNG/PNG Sequences/Slashing/0_Skeleton_Warrior_Slashing_00{i}.png"));
                else
                    skeletonAttackFrames.Add(new Bitmap($"pics/enemies/Skeleton_Warrior_1/PNG/PNG Sequences/Slashing/0_Skeleton_Warrior_Slashing_0{i}.png"));

            for (int i = 0; i < 12; i++) // Throw
                if (i < 10)
                    skeletonThrowFrames.Add(new Bitmap($"pics/enemies/Skeleton_Warrior_1/PNG/PNG Sequences/Throwing/0_Skeleton_Warrior_Throwing_00{i}.png"));
                else
                    skeletonThrowFrames.Add(new Bitmap($"pics/enemies/Skeleton_Warrior_1/PNG/PNG Sequences/Throwing/0_Skeleton_Warrior_Throwing_0{i}.png"));


            HeroBullets.Add(new Bitmap("pics/Knight_2/Bullet.png"));
            chestFrames.Add(chestClosed);
            chestFrames.Add(chestOpened);

            for (int i = 1; i < 7; i++)
            {
                coinFrames.Add(new Bitmap($"pics/collectableItems/coin_0{i}.png"));
            }

            for (int i = 0; i < herobullets; i++)
            {
                createBullets();
                Bullets[i].isFire = false;
            }

            createSkeleton();

            for (int i = 0; i < herobullets; i++)
            {
                createSpears();
                lspear[i].isFire = false;
            }

            createCoins(2770, lLadder[0].y - coinFrames[0].Height + 70); // 1st coin
            createCoins(3000, lLadder[0].y - coinFrames[0].Height + 70); // 2nd coin
            createCoins(2400, this.ClientSize.Height - 150 - coinFrames[0].Height); // laser coin
            
            createOrc();// 1
            //createOrc();// 2
            //lOrc[1].x = 900;

            createSensor();
            createCannon();
            createLaser();
            createElevator();
        }
        int HealthBar(Graphics g2, int healthCt, bool checkPickup, int coinCt)
        {
            int healthX = 10;
            int healthY = 10;
            
            // Draw the heart icon
            for (int i = 0; i < healthCt; i++)
            {
                g2.DrawImage(life, healthX + (i * 40), healthY, 30, 30);
            }

            // obtained key
            if (checkPickup)
            {
                g2.DrawImage(key, healthX + 100, healthY + 40, key.Width / 2, key.Height / 2);
            }

            // Draw Coin
            g2.DrawImage(coinFrames[0], healthX, healthY + 40, coinFrames[0].Width/3, coinFrames[0].Height/3);
            g2.DrawString(coinCt.ToString(), new Font("Arial", 26, FontStyle.Bold), Brushes.Yellow, new PointF(healthX + 40, healthY + 40));

            return healthCt;
        }
        void Drawscene(Graphics g2)
        {
            g2.Clear(Color.Black);

            if (isDead && GameOver)
            {
                g2.DrawImage(Die, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
                string DSdeath = "sounds/DSdeath.mp3";
                PlaySoundEffect(DSdeath);

                return;
            }

            if (isWin)
            {
                g2.DrawImage(win, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
                return;
            }

            Rectangle srcRect = new Rectangle(0, 0, iarena.Width, iarena.Height);
            Rectangle dstRect = new Rectangle(-scrollX, 0, this.ClientSize.Width, this.ClientSize.Height);

            Rectangle srcRect2 = new Rectangle(0, 0, iarena.Width, iarena.Height);
            Rectangle dstRect2 = new Rectangle(this.ClientSize.Width - scrollX - 1, 0, this.ClientSize.Width, this.ClientSize.Height);

            // Drawing the background images        
            // Draw three copies: previous, current, next
            for (int bg = 0; bg <= 2; bg++)
            {
                if (bg == 1)
                    dstRect = new Rectangle((bg * this.ClientSize.Width) - scrollX -1, 0,
                                                this.ClientSize.Width, this.ClientSize.Height);
                else if (bg == 2)
                    dstRect = new Rectangle((bg * this.ClientSize.Width) - scrollX - 2, 0,
                                                this.ClientSize.Width, this.ClientSize.Height);
                g2.DrawImage(iarena, dstRect, srcRect, GraphicsUnit.Pixel);
                g2.DrawImage(iarena2, dstRect, srcRect, GraphicsUnit.Pixel);
                g2.DrawImage(clone, dstRect, srcRect, GraphicsUnit.Pixel);
                g2.DrawImage(clone2, dstRect, srcRect, GraphicsUnit.Pixel);
            }

            // health bar
            HeroHealth = HealthBar(g2, HeroHealth, checkPickup, coinCT);
                
            int tileWidth = groundTile.Width;
            int tileHeight = groundTile.Height;
            int groundY1 = this.ClientSize.Height - 220;

            int startX = -(scrollX % tileWidth);
            int ct = 0;
            int tmp = 0;
            

            platformLeft = lLadder[0].x ;
            platformRight = 2470 ;
            // Draw Tiles
            if (true)
            {
                //platform1
                ct = 0;
                for (int x = lLadder[0].x; x < 2500; x += tileWidth)
                {
                    ct++;
                    g2.DrawImage(groundTile, x - (ct * 16) - scrollX, lLadder[0].y, tileWidth, tileHeight);
                }

                for (int x = startX; x < this.ClientSize.Width + (ct * 16); x += tileWidth)
                {
                    ct++;
                    g2.DrawImage(groundTile, x - (ct * 16), groundY1, tileWidth, tileHeight);
                }

                ct = 0;
                for (int i = groundY1 + 30; i < this.ClientSize.Height; i += tileHeight - 25)
                {
                    ct = 0;
                    for (int x = startX; x < this.ClientSize.Width + (ct * 16); x += tileWidth)
                    {
                        ct++;
                        g2.DrawImage(groundTile2, x - (ct * 16), i, tileWidth, tileHeight);
                        //createTiles(x - (ct * 16), i);
                    }
                }
                

            }

            //Draw  -platform 2-
            ct = 0;
            int tileIndex = elevatorTileStartIndex;
            platform2Left = (lLadder[0].x + 600);
            platform2Right = 3493 - 270;
            for (int x = lLadder[0].x + 600; x < 3500; x += tileWidth)
            {
                ct++;
                //platform2Left = (lLadder[0].x + 600) - (ct * 16);/////////////////////
                if (tileIndex < Tiles.Count)
                {
                    //Tiles[tileIndex].x = x - (ct * 16) - scrollX;
                    g2.DrawImage(groundTile, x - (ct * 16) - scrollX, lLadder[0].y, tileWidth, tileHeight);
                    tileIndex++;
                }
            }

            //Draw Elevtor
            elevatorLeft = 3400;
            elevatorRight = 3650;
            ct = 0;
            for (int x = elevatorLeft; x < elevatorRight + 150; x += tileWidth)
            {
                ct++;
                Elevator.x = x - (ct * 16) - scrollX;
                
                g2.DrawImage(groundTile, Elevator.x, Elevator.y, tileWidth, tileHeight);
            }

            // Draw Coins
            // set coins positions

            for (int i = 0; i < lcoin.Count; i++)
            {
                CAdvImgActor coin = lcoin[i];
                if (!coin.isCollected)
                {
                    Bitmap coinFrame = coinFrames[coin.iFr];
                    g2.DrawImage(coinFrame, coin.x - scrollX, coin.y, coinFrame.Width / 3, coinFrame.Height / 3);
                }
            }

            // Draw Apple
            if (lapple.Count > 0)
            {
                CAdvImgActor apple = lapple[0];
                apple.x = elevatorRight + 100;
                apple.y = this.ClientSize.Height - 220 - apple.img.Height/2;
                g2.DrawImage(apple.img, apple.x - scrollX, apple.y, apple.img.Width / 2, apple.img.Height / 2);
            }

            // Draw Door
            if (ldoor.Count > 0)
            {
                CAdvImgActor door = ldoor[0];

                g2.DrawImage(door.img, door.x - scrollX, door.y, door.img.Width, door.img.Height);
            }

            // Draw Chest
            for (int i = 0; i < lchest.Count; i++)
            {
                CAdvImgActor chest = lchest[i];
                
                if (chest.stat.state == "open")
                {
                    chest.img = chestOpened;
                }
                else
                {
                    chest.img = chestClosed;
                }
                g2.DrawImage(chest.img, chest.x - scrollX, chest.y, chest.img.Width, chest.img.Height);
            }

            // chest message
            if (onLadderPlatform)
            {
                g2.FillRectangle(Brushes.LightSalmon, lchest[0].x - scrollX - 5, lchest[0].y - 20, lchest[0].img.Width, 35);
                g2.DrawString("Press O to Open", new Font("Arial", 12), Brushes.Black, new PointF(lchest[0].x - scrollX -5, lchest[0].y - 20) /*, lchest[0].img.Width, 55*/);
                //g2.DrawString("Hero X: " + lHero[0].x.ToString(), new Font("Arial", 16), Brushes.White, new PointF(10, 10));
            }

            // Draw Ladder
            for (int i = 0; i < lLadder.Count; i++)
            {
                g2.DrawImage(lLadder[i].img, lLadder[i].x - scrollX, lLadder[i].y, lLadder[i].img.Width/2, (lLadder[i].img.Height/2) + 120);
            }

            // Drawing the Orcs
            for (int i = lOrc.Count - 1; i >= 0; i--)
            {
                
                if (lOrc[i].stat.state == "Die")
                {
                    if (lOrc[i].iFr >= lOrc[i].nFr - 1)
                    {
                        if (!KeySpawn)
                            keyTmpx = lOrc[i].x - scrollX;

                        lOrc.RemoveAt(i);
                        KeySpawn = true;
                        i--; 
                        continue; 
                    }
                }
                
                CAdvImgActor pnnE = lOrc[i];
                Bitmap orcFrame;
                if (pnnE.stat.state == "attack")
                    orcFrame = orcAttackFrames[pnnE.iFr];
                else if (pnnE.stat.state == "Hurt")
                    orcFrame = orcHurtFrames[pnnE.iFr];
                else if (pnnE.stat.state == "Die")
                    orcFrame = orcDieFrames[pnnE.iFr];
                else
                    orcFrame = orcWalkFrames[pnnE.iFr];



                // Flip if moving left
                if (pnnE.dir == -1)
                {
                    Bitmap flipped = (Bitmap)orcFrame.Clone();
                    flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    orcFrame = flipped;
                }

                float percentHealth = ((float)pnnE.CurrHlth / pnnE.health);
                float realH = (orcFrame.Width / 5) * percentHealth;
                g2.DrawRectangle(Pens.Black, pnnE.x - scrollX, pnnE.y - 10, orcFrame.Width / 5 + 1, 5 + 1);
                g2.FillRectangle(Brushes.Red, pnnE.x - scrollX + 1, pnnE.y - 10 + 1, realH, 5);

                g2.DrawImage(orcFrame, pnnE.x - scrollX, pnnE.y, orcFrame.Width / 5, orcFrame.Height / 5);
            }

            // Drawing the Skeletons
            for (int i = lSkeleton.Count - 1; i >= 0; i--)
            {

                if (lSkeleton[i].stat.state == "Die")
                {
                    if (lSkeleton[i].iFr >= lSkeleton[i].nFr - 1)
                    {
                        //if (!KeySpawn)
                            //keyTmpx = lSkeleton[i].x - scrollX;

                        lSkeleton.RemoveAt(i);
                        //KeySpawn = true;
                        i--; 
                        continue; 
                    }
                }

                CAdvImgActor pnnE = lSkeleton[i];
                Bitmap skeletonFrame;
                if (pnnE.stat.state == "attack")
                    skeletonFrame = skeletonAttackFrames[pnnE.iFr];
                else if (pnnE.stat.state == "Hurt")
                    skeletonFrame = skeletonHurtFrames[pnnE.iFr];
                else if (pnnE.stat.state == "Die")
                    skeletonFrame = skeletonDieFrames[pnnE.iFr];
                else if (pnnE.stat.state == "Throw")
                    skeletonFrame = skeletonThrowFrames[pnnE.iFr];
                else
                    skeletonFrame = skeletonWalkFrames[pnnE.iFr];



                // Flip if moving left
                if (pnnE.dir == -1)
                {
                    Bitmap flipped = (Bitmap)skeletonFrame.Clone();
                    flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    skeletonFrame = flipped;
                }

                float percentHealth = ((float)pnnE.CurrHlth / pnnE.health);
                float realH = (skeletonFrame.Width / 5) * percentHealth;
                g2.DrawRectangle(Pens.Black, pnnE.x - scrollX, pnnE.y - 10, skeletonFrame.Width / 5 + 1, 5 + 1);
                g2.FillRectangle(Brushes.Red, pnnE.x - scrollX + 1, pnnE.y - 10 + 1, realH, 5);

                g2.DrawImage(skeletonFrame, pnnE.x - scrollX, pnnE.y, skeletonFrame.Width / 5, skeletonFrame.Height / 5);
            }

            // Draw KEY
            if (KeySpawn)
            {
                //checkPickup = (lHero[0].x + lHero[0].img.Width) > keyTmpx;
                checkPickup = true;
                if (!checkPickup)
                {
                    //g2.DrawImage(key, keyTmpx, groundY + 210, key.Width / 2, key.Height / 2);
                }
            }
            
            //Draw patrol limits: (FOR DEBUG)
            //g2.DrawLine(Pens.Red, patrolLeft - scrollX, groundY1, patrolLeft - scrollX, this.ClientSize.Height);
            //g2.DrawLine(Pens.Red, patrolRight - scrollX, groundY1, patrolRight - scrollX, this.ClientSize.Height);

            // Draw cannon
            for (int i = 0; i < lCannon.Count; i++)
            {
                CAdvImgActor cannon = lCannon[i];
                g2.DrawImage(cannon.img, cannon.x - scrollX, cannon.y, (cannon.img.Width / 2) - 20, cannon.img.Height / 2);
            }

            // Draw sensor
            for (int i = 0; i < lsensor.Count; i++)
            {
                CAdvImgActor sensor = lsensor[i];
                g2.DrawImage(sensor.img, sensor.x - scrollX, sensor.y, sensor.img.Width / 2, sensor.img.Height / 2);
            }

            // Draw Laser
            if (lHero[0].y + lHero[0].img.Height * HeroDrawWidth >= sensorY &&
                    lHero[0].x + lHero[0].img.Width + scrollX - 30 >= lLadder[0].x + (ladder.Width / 2) && lHero[0].x + scrollX < lCannon[0].x)
            {
                for (int i = 0; i < lLaser.Count; i++)
                {
                    CAdvImgActor laser = lLaser[i];
                    g2.DrawImage(laser.img, laser.x - scrollX, laser.y, laser.img.Width * 2 + 100, laser.img.Height);
                }
            }

            // Draw Sensor line
            g2.DrawLine(Pens.Red, (lLadder[0].x + ladder.Width / 2) - scrollX, lsensor[0].y + 10, lsensor[0].x - scrollX, lsensor[0].y + 10);
            sensorY = lsensor[0].y + 10;

            // Hero Draw
            CAdvImgActor pnn = lHero[0];
            Bitmap frame = null;
            // Handling Hero states and frames
            if (true)
            {
                if (pnn.stat.state == "idle")
                {
                    frame = idleFrames[pnn.iFr];
                }
                else if (pnn.stat.state == "walk")
                {
                    frame = walkFrames[pnn.iFr];
                }
                else if (pnn.stat.state == "run")
                {
                    frame = Run[pnn.iFr];
                }
                else if (pnn.stat.state == "attack1")
                {
                    frame = attackFrames1[pnn.iFr];
                }
                else if (pnn.stat.state == "attack2")
                {
                    frame = attackFrames2[pnn.iFr];
                }
                else if (pnn.stat.state == "jump")
                {
                    frame = jumpFrames[pnn.iFr];
                }
                else if (pnn.stat.state == "Hurt")
                {
                    frame = HurtFrames[pnn.iFr];
                }
                else if (pnn.stat.state == "Dead")
                {
                    frame = DieFrames[pnn.iFr];
                }
                else if (pnn.stat.state == "Shield")
                {
                    frame = ShieldFrames[pnn.iFr];
                }
                else
                {
                    frame = idleFrames[0]; // Default to idle if no state matches
                }
            }
            // Flip the frame if facing left
            if (facing == -1)
            {
                Bitmap flipped = (Bitmap)frame.Clone();
                flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
                frame = flipped;
            }
            lHero[0] = pnn;
            
            g2.DrawImage(frame, pnn.x, pnn.y, frame.Width * 2, frame.Height * 2);
            ///*DEBUG*/g2.DrawRectangle(Pens.Red, pnn.x, pnn.y, frame.Width * 2, frame.Height * 2);

            if (HeroHealth <= 0)
            {
                isDead = true;
            }

            //debug
            {
                //g2.DrawString("Hero X: " + lHero[0].x.ToString(), new Font("Arial", 16), Brushes.White, new PointF(10, 10));
                //g2.DrawString("Orc X: " + lOrc[0].x.ToString(), new Font("Arial", 16), Brushes.White, new PointF(10, 30));

                //g2.DrawString("Hero World X: " + (lHero[0].x + scrollX).ToString(), new Font("Arial", 16), Brushes.White, new PointF(10, 60));
                //g2.DrawString("ScrollX: " + scrollX.ToString(), new Font("Arial", 16), Brushes.White, new PointF(10, 90));

                //int heroX = lHero[0].x;
                //int heroW = lHero[0].img.Width * HeroDrawWidth;
                //int orcW = lOrc[0].img.Width / OrcDrawScale;
                //int orcX = lOrc[0].x;
                //int orcLeft = orcX;
                //int orcRight = orcX + orcW;
                //int heroLeft = heroX;
                //int heroRight = heroX + heroW;

                //int distToHero;
                //if (orcRight < heroLeft)
                //    distToHero = heroLeft - orcRight;
                //else if (heroRight < orcLeft)
                //    distToHero = orcLeft - heroRight;
                //else
                //    distToHero = 0; // Overlapping

                //g2.DrawString("Distance Gap: " + distToHero.ToString(), new Font("Arial", 16), Brushes.White, new PointF(10, 90));


                //g2.DrawString("Dist X: " + ((lOrc[0].x) - (lHero[0].x)).ToString(), new Font("Arial", 16), Brushes.White, new PointF(10, 110));
            }

            // Draw Bullets
            for (int i = 0; i < Bullets.Count; i++)
            {
                CAdvImgActor bullet = Bullets[i];
                if (bullet.isFire)
                {
                    g2.DrawImage(bullet.img, bullet.x - scrollX, bullet.y, bullet.img.Width, bullet.img.Height);
                }
            }
            // Draw spear
            for (int i = 0; i < lspear.Count; i++)
            {
                CAdvImgActor spear = lspear[i];

                if (spear.isFire)
                {
                    int screenX = spear.x - scrollX;
                    int screenY = spear.y;
                    //*Debug*/  g2.FillRectangle(Brushes.Red, screenX, screenY, 10, 10);

                    //g2.FillRectangle(Brushes.Red, screenX, screenY, 10, 10);
                    //g2.DrawString($"S:({spear.x},{spear.y})", new Font("Arial", 8), Brushes.Yellow, screenX, screenY - 15);

                    g2.DrawImage(spear.img, screenX, screenY, spear.img.Width / 2, spear.img.Height / 2);
                    

                    // DEBUG:
                    /* Draw red rectangle (remove this once working)
                    //g2.FillRectangle(Brushes.Red, screenX, screenY, 20, 5);

                    // DEBUG: Show spear info
                    //g2.DrawString($"S:({spear.x},{spear.y})", new Font("Arial", 8), Brushes.Yellow, screenX, screenY - 15);
                    */
                }
            }
            /*
            //// Simple spear drawing test
            //foreach (CAdvImgActor spear in lspear)
            //{
            //    if (spear.isFire)
            //    {
            //        // Draw a simple red rectangle as spear
            //        g2.FillRectangle(Brushes.Red, spear.x - scrollX, spear.y, 20, 5);
            //    }
            //}*/
        }

    }

    public class CImgActor
    {
        public int x, y, w, h;
        public Bitmap img;

    }
    public class CAdvImgActor
    {
        public Bitmap img;
        public Rectangle rcDst;
        public Rectangle rcSrc;
        public int x;
        public int y;
        public int dx = 0;
        public int dy = 0;
        public int iFr; // curent frame     
        public int nFr; // number of frames
        public states stat;
        public int dir = 1;
        public int health;
        public int CurrHlth;
        public bool isBeingHurt = false;
        public bool isFire = false;
        public bool isFlipped = false;
        public bool isCollected = false; 
    }

    public class states
    {
        public string state = "idle";
        public List<Bitmap> imgs = new List<Bitmap>();
    }
}
