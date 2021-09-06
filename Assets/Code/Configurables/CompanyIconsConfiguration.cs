using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CompanyIconsConfiguration", menuName = "Configurables/Create Company Icons Configuration", order = 0)]
public class CompanyIconsConfiguration : ScriptableObject
{
    [SerializeField]
    private List<Company> _companys;
    [SerializeField]
    private List<Sprite> _sprites;

    private Dictionary<Company, Sprite> _companyToSprite;

    private void Awake()
    {
        CreateDictionary();
    }

    private void CreateDictionary()
    {
        _companyToSprite = new Dictionary<Company, Sprite>();
        int index = 0;

        foreach (var companu in _companys)
        {
            _companyToSprite.Add(companu, _sprites[index]);
            index++;
        }
    }

    public Sprite GetCompanySprite(Company company)
    {
        if (_companyToSprite.TryGetValue(company, out var sprite))
            return sprite;

        throw new Exception($"Company  {company} Icon not found");

    }
}