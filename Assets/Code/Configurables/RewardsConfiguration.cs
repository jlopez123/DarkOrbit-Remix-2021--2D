using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rewards Configuration", menuName = "Configurables/Rewards Configuration")]
public class RewardsConfiguration : ScriptableObject
{
    [SerializeField]
    private ShipRewards[] _shipsRewards;
    [SerializeField]
    private ShipRewards _defaultRwards;

    private Dictionary<string, ShipRewards> _shipIdToRewards;

    // Types : Boxes/Ships/Chests/Structures ...
    // id(String) -> Rewards
    //private Dictionary<TypeX, Dictionary<string, ShipRewards>> _idToRewards2;
    private void Awake()
    {
        CreateDictionary();
    }

    private void CreateDictionary()
    {
        _shipIdToRewards = new Dictionary<string, ShipRewards>();
        foreach (var rewards in _shipsRewards)
        {
            _shipIdToRewards.Add(rewards.ShipId, rewards);
        }
    }
    public ShipRewards GetRewards(string id)
    {
        if (_shipIdToRewards.TryGetValue(id, out var rewards))
            return rewards;

        return _defaultRwards;
        //throw new Exception($"Rewards from  {id} not found");
    }
    /*
    public ShipRewards GetRewards(TypeX type, string id)
    {
        if(_idToRewards.TryGetValue(type, out var rewardsFromType))
        {
            if (rewardsFromType.TryGetValue(id, out var rewards))
                return rewards;
            else
                throw new Exception($"Rewards from  {id} not found");
        }
        throw new Exception($"Rewards from type {type} not found");
    }
    */
}