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

private TextureAtlas blocksTexture,bigBlockTexture,bigBlockTexturePt2,itemTexture, smallMarioTexture, bigMarioTexture;
private TextureRegion ground,smallTube,castle,flagStill,mushroom,mediumTube, standingSmallMario, standingBigMario;

private AnimatedSprite questionBlock,questionBlockHit,flower,coin,star,flagMove, rightSmallMario, rightBigMario;

private List<IController> controllers;
private List<ISprite> blocks, items;
private List<IMario> marios;

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
         };

    itemTexture = TextureAtlas.FromFile(Content, "images/items-definition.xml");
    flower = itemTexture.CreateAnimatedSprite("flower");
    coin = itemTexture.CreateAnimatedSprite("coin");
    star = itemTexture.CreateAnimatedSprite("star");
    mushroom = itemTexture.GetRegion("mushroom");

    items = new List<ISprite>
    {
        new Flower(flower),
        new Coin(coin),
        new Star(star),
        new Mushroom(mushroom)
    };

    smallMarioTexture = TextureAtlas.FromFile(Content,"images/SmallMario-definition.xml");
    standingSmallMario = smallMarioTexture.GetRegion("standingSmallMario");
    rightSmallMario = smallMarioTexture.CreateAnimatedSprite("smallRightMove");
    bigMarioTexture = TextureAtlas.FromFile(Content, "images/BigMario-definition.xml");
    standingBigMario = bigMarioTexture.GetRegion("standingBigMario");
    rightBigMario = bigMarioTexture.CreateAnimatedSprite("bigRightMove");

    marios = new List<IMario>
    {
        new SmallMario(standingSmallMario),
        new BigMario(standingBigMario),
        new SmallMario(rightSmallMario),
        new BigMario(rightBigMario)
    };

       currentBlockCount=0;
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
        currentMario = marios[marioNumber];
    }
    public void MarioJump()
    {
        currentMario.Jump();
    }
    public void MarioRight()
    {
        int moveRightMario = 0;
        if(currentMario.Equals(marios[0]) || currentMario.Equals(marios[2]))
        {
            moveRightMario = 2;
        } 
        else if(currentMario.Equals(marios[1]) || currentMario.Equals(marios[3]))
        {
            moveRightMario = 3;
        }
        SetMario(moveRightMario);
        currentMario.MoveRight();
    }
    public void StopMarioRight()
    {
        int moveRightMario = 0;
        if(currentMario.Equals(marios[0]) || currentMario.Equals(marios[2]))
        {
            moveRightMario = 0;
            currentMario.position = new Vector2(300, 664);
        } 
        else if(currentMario.Equals(marios[1]) || currentMario.Equals(marios[3]))
        {
            moveRightMario = 1;
            currentMario.position = new Vector2(300, 600);
        }
        SetMario(moveRightMario);
    }
}
