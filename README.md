This is the project for the 3902 CSE Project class at OSU!

Player controls:
 -'wasd' and arrow keys to move
 -Press numbers (1,2,3) to change Mario sprite
 -Press 'e' to see Mario damaged
 -Press up arrow or 'w' to jump
 -Press space to shoot a fireball as Fire Mario
To view different maps, left click in the top left corner of the screen.
To view the different items, press 'u' or 'i'
To view the different enemies, press 'o' or 'p'
If you wish to reset the game, press 'r'
If you wish to quit the game, press 'esc' 
If you wish to pause the background music, press 'm'
If you wish to unpause the background music, press 'n'

Code reference links:
    Code from the MonoGameLibrary is written using MonoGame Tutorals. The exact breakdown is below. \
    AnimatedSprite.cs -> https://docs.monogame.net/articles/tutorials/building_2d_games/09_the_animatedsprite_class/index.html &https://docs.monogame.net/articles/getting_to_know/howto/graphics/HowTo_Animate_Sprite.html \
    Animation.cs -> https://docs.monogame.net/articles/tutorials/building_2d_games/09_the_animatedsprite_class/index.html \
    ISprite.cs -> https://docs.monogame.net/articles/tutorials/building_2d_games/08_the_sprite_class/index.html  & https://docs.monogame.net/articles/tutorials/building_2d_games/07_optimizing_texture_rendering/index.html \
    TextureAtlas.cs -> https://docs.monogame.net/articles/tutorials/building_2d_games/07_optimizing_texture_rendering/index.html & https://docs.monogame.net/articles/tutorials/building_2d_games/09_the_animatedsprite_class/index.html \
    TextureRegion.cs -> https://docs.monogame.net/articles/tutorials/building_2d_games/08_the_sprite_class/index.html & https://docs.monogame.net/articles/tutorials/building_2d_games/07_optimizing_texture_rendering/index.html \
    GamePadInfo.cs -> https://docs.monogame.net/articles/tutorials/building_2d_games/11_input_management/index.html \
    IController.cs -> https://docs.monogame.net/articles/tutorials/building_2d_games/11_input_management/index.html \
    KeyboardInfo.cs -> https://docs.monogame.net/articles/tutorials/building_2d_games/11_input_management/index.html \
    MouseInfo.cs -> https://docs.monogame.net/articles/tutorials/building_2d_games/11_input_management/index.html & https://docs.monogame.net/api/Microsoft.Xna.Framework.Input.Mouse.html 
    
Sprite reference links:
        Sprites used for this game are from Super Mario Bros created by Nintendo. The sprite sheets are allocated from https://www.mariouniverse.com/sprites-nes-smb/. 

Music reference links:
    Music was created by Nintendo. The music and sounds are sourced from https://sounds.spriters-resource.com/nes/supermariobros/asset/393915/ && https://youtu.be/L4PxvY2gjP0?si=QSvVBBe5VsaafvKW (converted to WAV file by: https://media.ytmp3.gg/youtube-to-wav-converter)


This is currently a work in progress!
Known issues: 
   - During death animation, game will instantly restart rather than showing mario's death animation.
   - The current way blocks are drawn, mario will also have collision at the block's position if they are all uncommented. 
   - Fire Mario currently glides rather than walk. 

Currently being worked on:
    -Collision between blocks, items, and enemies.
    -Maps
    -Music
    -HUD layout
    -Game States
    -Camera