using Godot;

public static class MapFactory
{
    public static IMapModel CreateMapModel (
        ITimeProvider timeProvider, 
        MapSettingsResource mapSettingsResource
    )
    {
        IMapUICanvasModel mapUICanvasModel = CreateMapUICanvasModel(timeProvider);
        IMapWorld2DModel mapWorld2DModel = CreateMapWorld2DModel(mapSettingsResource);

        return new MapModel(mapUICanvasModel, mapWorld2DModel);
    }

    public static IMapNode CreateMapNode (
        Node callerNode,
        NodePath mapNodePath,
        NodePath mapUICanvasNodePath,
        NodePath mapWorld2dNodePath,
        IMapUICanvasModel mapUICanvasModel,
        IMapWorld2DModel mapWorld2DModel
    )
    {
        IMapUICanvasNode mapUICanvasNode = CreateMapUICanvasNode(
            callerNode, 
            mapUICanvasNodePath, 
            mapUICanvasModel
        );
        IMapWorld2DNode mapWorld2DNode = CreateMapWorld2DNode(
            callerNode, 
            mapWorld2dNodePath,
            mapWorld2DModel
        );
        IMapNode mapNode = callerNode.GetNode<MapNode>(mapNodePath);
        mapNode.Setup(mapUICanvasNode, mapWorld2DNode);
        return mapNode;
    }

    static IMapUICanvasModel CreateMapUICanvasModel (ITimeProvider timeProvider)
    {
        IPauseModel pauseModel = new PauseModel(timeProvider);
        return new MapUICanvasModel(pauseModel);
    }

    static IMapWorld2DModel CreateMapWorld2DModel (MapSettingsResource mapSettingsResource)
    {
        IPlayerSettings playerSettings = PlayerFactory.CreatePlayerSettings(mapSettingsResource);
        IPlayerModel playerModel = PlayerFactory.CreatePlayerModel(playerSettings);
        return new MapWorld2DModel(playerModel);
    }

    static IMapUICanvasNode CreateMapUICanvasNode (
        Node callerNode,
        NodePath mapUICanvasNodePath,
        IMapUICanvasModel mapUICanvasModel
    )
    {
        IMapUICanvasNode mapUICanvasNode = callerNode.GetNode<MapUICanvasNode>(mapUICanvasNodePath);
        mapUICanvasNode.Setup(mapUICanvasModel.PauseModel);
        return mapUICanvasNode;
    }

    static IMapWorld2DNode CreateMapWorld2DNode (
        Node callerNode,
        NodePath mapWorld2dNodePath,
        IMapWorld2DModel mapWorld2DModel
    )
    {
        IMapWorld2DNode mapWorld2DNode = callerNode.GetNode<MapWorld2DNode>(mapWorld2dNodePath);
        mapWorld2DNode.Setup(mapWorld2DModel.PlayerModel);
        return mapWorld2DNode;
    }
}