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
using playerItemCollision;
using EnemyPlayerCollision;
using FireballCollisions;
using SoundManager;



namespace SprintZero;

public class Game1 : Core
{

    private TextureAtlas blocksTexture, bigBlockTexture, bigBlockTexturePt2, itemTexture, smallMarioTexture, bigMarioTexture, fireMarioTexture, projectileTexture, goombaTexture;
    private TextureRegion ground, smallTube, castle, mushroom, mediumTube, oneup_mushroom;

    private AnimatedSprite questionBlockHit, flower, coin, star, flagMove, aboveGroundBreak, fireballRolling, fireballPop;

   private playerItemCollisions playerItemCollision;
   private FireballCollision fireballCollision;
    private Song backgroundMusic; 

    private List<IController> controllers;
    private List<ICollectable> items;
    private List<IBlock> blocks;
    private List<IProjectile> projectiles;
    private List<IMario> marios;
    private List<IEnemy> enemies;
    private ICollectable currentItem;
    private IBlock currentBlock;
    private IMario currentMario;
    private IEnemy currentEnemy;

    private List<TileMap> maps; // Temporary!
    private TileMap map; // Current map.

    private int currentBlockCount, currentItemCount, currentMarioNum, currentEnemyCount, currentLevel;
    private Rectangle Bounds;
    private OrthographicCamera camera;
    private float prevX;

    public Game1() : base("SMB1", 1920, 1080, false) { }
    protected override void Initialize()
    {
        controllers = new List<IController>
        {
            new KeyController(this),
            new MouseController(this)
        };

        Bounds = new Rectangle(0, 0, 1920, 1080);

        maps = new List<TileMap>();

        base.Initialize();
        // Create camera with viewport adapter
        var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 1600, 960);
        camera = new OrthographicCamera(viewportAdapter);
        playerItemCollision = new playerItemCollisions();

        fireballCollision = new FireballCollision(enemies,currentEnemyCount,currentEnemy,blocks);
    }
    protected override void LoadContent()
    {
        blocksTexture = TextureAtlas.FromFile(Content, "images/block-definition.xml");
        ground = blocksTexture.GetRegion("ground");
        questionBlockHit = blocksTexture.CreateAnimatedSprite("hit-Question");
        bigBlockTexture = TextureAtlas.FromFile(Content, "images/bigblock-definition.xml");
        aboveGroundBreak = blocksTexture.CreateAnimatedSprite("aboveGroundBreak");
        smallTube = bigBlockTexture.GetRegion("tube");
        castle = bigBlockTexture.GetRegion("castle");
        bigBlockTexturePt2 = TextureAtlas.FromFile(Content, "images/BigBlocks2-definition.xml");
        mediumTube = bigBlockTexturePt2.GetRegion("mediumTube");
        flagMove = bigBlockTexturePt2.CreateAnimatedSprite("flagMove");
      
      Music.LoadContent(Content);

        Vector2 blockPos = new Vector2(600, 500);

        blocks = new List<IBlock>
         {
         //  new ground(ground,Content), 
       new questionMarkHit(questionBlockHit, blockPos) 
        //   new smallTube(smallTube),  
       //       new CastleBlock(castle), 
     //    new FlagMove(flagMove),
    //    new MediumTube(mediumTube),
    //     new AboveGroundBreak(aboveGroundBreak) 
         };

        itemTexture = TextureAtlas.FromFile(Content, "images/items-definition.xml");
        flower = itemTexture.CreateAnimatedSprite("flower");
        coin = itemTexture.CreateAnimatedSprite("coin");
        star = itemTexture.CreateAnimatedSprite("star");
        mushroom = itemTexture.GetRegion("mushroom");
        oneup_mushroom = itemTexture.GetRegion("one_up");

        //Projectiles
        projectileTexture = TextureAtlas.FromFile(Content, "images/Fireball-definition.xml");
        fireballRolling = projectileTexture.CreateAnimatedSprite("FireballRolling");
        fireballPop = projectileTexture.CreateAnimatedSprite("FireballPop");

        // fireballs are dynamic objects, don't exist at load time. They are created when the player presses the fire button.
        // Fireballs will be added to the list as the user presses the shoot button.
        projectiles = new List<IProjectile>();

        items = new List<ICollectable>
    {
        new Flower(flower),
        new Coin(coin),
        new Star(star),
        new Mushroom(mushroom),
        new OneUp(oneup_mushroom)
    };

        // Small Mario
        smallMarioTexture = TextureAtlas.FromFile(Content, "images/SmallMario-definition.xml");

        // Big Mario
        bigMarioTexture = TextureAtlas.FromFile(Content, "images/BigMario-definition.xml");

        // Fire Mario
        fireMarioTexture = TextureAtlas.FromFile(Content, "Images/FireMario-definition.xml"); // or "images/..." depending on your output folder

        marios = new List<IMario>
    {
        new SmallMario(smallMarioTexture,Content,this),
        new BigMario(bigMarioTexture,Content),
        new FireMario(fireMarioTexture,Content)
    };

        goombaTexture = TextureAtlas.FromFile(Content, "images/goomba-definition.xml");


        Koopa.LoadTextures(Content);

        enemies = new List<IEnemy>

        {
            new Goomba(goombaTexture),
            new Koopa(),
            new Koopa(Koopa.KoopaType.Red),
            new Koopa(Koopa.KoopaType.Blue)
        };

        currentBlockCount = 0;
        currentItemCount = 0;
        currentBlock = blocks[currentBlockCount];
        currentItem = items[currentItemCount];
        currentMario = marios[0];
        currentMarioNum = 0;
        currentEnemyCount = 0;
        currentEnemy = enemies[currentEnemyCount];
        prevX = 0;

        currentLevel = 0;
        TextureAtlas blockTextures = TextureAtlas.FromFile(Content, "images/block-definition.xml");
        TileMap map1 = new TileMap();
        ILevel level = new LevelOne(Content, blockTextures, "LevelData/LevelOne.xml");
        level.FromFile(map1);
        maps.Add(map1);
        TileMap mapBonus = new TileMap();
        level = new LevelOneBonus(Content, blockTextures, "LevelData/LevelOneBonus.xml");
        level.FromFile(mapBonus);
        maps.Add(mapBonus);
      //   Music.music(Content);
        //Can move this into the same class as our map if wanted to or just leave it here
        backgroundMusic = Content.Load<Song>("Music/Background");
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Volume = 0.5f;
        MediaPlayer.Play(backgroundMusic);

        base.LoadContent();
   
    }

    protected override void Update(GameTime gameTime)
    {
        foreach (IController controller in controllers)
        {
            controller.Update(gameTime);
        }

        map = maps[currentLevel];


        currentMario.Update(gameTime);


        currentBlock.Update(gameTime);
        currentItem.Update(gameTime);

        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            projectiles[i].Update(gameTime);
            if (projectiles[i] is Fireball fb)
            {
                if (!fb.IsActive)
                {
                    projectiles.RemoveAt(i);
                    continue;
                }
                fireballCollision.collisionCheck(fb);
            }
        }

        currentEnemy.Update(gameTime);
        CheckEnemyCollisions.CheckEnemyBlockCollisions(currentEnemy, blocks, map);

        //playerBlockCollision.checkBlockCollision(currentMario, blocks);
        List<IBlock> collidableBlocks = map.getBlocksInRectangle(currentMario.MarioCollider, 96);

        foreach (IBlock b in blocks) { // these extra blocks should be fit into TileMap somehow.
            collidableBlocks.Add(b);
        }

        playerBlockCollision.checkBlockCollision(
            currentMario,
            collidableBlocks
        ); // We should only call this method once per update.

        playerItemCollision.CheckCollisions(currentMario,currentItem,currentItemCount,currentMarioNum,SetMario);
        CheckEnemyCollisions.CheckEnemyMarioCollisions(currentEnemy,currentMario,Damage);

        // Camera
        if(currentMario.location.X > prevX)
        {
            camera.Position = new Vector2(currentMario.location.X - 560f, camera.Position.Y);
            prevX = currentMario.location.X;
        }


        var visibleArea = camera.BoundingRectangle;
        Rectangle cameraRect = new Rectangle(
            (int)visibleArea.Left,
            (int)visibleArea.Top,
            (int)visibleArea.Width,
            (int)visibleArea.Height
        );
        map.Update(gameTime, cameraRect, 64);

        foreach (var item in items)
        {
            if (item is Coin coin)
            {
                coin.CheckCollisions(currentMario);
            }
            else if (item is Flower flower)
            {
                flower.CheckCollisions(currentMario);
            }
        }
        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {

        GraphicsDevice.Clear(Color.CornflowerBlue);
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetViewMatrix());
        currentBlock.Draw(SpriteBatch);
        currentItem.Draw(SpriteBatch);
        currentMario.Draw(SpriteBatch);
        foreach (var p in projectiles)
            p.Draw(SpriteBatch);
        currentEnemy.Draw(SpriteBatch);
        var visibleArea = camera.BoundingRectangle;
        Rectangle cameraRect = new Rectangle(
            (int)visibleArea.Left,
            (int)visibleArea.Top,
            (int)visibleArea.Width,
            (int)visibleArea.Height
        );
        map.Draw(SpriteBatch, cameraRect, 64);
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
    public void NextBlock()
    {
        currentBlockCount = (currentBlockCount + 1) % blocks.Count;
        currentBlock = blocks[currentBlockCount];
    }
    public void PreviousBlock()
    {
        currentBlockCount--;
        if (currentBlockCount < 0)
        {
            currentBlockCount = blocks.Count - 1;
        }
        currentBlock = blocks[currentBlockCount];
    }
    public void NextItem()
    {
        currentItemCount = (currentItemCount + 1) % items.Count;
        currentItem = items[currentItemCount];
    }
    public void PreviousItem()
    {
        currentItemCount--;
        if (currentItemCount < 0)
        {
            currentItemCount = items.Count - 1;
        }
        currentItem = items[currentItemCount];
    }

    public void nextEnemy()
    {
        currentEnemyCount = (currentEnemyCount + 1) % enemies.Count;
        enemies[currentEnemyCount].Dead = false;
        currentEnemy = enemies[currentEnemyCount];
    }
    public void previousEnemy()
    {
        currentEnemyCount--;
        if (currentEnemyCount < 0)
        {
            currentEnemyCount = enemies.Count - 1;
        }
        enemies[currentEnemyCount].Dead = false;
        currentEnemy = enemies[currentEnemyCount];
    }
    public void SetMario(int marioNumber)
    {
        Vector2 currentPosition = currentMario.location;

        if (marioNumber == 0)
        {
            if (currentMarioNum > 0) currentPosition = new Vector2(currentPosition.X, currentPosition.Y + 64f);
            currentMario = new SmallMario(smallMarioTexture, currentPosition,Content,this);
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
            currentMario = new FireMario(fireMarioTexture, currentPosition,Content);
            currentMarioNum = marioNumber;
            currentMario.yVelocity = velocity;
            currentMario.Falling = true; 
        }
    
    }
    public void MarioJump()
    {
        currentMario.Jump(); 
    }
    public void MarioCrouch()
    {
        currentMario.Crouching = true;
        currentMario.Crouch();
    }
    public void MarioUncrouch()
    {
        currentMario.Crouching = false;
        currentMario.Crouch();
    }
    public void MarioFire()
    {
        if (currentMarioNum == 2)
        {
            currentMario.Fireball();
            SpawnFireball();
        }
    }
    public void MarioRight()
    {
        currentMario.Direction = true;
        currentMario.Move();
    }
    public void MarioLeft()
    {
        currentMario.Direction = false;
        currentMario.Move();
    }
    public void StopMarioRight()
    {
        currentMario.Direction = true;
        currentMario.StopMove();
    }
    public void StopMarioLeft()
    {
        currentMario.Direction = false;
        currentMario.StopMove();
    }
    public void Damage()
    {
        if (currentMarioNum == 0)
        {
            currentMario.Damage();
        }
        else if (currentMarioNum == 1)
        {
            SetMario(0);
        }
        else if (currentMarioNum == 2)
        {
            SetMario(1);
        }
    }

    public void toggleMap(int roomNumber)
    {
        currentLevel = roomNumber;
        if (roomNumber >= maps.Count)
        {
            currentLevel = 0;
        }
        // update function handles it from here.
    }

    public void Reset()
    {
         
        Initialize();
    }
 
    public void play()
    {
        MediaPlayer.Play(backgroundMusic);
    }
}