using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using SprintZero.Controllers;
using SpriteZero.Enemies;
using SpriteZero.Marios;
using SpriteZero.Sprites;
using SpriteZero.blocks;
using SprintZero.PBCollision;

namespace SprintZero;

public class Game1 : Core
{

    private TextureAtlas blocksTexture, bigBlockTexture, bigBlockTexturePt2, itemTexture, smallMarioTexture, bigMarioTexture, fireMarioTexture, projectileTexture, goombaTexture;
    private TextureRegion ground, smallTube, castle, mushroom, mediumTube, oneup_mushroom;

    private AnimatedSprite  questionBlockHit, flower, coin, star, flagMove, aboveGroundBreak, fireballRolling, fireballPop;

    private List<IController> controllers;
    private List<ISprite>  items;
    private List<IBlock> blocks;
    private List<IProjectile> projectiles;
    private List<IMario> marios;
    private List<IEnemy> enemies;

    private ISprite  currentItem;
    private IBlock currentBlock;
    private IMario currentMario;
    private IEnemy currentEnemy;

    private int currentBlockCount, currentItemCount, currentMarioNum, currentEnemyCount;
    private Rectangle Bounds;

    public Game1() : base("SMB1", 1920, 1080, false) { }
    protected override void Initialize()
    {
        controllers = new List<IController>
        {
            new KeyController(this)
        };

        Bounds = new Rectangle(0, 0, 1920, 1080);

        base.Initialize();
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




        blocks = new List<IBlock>
         {
         //   new Ground(ground),
           // new questionMarkHit(questionBlockHit),
           // new smallTube(smallTube),
           // new CastleBlock(castle),
           // new FlagMove(flagMove),
            new MediumTube(mediumTube),
           // new AboveGroundBreak(aboveGroundBreak)

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
        smallMarioTexture = TextureAtlas.FromFile(Content, "images/SmallMario-definition.xml");

        // Big Mario
        bigMarioTexture = TextureAtlas.FromFile(Content, "images/BigMario-definition.xml");

        // Fire Mario
        fireMarioTexture = TextureAtlas.FromFile(Content, "Images/FireMario-definition.xml"); // or "images/..." depending on your output folder

        marios = new List<IMario>
    {
        new SmallMario(smallMarioTexture),
        new BigMario(bigMarioTexture),
        new FireMario(fireMarioTexture)
    };

        goombaTexture = TextureAtlas.FromFile(Content, "images/goomba-definition.xml");


        Koopa.loadTextures(Content);

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
            if (projectiles[i] is Fireball fb) {
                if(!fb.IsActive) {  
                    projectiles.RemoveAt(i);
                    continue;
                }
                collisionCheck(fb);   
            }
        }
        currentEnemy.Update(gameTime);
        CheckBounds();
        CheckCollisions();
        CheckEnemyCollisions();
        playerBlockCollision.checkBlockCollision(currentMario,blocks);
        base.Update(gameTime);
    }

    public void CheckBounds()
    {
        if(Bounds.Left >= currentMario.MarioCollider.Left)
        {
            currentMario.position = new Vector2(currentMario.position.X + 4f, currentMario.position.Y);
        }
        else if(Bounds.Right <= currentMario.MarioCollider.Right)
        {
            currentMario.position = new Vector2(currentMario.position.X - 4f, currentMario.position.Y);
        }
        if(Bounds.Left >= currentItem.Collider.Left)
        {
            currentItem.location = new Vector2(currentItem.location.X + 2f, currentItem.location.Y);
        }
        else if(Bounds.Right <= currentItem.Collider.Right)
        {
            currentItem.location = new Vector2(currentItem.location.X - 2f, currentItem.location.Y);
        }
    }

    public void CheckCollisions()
    {
        if((currentItem.Collider.Intersects(currentMario.MarioCollider)))
        {
            // Checks to see if item is a fire flower and if mario is small or big
            if(currentItemCount == 0 && currentMarioNum <= 1)
            {
                SetMario(2);
            }
            // Checks to see if the item is a mushroom and if mario is small
            else if(currentItemCount == 3 && currentMarioNum == 0)
            {
                SetMario(1);
            }
        }
    }

    public void CheckEnemyCollisions()
    {
       if (currentEnemy.EnemyCollider.Intersects(currentMario.MarioCollider) && !currentEnemy.Dead)
       {
            if (currentMario.Jumping && currentMario.MarioCollider.Bottom <= currentEnemy.EnemyCollider.Center.Y + 10)
            {
                currentEnemy.Dead = true;
            }
            else
            {
                Damage();
            }
        }
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
        currentEnemy = enemies[currentEnemyCount];
    }
    public void previousEnemy()
    {
        currentEnemyCount--;
        if (currentEnemyCount < 0)
        {
            currentEnemyCount = enemies.Count - 1;
        }
        currentEnemy = enemies[currentEnemyCount];
    }
    public void SetMario(int marioNumber)
    {
        Vector2 currentPosition = currentMario.position;
        if (marioNumber == 0)
        {
            if(currentMarioNum > 0) currentPosition = new Vector2(currentPosition.X, currentPosition.Y + 64f);
            currentMario = new SmallMario(smallMarioTexture, currentPosition);
            currentMarioNum = marioNumber;
        }
        else if (marioNumber == 1)
        {
            if(currentMarioNum == 0) currentPosition = new Vector2(currentPosition.X, currentPosition.Y - 64f);
            currentMario = new BigMario(bigMarioTexture, currentPosition);
            currentMarioNum = marioNumber;
        }
        else if (marioNumber == 2)
        {
            if(currentMarioNum == 0) currentPosition = new Vector2(currentPosition.X, currentPosition.Y - 64f);
            currentMario = new FireMario(fireMarioTexture, currentPosition);
            currentMarioNum = marioNumber;
        }
    }
    public void MarioJump()
    {
        currentMario.Jump();
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

    public void collisionCheck(Fireball fb)
    {
        for (int j = enemies.Count-1; j >= 0; j--)
        {
            switch (enemies[j])
            {
                case Goomba:
                    if (fb.location.X < enemies[j].position.X + 16 &&
                        fb.location.X + 8 > enemies[j].position.X &&
                        fb.location.Y < enemies[j].position.Y + 16 &&
                        fb.location.Y + 8 > enemies[j].position.Y)
                    {
                        // Goomba take damage
                        enemies[j].Dead = true;
                        fb.Pop();
                    }
                    break;
                case Koopa:
                    if (fb.location.X < enemies[j].position.X + 16 &&
                        fb.location.X + 8 > enemies[j].position.X &&
                        fb.location.Y < enemies[j].position.Y + 24 &&
                        fb.location.Y + 8 > enemies[j].position.Y)
                    {
                        // Koopa take damage
                        enemies[j].Dead = true;
                        fb.Pop();
                    }
                    break;
            }
        }
    }

    public void Reset()
    {
        Initialize();
    }
}
