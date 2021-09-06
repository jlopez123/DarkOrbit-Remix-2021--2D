public class TargetInfo
{
    //PLAYERS - NPCS

    public string Name;
    public string Title;
    public int Rank;
    public Company Company;
    public Team Team;
    public TargetInfo(string name, string tittle,int rank, Company company)
    {
        Name = name;
        Title = tittle;
        Rank = rank;
        Company = company;
    }
}
