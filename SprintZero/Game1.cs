using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using SprintZero.Controllers;
using SpriteZero.Enemies;
using SprintZero.Marios;
using SpriteZero.Sprites;
using SprintZero.blocks;
using SprintZero.PBCollision;
using SprintZero.Map;
using SprintZero.Items;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using Microsoft.Xna.Framework.Media;
using FireballCollisions;
using ItemCollisions;
using EnemyCollisions;
using HammerCollisions;
using BowserFireballCollisions;
using SoundManager;
using System.Security.Cryptography;
using System.IO.Pipes;


namespace SprintZero;

public class Game1 : Core
{

    private TextureAtlas bigBlockTexture, bigBlockTexturePt2, itemTexture, smallMarioTexture, bigMarioTexture, fireMarioTexture, projectileTexture, goombaTexture, flagPoleTexture, hammerTexture, bowserFireballTexture, oneDashFourtexture;

    private SpriteFont font1;
    private List<IController> controllers;
    private List<ICollectable> currentItems;
    private List<IBlock> blocks;
    private List<IPipe> pipes;
    private List<IProjectile> projectiles;
    private List<Hammer> hammers;
    private List<BowserFireball> bowserFireballs;
    private List<IMario> marios;
    private List<IEnemy> enemies;
    private List<IEnemy> unspawnedEnemies;
    private List<List<IEnemy>> levelEnemies;
    public IMario currentMario;
    private bool hurryupPlayed = false;

    public List<TileMap> maps; // Temporary!
    private TileMap map; // Current map.

    public int currentMarioNum, currentLevel;
    public int coinCount, livesCount, worldNumber, levelNumber, marioScore;
    private OrthographicCamera camera;
    private float prevX;
    private const float cooldownForDamage = 1.0f;
    private bool canTakeDamage = true;
    private float cooldownTimer = 0f;
    public float gameTimer = 400f;

    public bool IsPaused { get; set; } = false;
    private Texture2D pauseTexture, winTexture;
    public bool IsGameOver { get; set; } = false;
    private Texture2D gameOverTexture;

    public Game1() : base("SMB1", 1920, 1080, false) { }


    protected override void Initialize()
    {

        controllers = new List<IController>
        {
            new KeyController(this),
            new MouseController(this)
        };

        maps = new List<TileMap>();

        base.Initialize();
        var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 1600, 960);
        camera = new OrthographicCamera(viewportAdapter);

        // fireballCollision = new FireballCollision(enemies,currentEnemyCount,currentEnemy,blocks);
    }
    protected override void LoadContent()
    {
        gameOverTexture = Content.Load<Texture2D>("Images/gameover");
        flagPoleTexture = TextureAtlas.FromFile(Content, "Images/flag.xml");
        bigBlockTexture = TextureAtlas.FromFile(Content, "images/bigblock-definition.xml");
        bigBlockTexturePt2 = TextureAtlas.FromFile(Content, "images/BigBlocks2-definition.xml");
        oneDashFourtexture = TextureAtlas.FromFile(Content, "images/1-4-definition.xml");

        Music.LoadContent(Content);

        font1 = Content.Load<SpriteFont>("Font/File");

        blocks = new List<IBlock> { };
        pipes = new List<IPipe> { };

        itemTexture = TextureAtlas.FromFile(Content, "images/items-definition.xml");

        //Projectiles
        projectileTexture = TextureAtlas.FromFile(Content, "images/Fireball-definition.xml");
        hammerTexture = TextureAtlas.FromFile(Content, "Images/Hammer-definition.xml");
        bowserFireballTexture = TextureAtlas.FromFile(Content, "Images/BowserFireball-definition.xml");

        // fireballs are dynamic objects, don't exist at load time. They are created when the player presses the fire button.
        // Fireballs will be added to the list as the user presses the shoot button.
        projectiles = new List<IProjectile>();
        hammers = new List<Hammer>();
        bowserFireballs = new List<BowserFireball>();

        // Small Mario
        smallMarioTexture = TextureAtlas.FromFile(Content, "images/SmallMario-definition.xml");

        // Big Mario
        bigMarioTexture = TextureAtlas.FromFile(Content, "images/BigMario-definition.xml");

        // Fire Mario
        fireMarioTexture = TextureAtlas.FromFile(Content, "Images/FireMario-definition.xml"); // or "images/..." depending on your output folder

        goombaTexture = TextureAtlas.FromFile(Content, "images/goomba-definition.xml");

        Koopa.LoadTextures(Content);

        enemies = new List<IEnemy>();

        levelEnemies = new List<List<IEnemy>>();

        prevX = 0;
        marioScore = 0;
        coinCount = 0;
        livesCount = 3;
        worldNumber = 1;
        levelNumber = 1;

        currentItems = new List<ICollectable>();

        currentLevel = 0;
        LoadMaps();

        marios = new List<IMario>
        {
            new SmallMario(smallMarioTexture, maps[currentLevel].getSpawn(), Content, this),
            new BigMario(bigMarioTexture, Content, this),
            new FireMario(fireMarioTexture, Content, this)
        };
        currentMario = marios[0];
        currentMarioNum = 0;

        map = maps[currentLevel];

        unspawnedEnemies = levelEnemies[currentLevel];
        pauseTexture = Content.Load<Texture2D>("Images/Pause");
        winTexture = Content.Load<Texture2D>("Images/You-Win-4-21-2026");
        base.LoadContent();

    }

    private void LoadMaps()
    {
        TextureAtlas blockTextures = TextureAtlas.FromFile(Content, "images/block-definition.xml");
        TileMap map1 = new TileMap();
        ILevel level = new LevelOne(Content, blockTextures, itemTexture, currentItems, "LevelData/LevelOne.xml", bigBlockTexturePt2, bigBlockTexture, this);
        levelEnemies.Add(level.GetEnemies());
        level.FromFile(map1);
        maps.Add(map1);
        TileMap mapBonus = new TileMap();
        level = new LevelOneBonus(Content, blockTextures, "LevelData/LevelOneBonus.xml", this);
        levelEnemies.Add(level.GetEnemies());
        level.FromFile(mapBonus);
        maps.Add(mapBonus);
        TileMap finalLevel = new TileMap();
        level = new LevelFour(Content, oneDashFourtexture, itemTexture, currentItems, "LevelData/LevelFour.xml", this);
        levelEnemies.Add(level.GetEnemies());
        level.FromFile(finalLevel);
        maps.Add(finalLevel);
        Music.PlayBackground();
    }
    private void TriggerGameOver()
    {
        IsGameOver = true;

        // Stop background music
        Music.StopMusic();

        // Play game over sound ONCE
        Music.gameOver.Play();
    }
    protected override void Update(GameTime gameTime)
    {
        if (gameTimer < 100 && !hurryupPlayed)
        {
            Music.hurryUpSound.Play();
            hurryupPlayed = true;
        }
        if (!IsPaused && !currentMario.WinState && !IsGameOver)
        {
            gameTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (gameTimer <= 0 && !IsGameOver)
            {
                gameTimer = 0;
                TriggerGameOver();
            }
        }

        foreach (IController controller in controllers)
        {
            controller.Update(gameTime);
        }

        if (IsPaused || currentMario.WinState || IsGameOver)
        {
            base.Update(gameTime);
            return;
        }

        var visibleArea = camera.BoundingRectangle;
        Rectangle cameraRect = new Rectangle(
            (int)visibleArea.Left,
            (int)visibleArea.Top,
            (int)visibleArea.Width,
            (int)visibleArea.Height
        );

        currentMario.Update(gameTime);

        map.Update(gameTime, cameraRect, 64);

        List<IBlock> collidableBlocks = map.getBlocksInRectangle(currentMario.MarioCollider, 96);
        List<IPipe> collidablePipes = map.getPipesInRectangle(currentMario.MarioCollider, 96);

        int mapChange = playerBlockCollision.checkBlockCollision(
            currentMario,
            collidableBlocks,
            collidablePipes,
            this
        ); // We should only call this method once per update.

        foreach (ICollectable item in currentItems)
        {
            item.Update(gameTime);
        }

        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            Fireball currentFireball = (Fireball)projectiles[i];
            currentFireball.Update(gameTime);
            List<IBlock> fireballCollidableBlocks = map.getBlocksInRectangle(currentFireball.FireballCollider, 64);
            List<IPipe> fireballCollidablePipes = map.getPipesInRectangle(currentFireball.FireballCollider, 64);
            FireballCollision.checkFireballBlockCollision(currentFireball, fireballCollidableBlocks);
            FireballCollision.checkFireballPipeCollision(currentFireball, fireballCollidablePipes);
            FireballCollision.checkFireballEnemyCollision(currentFireball, enemies);
            if (!currentFireball.IsActive)
            {
                projectiles.RemoveAt(i);
            }
        }

        //resets scoring stomp combo
        if (!currentMario.Falling)
        {
            ScoreManager.ResetStompCombo();
        }

        // Camera
        if (currentMario.location.X > prevX && currentMario.location.X > 560)
        {
            camera.Position = new Vector2((int)currentMario.location.X - 560f, (int)camera.Position.Y);
            prevX = currentMario.location.X;
        }

        playerBlockCollision.checkCameraCollision(currentMario, cameraRect, SetMario, Damage);

        float cameraRightEdge = visibleArea.Right;

        for (int i = unspawnedEnemies.Count - 1; i >= 0; i--)
        {
            IEnemy sleepingEnemy = unspawnedEnemies[i];
            if (cameraRightEdge > sleepingEnemy.position.X)
            {
                enemies.Add(sleepingEnemy);
                unspawnedEnemies.RemoveAt(i);
            }
        }

        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            IEnemy activeEnemy = enemies[i];

            activeEnemy.Update(gameTime);
            CheckEnemyCollisions.CheckEnemyBlockCollisions(activeEnemy, blocks, map);
            CheckEnemyCollisions.CheckEnemyPipeCollisions(activeEnemy, pipes, map);
            CheckEnemyCollisions.CheckEnemyMarioCollisions(activeEnemy, currentMario, Damage, this);

            if (i >= enemies.Count) break;

            if (activeEnemy is Goomba goomba && goomba.Despawn) enemies.RemoveAt(i);
            else if (activeEnemy is Koopa koopa && koopa.Despawn) enemies.RemoveAt(i);
        }
        CheckEnemyCollisions.CheckEnemyEnemyCollisions(enemies, this);

        List<ICollectable> collectedItems = new List<ICollectable>();

        foreach (var item in currentItems)
        {
            if (!item.Collected)
            {
                List<IBlock> itemCollidableBlocks = map.getBlocksInRectangle(item.RectCollider, 96);
                List<IPipe> itemCollidablePipes = map.getPipesInRectangle(item.RectCollider, 96);
                ItemCollision.CheckItemBlockCollisions(item, itemCollidableBlocks, map);
                ItemCollision.CheckItemPipeCollisions(item, itemCollidablePipes, map);
                ItemCollision.CheckItemMarioCollisions(item, currentMario, this);
            }
            else
            {
                collectedItems.Add(item);
            }

        }

        foreach (ICollectable item in collectedItems)
        {
            currentItems.Remove(item);
        }

        collectedItems.Clear();

        for (int i = hammers.Count - 1; i >= 0; i--)
        {
            Hammer h = hammers[i];
            h.Update(gameTime);
            h.CheckOffScreen(cameraRect);
            HammerCollision.CheckHammerMarioCollision(h, currentMario, Damage);
            if (!h.IsActive)
                hammers.RemoveAt(i);
        }

        for (int i = bowserFireballs.Count - 1; i >= 0; i--)
        {
            BowserFireball b = bowserFireballs[i];
            b.Update(gameTime);
            b.CheckOffScreen(cameraRect);
            BowserFireballCollision.CheckBowserFireballMarioCollision(b, currentMario, Damage);
            if (!b.IsActive)
                bowserFireballs.RemoveAt(i);
        }

        if (!canTakeDamage)
        {
            cooldownTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (cooldownTimer <= 0)
            {
                canTakeDamage = true;
            }
        }

        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {

        Color background = maps[currentLevel].GetBackgroundColor();
        GraphicsDevice.Clear(background);
        
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetViewMatrix());
        currentMario.Draw(SpriteBatch);
        foreach (ICollectable item in currentItems)
        {
            item.Draw(SpriteBatch);
        }
        foreach (var p in projectiles)
            p.Draw(SpriteBatch);
        foreach (IEnemy enemy in enemies)
        {
            enemy.Draw(SpriteBatch);
        }
        foreach (Hammer h in hammers)
        {
            h.Draw(SpriteBatch);
        }
        foreach (BowserFireball fireball in bowserFireballs)
        {
            fireball.Draw(SpriteBatch);
        }
        var visibleArea = camera.BoundingRectangle;
        Rectangle cameraRect = new Rectangle(
            (int)visibleArea.Left,
            (int)visibleArea.Top,
            (int)visibleArea.Width,
            (int)visibleArea.Height
        );
        map.Draw(SpriteBatch, cameraRect, 64);
        SpriteBatch.End();
        if (IsPaused)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(pauseTexture, new Rectangle(0, 0, 200, 200), Color.White);
            SpriteBatch.End();
        }
        if (currentMario.WinState)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(winTexture, new Rectangle(100, 100, 400, 200), Color.White);
            SpriteBatch.End();
        }
        if (IsGameOver)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(gameOverTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            SpriteBatch.End();
        }
        string score = marioScore.ToString("D6");
        string coins = coinCount.ToString("D2");
        int timeleft = (int)gameTimer;
        string time = timeleft.ToString("D3");
        string HUD = "MARIO              WORLD         TIME\n" + score + "   Ox" + coins + "       " + worldNumber + "-" + levelNumber + "              " + time;
        Vector2 HUDpos = new Vector2(0, 0);
        SpriteBatch.Begin();
        SpriteBatch.DrawString(font1, HUD, HUDpos, Color.White);
        SpriteBatch.End();


        base.Draw(gameTime);
    }


    private void SpawnFireball()
    {
        // 2 fireballs max
        if (projectiles.Count >= 2) return;

        Vector2 spawnPos = currentMario.location + new Vector2(currentMario.Direction ? 40f : -10f, 40f);

        // create new animated sprites for each fireball
        AnimatedSprite roll = projectileTexture.CreateAnimatedSprite("FireballRolling");
        AnimatedSprite pop = projectileTexture.CreateAnimatedSprite("FireballPop");

        var s = new Vector2(4f, 4f);
        roll.Scale = s;
        pop.Scale = s;

        projectiles.Add(new Fireball(roll, pop, spawnPos, currentMario.Direction));
    }

    public void SpawnHammer(Vector2 bowserPosition, bool direction)
    {
        AnimatedSprite spin = hammerTexture.CreateAnimatedSprite("HammerSpin");
        spin.Scale = new Vector2(4f, 4f);

        //  true : right | false : left 
        hammers.Add(new Hammer(spin, bowserPosition, direction));
    }

    public void SpawnBowserFireball(Vector2 bowserPosition, bool direction)
    {
        AnimatedSprite fire = bowserFireballTexture.CreateAnimatedSprite("BowserFireball");
        fire.Scale = new Vector2(4f, 4f);

        //  true : right | false : left 
        bowserFireballs.Add(new BowserFireball(fire, bowserPosition, direction));
    }

    public void SetMario(int marioNumber)
    {
        Vector2 currentPosition = currentMario.location;

        if (marioNumber == 0)
        {
            if (currentMarioNum > 0) currentPosition = new Vector2(currentPosition.X, currentPosition.Y + 64f);
            currentMario = new SmallMario(smallMarioTexture, currentPosition, Content, this);
            currentMarioNum = marioNumber;
        }
        else if (marioNumber == 1)
        {
            if (currentMarioNum == 0) currentPosition = new Vector2(currentPosition.X, currentPosition.Y - 64f);
            float velocity = currentMario.yVelocity;
            currentMario = new BigMario(bigMarioTexture, currentPosition);
            currentMarioNum = marioNumber;
            currentMario.yVelocity = velocity;
            currentMario.Falling = true;
        }
        else if (marioNumber == 2)
        {
            if (currentMarioNum == 0) currentPosition = new Vector2(currentPosition.X, currentPosition.Y - 64f);
            float velocity = currentMario.yVelocity;
            currentMario = new FireMario(fireMarioTexture, currentPosition, Content);
            currentMarioNum = marioNumber;
            currentMario.yVelocity = velocity;
            currentMario.Falling = true;
        }
        canTakeDamage = true;
        cooldownTimer = 0f;

    }
    public void MarioJump()
    {
        if (IsPaused || currentMario.WinState) return;
        currentMario.Jump();
    }
    public void MarioCrouch()
    {
        if (IsPaused || currentMario.WinState) return;
        currentMario.Crouching = true;
        currentMario.Crouch();
    }
    public void MarioUncrouch()
    {
        if (IsPaused || currentMario.WinState) return;
        currentMario.Crouching = false;
        currentMario.Crouch();
    }
    public void MarioFire()
    {
        if (currentMarioNum == 2)
        {
            if (IsPaused || currentMario.WinState) return;
            currentMario.Fireball();
            SpawnFireball();
        }
    }
    public void MarioRight()
    {
        if (IsPaused || currentMario.WinState) return;
        currentMario.Direction = true;
        currentMario.Move();
    }
    public void MarioLeft()
    {
        if (IsPaused || currentMario.WinState) return;
        currentMario.Direction = false;
        currentMario.Move();
    }
    public void StopMarioRight()
    {
        if (IsPaused || currentMario.WinState) return;
        currentMario.Direction = true;
        currentMario.StopMove();
    }
    public void StopMarioLeft()
    {
        if (IsPaused || currentMario.WinState) return;
        currentMario.Direction = false;
        currentMario.StopMove();
    }
    public void Damage()
    {
        if (IsPaused || currentMario.WinState || IsGameOver)
            return;

        if (!canTakeDamage)
            return;

        canTakeDamage = false;
        cooldownTimer = cooldownForDamage;


        if (currentMarioNum == 2)
        {
            SetMario(1);
            return;

        }
        else if (currentMarioNum == 1)
        {
            SetMario(0);
            return;
        }


        else if (currentMarioNum == 0)
        {
            livesCount--;

            if (livesCount <= 0)
            {
                TriggerGameOver();
                return;
            }
            else
            {
                ResetAfterDeath();
            }

            ResetMarioPosition();
        }
    }

    private void ResetMarioPosition()
    {
        Vector2 spawn = maps[currentLevel].getSpawn();

        currentMario = new SmallMario(smallMarioTexture, spawn, Content, this);
        currentMarioNum = 0;
    }
    public void PauseGame()
    {
        IsPaused = true;
        Music.PauseMusic();
    }
    public void UnpauseGame()
    {
        IsPaused = false;
        Music.ResumeMusic();
    }

    public void toggleMap(int roomNumber)
    {
        prevX = 0;
        currentLevel = roomNumber;
        if (roomNumber >= maps.Count)
        {
            currentLevel = 0;
        }
        unspawnedEnemies = levelEnemies[currentLevel];
        currentItems.Clear();
        projectiles.Clear();
        maps.Clear();
        LoadMaps();
        map = maps[currentLevel];
        // update function handles it from here.
    }

    public void spawnMarioAt(Vector2 pos)
    {
        currentMario.location = pos;
    }

    public void Reset()
    {
        hurryupPlayed = false;
        IsGameOver = false;
        IsPaused = false;
        Initialize();
    }
    private void ResetAfterDeath()
    {
        Vector2 spawn = new Vector2(300, 664);

        currentMario = new SmallMario(smallMarioTexture, spawn, Content, this);
        currentMarioNum = 0;
        gameTimer = 400;


        prevX = 0;

        camera.Position = new Vector2(0, camera.Position.Y);

        canTakeDamage = true;
        cooldownTimer = 0f;

        Music.StopMusic();
        Music.deathSound.Play();

        enemies.Clear();
        currentItems.Clear();
        projectiles.Clear();
        LoadMaps();
    }
    public void play()
    {
        Music.PlayBackground();
    }

}