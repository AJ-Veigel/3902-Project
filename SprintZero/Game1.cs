using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using SprintZero.Controllers;
using SpriteZero.Sprites;

namespace SprintZero;

 public class Game1 : Core
{   

private TextureAtlas blocksTexture,bigBlockTexture;
private TextureRegion ground,tube,castle,flagStill;

private AnimatedSprite questionBlock,questionBlockHit,flagMove,coin,flower,star,mushroom;


private List<IController> controllers;
private List<ISprite> sprites;
private ISprite currentSprite;

private int currentIndex;


public Game1() : base("Sprint Zero",1280,720,false){}
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
        tube = bigBlockTexture.GetRegion("tube");
       // castle = bigBlockTexture.GetRegion("castle");


      
         sprites = new List<ISprite>
         {
            new Ground(ground),
            new QuestionBlock(questionBlock),
            new questionMarkHit(questionBlockHit),
            new TubeBlock(tube),
            
         };
        currentIndex =0;
         currentSprite = sprites[currentIndex];
        base.LoadContent();
    }
    
    protected override void Update(GameTime gameTime)
    {
        foreach (IController controller in controllers)
        {
            controller.Update(gameTime);
        }
         currentSprite.Update(gameTime);
         
        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {
 
        GraphicsDevice.Clear(Color.CornflowerBlue);
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
         currentSprite.Draw(SpriteBatch);
        SpriteBatch.End();
        base.Draw(gameTime);
    }

 public void NextSprite()
    {
        currentIndex = (currentIndex +1 ) % sprites.Count;
        currentSprite = sprites[currentIndex];
    }
public void previousSprite()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = sprites.Count -1;
        
        }
        currentSprite=sprites[currentIndex];
    }

}
