using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using SprintZero.Controllers;
using SpriteZero.Marios;
using SpriteZero.Sprites;

namespace SprintZero;

 public class Game1 : Core
{   

private TextureAtlas blocksTexture,bigBlockTexture,bigBlockTexturePt2,itemTexture, smallMarioTexture, bigMarioTexture, goombaTexture;
private TextureRegion ground,smallTube,castle,flagStill,mushroom,mediumTube, oneup_mushroom;
private TextureRegion goombaRight1, goombaLeft1, goombaFlat1;


private AnimatedSprite questionBlock,questionBlockHit,flower,coin,star,flagMove,aboveGroundBreak;
private AnimatedSprite goombaWalk1, goombaHit1;

    private List<IController> controllers;
private List<ISprite> blocks, items;
private List<IMario> marios;
private List<ISprite> enemies;

private ISprite currentBlock,currentItem;
private IMario currentMario;

private int currentBlockCount, currentItemCount;

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

    items = new List<ISprite>
    {
        new Flower(flower),
        new Coin(coin),
        new Star(star),
        new Mushroom(mushroom),
        new OneUp(oneup_mushroom)
    };

    smallMarioTexture = TextureAtlas.FromFile(Content,"images/SmallMario-definition.xml");
    bigMarioTexture = TextureAtlas.FromFile(Content, "images/BigMario-definition.xml");

    marios = new List<IMario>
    {
        new SmallMario(smallMarioTexture),
        new BigMario(bigMarioTexture)
    };

    goombaTexture = TextureAtlas.FromFile(Content, "images/goomba-definition.xml");
    goombaRight1 = goombaTexture.GetRegion("goombaRight1");
    goombaLeft1 = goombaTexture.GetRegion("goombaLeft1");
    goombaFlat1 = goombaTexture.GetRegion("goombaFlat1");
    goombaWalk1 = goombaTexture.CreateAnimatedSprite("goombaWalk1");
    goombaHit1 = goombaTexture.CreateAnimatedSprite("goombaHit1");

        enemies = new List<ISprite>
        {
            //TODO: add goomba sprites
        };

        currentBlockCount =0;
       currentItemCount=0;
       currentBlock = blocks[currentBlockCount];
       currentItem = items[currentItemCount];
       currentMario = marios[0];
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
        base.Update(gameTime);
    }
    

    protected override void Draw(GameTime gameTime)
    {
 
        GraphicsDevice.Clear(Color.CornflowerBlue);
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
         currentBlock.Draw(SpriteBatch);
         currentItem.Draw(SpriteBatch);
         currentMario.Draw(SpriteBatch);
        SpriteBatch.End();
        base.Draw(gameTime);
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
    public void SetMario(int marioNumber)
    {
        currentMario =  new marios[marioNumber];
    }
    public void MarioJump()
    {
        currentMario.Jump();
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
        if(currentMario.Equals(marios[0]))
        {
            currentMario.Damage();
        }
    }
    public void Reset()
    {
        Initialize();
    }
}
 