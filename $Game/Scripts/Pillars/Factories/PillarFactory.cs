public static class PillarFactory
{
    public static IPillarManagerModel CreatePillarManagerModel () 
        => new PillarManagerModel();

    public static IPillarModel CreatePillarModel () 
        => new PillarModel();
}