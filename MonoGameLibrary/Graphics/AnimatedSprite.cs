using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Graphics;

public class AnimatedSprite : Sprite 
{
private int _currentFrame;
private TimeSpan _elapsed;
private Animation _animation;
private bool _isPaused =false;


/// <summary>
/// Gets or Sets the animation for this animated sprite.
/// </summary>
public Animation Animation
{
    get => _animation;
    set
    {
        _animation = value;
        _currentFrame = 0;
        _elapsed = TimeSpan.Zero;
        Region = _animation.Frames[0];
    }
}
/// <summary>
/// Creates a new animated sprite.
/// </summary>
public AnimatedSprite() { }

/// <summary>
/// Creates a new animated sprite with the specified frames and delay.
/// </summary>
/// <param name="animation">The animation for this animated sprite.</param>
public AnimatedSprite(Animation animation)
{
    Animation = animation;
}
/// <summary>
/// Updates this animated sprite.
/// </summary>
/// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
public void Update(GameTime gameTime)
{
    if (_isPaused || _animation == null)
        {
            return;
        }
    _elapsed += gameTime.ElapsedGameTime;

    if (_elapsed >= _animation.Delay)
    {
        _elapsed -= _animation.Delay;
        _currentFrame++;

        if (_currentFrame >= _animation.Frames.Count)
        {
            _currentFrame = 0;
        }

        Region = _animation.Frames[_currentFrame];
    }
}
///<summary>
/// Stops the animation and resets it
/// </summary>
 public void Stop()
    {
        _isPaused = true;
        _currentFrame = 0;
        _elapsed = TimeSpan.Zero;
        if (_animation != null)
        {
            Region = _animation.Frames[_currentFrame];
        }
    }
    
///<summary>
/// Pauses at the current frame
/// </summary>
public void Pause()
        {
            _isPaused = true;
        }
///<summary>
/// Pauses on frame
/// </summary>
public void PauseFrame(int frameIndex)
    {
        if (_animation == null)
        {
            return;
        }
        _currentFrame = Math.Clamp(frameIndex,0,_animation.Frames.Count-1);
        Region = _animation.Frames[_currentFrame];
        _isPaused = true;
    }
///<summary>
/// Unpauses
/// </summary>
public void Play()
    {
        _isPaused = false;
    }
    }


