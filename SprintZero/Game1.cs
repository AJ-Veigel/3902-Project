using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using SprintZero.Controllers;
using SpriteZero.Enemies;
using SpriteZero.Marios;
using SpriteZero.Sprites;

namespace SprintZero;

 public class Game1 : Core
{   

private TextureAtlas blocksTexture,bigBlockTexture,bigBlockTexturePt2,itemTexture, smallMarioTexture, bigMarioTexture, fireMarioTexture, projectileTexture;
private TextureRegion ground,smallTube,castle,flagStill,mushroom,mediumTube, oneup_mushroom;

private AnimatedSprite questionBlock,questionBlockHit,flower,coin,star,flagMove,aboveGroundBreak, fireballRolling, fireballPop;
private AnimatedSprite rightSmallMario, leftSmallMario, swimmingRightSmallMario, swimmingLeftSmallMario, flagpoleRightSmallMario, flagpoleLeftSmallMario;
private AnimatedSprite rightBigMario, leftBigMario, swimmingRightBigMario, swimmingLeftBigMario, flagpoleRightBigMario, flagpoleLeftBigMario;

// ===========
// Fire Mario 
// ===========
private TextureRegion standingLeftFireMario, standingRightFireMario;
private TextureRegion jumpingLeftFireMario, jumpingRightFireMario;
private TextureRegion crouchLeftFireMario, crouchRightFireMario;
private TextureRegion leftFlameThrow, rightFlameThrow;

// Dash
private TextureRegion dashFireMarioLeft, dashFireMarioRight;

private AnimatedSprite rightFireMario, leftFireMario;
private AnimatedSprite swimmingRightFireMario, swimmingLeftFireMario;
private AnimatedSprite flagpoleRightFireMario, flagpoleLeftFireMario;

// throw 
private AnimatedSprite throwRightFireMario, throwLeftFireMario;



private List<IController> controllers;
private List<ISprite> blocks, items;
private List<IProjectile> projectiles;
private List<IMario> marios;
private List<IEnemy> enemies;

private ISprite currentBlock,currentItem;
private IMario currentMario;
private IEnemy currentEnemy;

    private int currentBlockCount, currentItemCount, currentMarioNum, currentEnemyCount;

public Game1() : base("SMB1",1920,1080,false){}
    protected override void Initialize()
    {
        controllers = new List<IController>
        {
            new KeyController(this)
        };

        base.Initialize();
    }
    protected override void LoadContent()
    {
        blocksTexture = TextureAtlas.FromFile(Content, "images/block-definition.xml");
        ground = blocksTexture.GetRegion("ground");
        questionBlock = blocksTexture.CreateAnimatedSprite("question-Block");
        questionBlockHit = blocksTexture.CreateAnimatedSprite("hit-Question");
        bigBlockTexture = TextureAtlas.FromFile(Content,"images/bigblock-definition.xml");
        aboveGroundBreak = blocksTexture.CreateAnimatedSprite("aboveGroundBreak");
        smallTube = bigBlockTexture.GetRegion("tube");
        castle = bigBlockTexture.GetRegion("castle"); 
        flagStill = bigBlockTexture.GetRegion("flag");  
        bigBlockTexturePt2 = TextureAtlas.FromFile(Content,"images/BigBlocks2-definition.xml");
        mediumTube = bigBlockTexturePt2.GetRegion("mediumTube");
        flagMove = bigBlockTexturePt2.CreateAnimatedSprite("flagMove");
       

        
                 
         blocks = new List<ISprite>
         {
            new Ground(ground),
            new QuestionBlock(questionBlock),
            new questionMarkHit(questionBlockHit),
            new smallTube(smallTube),
            new CastleBlock(castle), 
            new FlagStill(flagStill),
            new FlagMove(flagMove),
            new MediumTube(mediumTube),
            new AboveGroundBreak(aboveGroundBreak)

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

    items = new List<ISprite>
    {
        new Flower(flower),
        new Coin(coin),
        new Star(star),
        new Mushroom(mushroom),
        new OneUp(oneup_mushroom)
    };

    // Small Mario
    smallMarioTexture = TextureAtlas.FromFile(Content,"images/SmallMario-definition.xml");
    standingLeftSmallMario = smallMarioTexture.GetRegion("standingLeftSmallMario");
    standingRightSmallMario = smallMarioTexture.GetRegion("standingRightSmallMario");
    jumpingLeftSmallMario = smallMarioTexture.GetRegion("jumpingLeftSmallMario");
    jumpingRightSmallMario = smallMarioTexture.GetRegion("jumpingRightSmallMario");
    deathSmallMario = smallMarioTexture.GetRegion("deadMario");
    rightSmallMario = smallMarioTexture.CreateAnimatedSprite("smallRightMove");
    leftSmallMario = smallMarioTexture.CreateAnimatedSprite("smallLeftMove");
    swimmingRightSmallMario = smallMarioTexture.CreateAnimatedSprite("smallRightSwim");
    swimmingLeftSmallMario = smallMarioTexture.CreateAnimatedSprite("smallLeftSwim");
    flagpoleLeftSmallMario = smallMarioTexture.CreateAnimatedSprite("smallLeftFlag");
    flagpoleRightSmallMario = smallMarioTexture.CreateAnimatedSprite("smallRightFlag");

    // Big Mario
    bigMarioTexture = TextureAtlas.FromFile(Content, "images/BigMario-definition.xml");

    // Fire Mario
    fireMarioTexture = TextureAtlas.FromFile(Content, "Images/FireMario-definition.xml"); // or "images/..." depending on your output folder

    // Static regions
    standingLeftFireMario  = fireMarioTexture.GetRegion("standingLeftFireMario");
    standingRightFireMario = fireMarioTexture.GetRegion("standingRightFireMario");

    jumpingLeftFireMario   = fireMarioTexture.GetRegion("jumpingLeftFireMario");
    jumpingRightFireMario  = fireMarioTexture.GetRegion("jumpingRightFireMario");

    crouchLeftFireMario    = fireMarioTexture.GetRegion("crouchLeftFireMario");
    crouchRightFireMario   = fireMarioTexture.GetRegion("crouchRightFireMario");

    // Optional: access the raw throw pose regions (only if you want them)
    leftFlameThrow  = fireMarioTexture.GetRegion("leftFlameThrow");
    rightFlameThrow = fireMarioTexture.GetRegion("rightFlameThrow");

    // Optional: the two extra regions (keeps “all 32 accounted for”)
    dashFireMarioLeft  = fireMarioTexture.GetRegion("extraFireMarioLeft");
    dashFireMarioRight = fireMarioTexture.GetRegion("extraFireMarioRight");

    // Animated sprites
    rightFireMario         = fireMarioTexture.CreateAnimatedSprite("fireRightMove");
    leftFireMario          = fireMarioTexture.CreateAnimatedSprite("fireLeftMove");

    swimmingRightFireMario = fireMarioTexture.CreateAnimatedSprite("fireRightSwim");
    swimmingLeftFireMario  = fireMarioTexture.CreateAnimatedSprite("fireLeftSwim");

    flagpoleLeftFireMario  = fireMarioTexture.CreateAnimatedSprite("fireLeftFlag");
    flagpoleRightFireMario = fireMarioTexture.CreateAnimatedSprite("fireRightFlag");

    // Throw (1-frame “animations”)
    throwRightFireMario    = fireMarioTexture.CreateAnimatedSprite("fireThrowRight");
    throwLeftFireMario     = fireMarioTexture.CreateAnimatedSprite("fireThrowLeft");


    marios = new List<IMario>
    {
        new SmallMario(standingLeftSmallMario, standingRightSmallMario, jumpingLeftSmallMario, jumpingRightSmallMario, deathSmallMario, rightSmallMario, leftSmallMario, swimmingRightSmallMario, swimmingLeftSmallMario, flagpoleLeftSmallMario, flagpoleRightSmallMario),
        new BigMario(standingLeftBigMario, standingRightBigMario, jumpingLeftBigMario, jumpingRightBigMario, crouchLeftBigMario, crouchRightBigMario, rightBigMario, leftBigMario, swimmingRightBigMario, swimmingLeftBigMario, flagpoleLeftBigMario, flagpoleRightBigMario),
        new FireMario(
            standingLeftFireMario, standingRightFireMario,
            jumpingLeftFireMario, jumpingRightFireMario,
            crouchLeftFireMario, crouchRightFireMario,
            rightFireMario, leftFireMario,
            swimmingRightFireMario, swimmingLeftFireMario,
            flagpoleLeftFireMario, flagpoleRightFireMario,
            throwLeftFireMario, throwRightFireMario
        )
    };

    goombaTexture = TextureAtlas.FromFile(Content, "images/goomba-definition.xml");


        Koopa.loadTextures(Content);

        enemies = new List<IEnemy>

        {
            new Goomba(goombaTexture)
        };

        currentBlockCount =0;
        currentItemCount=0;
        currentBlock = blocks[currentBlockCount];
        currentItem = items[currentItemCount];
        currentMario = marios[0];
        currentMarioNum = 0;
        currentEnemyCount = 0;
        currentEnemy = enemies[currentEnemyCount];
        

        base.LoadContent();
    }
    
    protected override void Update(GameTime gameTime)
    {
        foreach (IController controller in controllers)
        {
            controller.Update(gameTime);
        }
         currentBlock.Update(gameTime);
         currentItem.Update(gameTime);
         currentMario.Update(gameTime);
         for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            projectiles[i].Update(gameTime);
            if (projectiles[i] is Fireball fb && !fb.IsActive)
                projectiles.RemoveAt(i);
        }
         currentEnemy.Update(gameTime);
        base.Update(gameTime);
    }
    

    protected override void Draw(GameTime gameTime)
    {
 
        GraphicsDevice.Clear(Color.CornflowerBlue);
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
         currentBlock.Draw(SpriteBatch);
         currentItem.Draw(SpriteBatch);
         currentMario.Draw(SpriteBatch);
         foreach (var p in projectiles)
            p.Draw(SpriteBatch);
         currentEnemy.Draw(SpriteBatch);
        SpriteBatch.End();
        base.Draw(gameTime);
    }

private void SpawnFireball()
{
    // 2 fireballs max
    if (projectiles.Count >= 2) return;

    Vector2 spawnPos = currentMario.position + new Vector2(currentMario.Direction ? 40f : -10f, 40f);

    // create new animated sprites for each fireball
    AnimatedSprite roll = projectileTexture.CreateAnimatedSprite("FireballRolling");
    AnimatedSprite pop  = projectileTexture.CreateAnimatedSprite("FireballPop");
    
    var s = new Vector2(4f, 4f);
    roll.Scale = s;
    pop.Scale = s;

    projectiles.Add(new Fireball(roll, pop, spawnPos, currentMario.Direction));
}
public void NextBlock()
    {
        currentBlockCount = (currentBlockCount+1)  % blocks.Count;
        currentBlock = blocks[currentBlockCount];
    }
public void PreviousBlock()
    {
        currentBlockCount--;
        if (currentBlockCount < 0)
        {
            currentBlockCount = blocks.Count -1;
        }
        currentBlock = blocks[currentBlockCount];
    }
public void NextItem()
    {
        currentItemCount = (currentItemCount+1)  % items.Count;
        currentItem = items[currentItemCount];
    }
public void PreviousItem()
    {
        currentItemCount--;
        if (currentItemCount < 0)
        {
            currentItemCount = items.Count -1;
        }
        currentItem = items[currentItemCount];
    }

 public void nextEnemy()
    {
        currentEnemyCount = (currentEnemyCount+1)  % enemies.Count;
        currentEnemy = enemies[currentEnemyCount];
    }
public void previousEnemy()
    {
        currentEnemyCount--;
        if (currentEnemyCount < 0)
        {
            currentEnemyCount = enemies.Count -1;
        }
        currentEnemy = enemies[currentEnemyCount];
    }
    public void SetMario(int marioNumber)
    {
        if(marioNumber == 0)
        {
            currentMario = new SmallMario(smallMarioTexture);
            currentMarioNum = marioNumber;
        }
        else if(marioNumber == 1)
        {
            currentMario = new BigMario(bigMarioTexture);
            currentMarioNum = marioNumber;
        }
    }
    public void MarioJump()
    {
        currentMario.Jump();
    }
    public void MarioFire()
    {
        if (currentMario.Equals(marios[2]))
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
        Vector2 previousMarioPos;
        if(currentMarioNum == 0)
        {
            currentMario.Damage();
        }
        else if(currentMarioNum == 1)
        {
            previousMarioPos = currentMario.position;
            SetMario(0);
            currentMario.position = new Vector2(previousMarioPos.X, previousMarioPos.Y + 64f);
            
        }
    }
    public void Reset()
    {
        Initialize();
    }
}
 