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
//Initialization 
private TextureAtlas atlas;
private TextureRegion nonMove;
private TextureRegion moveNonAnimated;
private AnimatedSprite walkingMove;
private AnimatedSprite nonMoveAnimated;
private SpriteFont font;

private List<IController> controllers;
private List<ISprite> sprites;
private ISprite currentSprite;



public Game1() : base("Sprint Zero",1280,720,false){}
    protected override void Initialize()
    {
        controllers = new List<IController>
        {
            new KeyController(this),new MouseController(this)
        };

        base.Initialize();
    }
    protected override void LoadContent()
    {
       //this is for the font
       font = Content.Load<SpriteFont>("Font/File");
        //Getting all the different sprites
        atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        walkingMove = atlas.CreateAnimatedSprite("walking-animation");
        nonMoveAnimated = atlas.CreateAnimatedSprite("JumpAnimated");
        walkingMove.Scale = new Vector2(4.0f);
        nonMoveAnimated.Scale = new Vector2(4.0f);
        nonMove = atlas.GetRegion("Nonmove");
        moveNonAnimated = atlas.GetRegion("swimMoveNonAnimated");
        //Making a list of the sprites
         sprites = new List<ISprite>
         {
             new notMoving(nonMove),
             new animatedButNonMoving(nonMoveAnimated),
             new upAndDownS(moveNonAnimated),
             new LRAnimated(walkingMove)
         };
    
         currentSprite = sprites[0];
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

        string credits = ("Credits");
        Vector2 locationC = new Vector2(300,400);
        Color theColor = Color.Black;
        SpriteBatch.DrawString(font,credits,locationC,theColor);
   
        string programming = ("Programmed by Madison Gysan");
        Vector2 locationP = new Vector2(300,430);
        SpriteBatch.DrawString(font,programming,locationP,theColor);
     
        string spriteC = ("Sprites from: https://www.spriters-resource.com/pc_computer/omori/asset/155739/");
        Vector2 locationSC = new Vector2(200,460);
        SpriteBatch.DrawString(font,spriteC,locationSC,theColor);
        SpriteBatch.End();
        base.Draw(gameTime);
    }

 public void SetSprite(int i)
    {
        if (i >= 0 && i < sprites.Count)
        currentSprite = sprites[i];
    }

}
