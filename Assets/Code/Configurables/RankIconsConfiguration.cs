using UnityEngine;

[CreateAssetMenu(fileName = "RankIconsConfiguration", menuName = "Configurables/Create Rank Icons Configuration", order = 0)]
public class RankIconsConfiguration : ScriptableObject
{
    [SerializeField]
    private Sprite[] _sprites;

    public Sprite GetRankSprite(int rank)
    {
        if (rank >= _sprites.Length || rank < 0)
            rank = 0;

        return _sprites[rank];
    }
}
