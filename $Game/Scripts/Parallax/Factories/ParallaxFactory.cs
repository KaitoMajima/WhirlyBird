public static class ParallaxFactory
{
    public static IParallaxManagerModel CreateParallaxManagerModel (IPillarManagerModel pillarManagerModel) 
        => new ParallaxManagerModel(pillarManagerModel);
}