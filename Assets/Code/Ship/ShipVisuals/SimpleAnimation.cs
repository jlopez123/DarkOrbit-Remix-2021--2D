using System;
using System.Threading.Tasks;
using UnityEngine;

public class SimpleAnimation : IShipVisual
{
    private readonly SpriteRenderer _spriteRenderer;
    private readonly Sprite[] _sprites;
    private readonly float _fps;
    //private readonly bool _scaledTime;
    //
    private bool _loop = true;

    private int _currentSprite;
    private float _timeInterval;

    private bool _animationRunning;
    public SimpleAnimation(SpriteRenderer spriteRenderer, Sprite[] sprites, float fps, bool flipX)
    {
        _spriteRenderer = spriteRenderer;
        _sprites = sprites;
        _fps = fps;
        _spriteRenderer.flipX = flipX;

        _currentSprite = 0;
        _timeInterval = 1f / fps;

        _animationRunning = true;
        DoAnimation();        
    }
    private async void DoAnimation()
    {
        while(_animationRunning && _spriteRenderer != null)
        {
            _spriteRenderer.sprite = _sprites[_currentSprite];

            _currentSprite++;

            if(_currentSprite == _sprites.Length)
                _currentSprite = 0;

            await Task.Delay(TimeSpan.FromSeconds(_timeInterval));
            //yield return new WaitForSeconds(_timeInterval);
        }
    }
    public void UpdateSprite(float angle)
    {

    }
    public void Hide()
    {
        _animationRunning = false;
        _spriteRenderer.enabled = false;
    }

    //private void ChangeSprites(Sprite[] sprites)
}