using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace SoundManager;

public class Music
{
    public static  SoundEffect itemSound, blockSound, jumpSmallSound,jumpBigSound,coinSound,deathSound,gameOver,oneupSound,fireballSound,flagpoleSound,pauseSound;
    public static  Song background; 

    public static void LoadContent(ContentManager content)
    {
        background = content.Load<Song>("Music/Background");
        itemSound = content.Load<SoundEffect>("Music/item");
        blockSound = content.Load<SoundEffect>("Music/bump");
        jumpBigSound = content.Load<SoundEffect>("Music/jump");
        jumpSmallSound = content.Load<SoundEffect>("Music/jumpsmall");
        coinSound = content.Load<SoundEffect>("Music/coin");
        deathSound = content.Load<SoundEffect>("Music/death");
        gameOver = content.Load<SoundEffect>("Music/gameover");
        oneupSound = content.Load<SoundEffect>("Music/1up");
        flagpoleSound  = content.Load<SoundEffect>("Music/flagpole");
        fireballSound = content.Load<SoundEffect>("Music/fireball");
        pauseSound = content.Load<SoundEffect>("Music/pause");

    }
}