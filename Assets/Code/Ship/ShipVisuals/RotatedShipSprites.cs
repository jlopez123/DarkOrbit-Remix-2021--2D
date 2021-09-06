using UnityEngine;

public class RotatedShipSprites : IShipVisual
{
    private readonly SpriteRenderer _spriteRenderer;
    private readonly Sprite[] _sprites;
    private readonly int _spritesCount;
    public RotatedShipSprites(SpriteRenderer spriteRenderer, Sprite[] sprites, bool flipX)
    {
        _spriteRenderer = spriteRenderer;
        _sprites = sprites;
        _spriteRenderer.flipX = flipX;

        _spritesCount = sprites.Length;
        UpdateSprite(0f);
    }
    public void UpdateSprite(float angle)
    {
        float degPerState = 360f / (_spritesCount - 1);
        var currSpriteIndex = (int)(angle / degPerState);
        _spriteRenderer.sprite = _sprites[currSpriteIndex];
    }
    public void Hide()
    {
        _spriteRenderer.enabled = false;
    }
}
