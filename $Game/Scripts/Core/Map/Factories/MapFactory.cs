using Godot;

public static class MapFactory
{
    public static IMapModel CreateMapModel ()
    {
        IMapUICanvasModel mapUICanvasModel = CreateMapUICanvasModel();
        IMapWorld2DModel mapWorld2DModel = CreateMapWorld2DModel();

        return new MapModel(mapUICanvasModel, mapWorld2DModel);
    }

    public static IMapNode CreateMapNode (
        Node callerNode,
        NodePath mapNodePath,
        NodePath mapUICanvasNodePath,
        NodePath mapWorld2dNodePath
    )
    {
        IMapUICanvasNode mapUICanvasNode = CreateMapUICanvasNode(callerNode, mapUICanvasNodePath);
        IMapWorld2DNode mapWorld2DNode = CreateMapWorld2DNode(callerNode, mapWorld2dNodePath);
        IMapNode mapNode = callerNode.GetNode<MapNode>(mapNodePath);
        mapNode.Setup(mapUICanvasNode, mapWorld2DNode);
        return mapNode;
    }

    static IMapUICanvasModel CreateMapUICanvasModel () => new MapUICanvasModel();
    
    static IMapWorld2DModel CreateMapWorld2DModel () => new MapWorld2DModel();
    
    static IMapUICanvasNode CreateMapUICanvasNode (
        Node callerNode,
        NodePath mapUICanvasNodePath
    )
    {
        IMapUICanvasNode mapUICanvasNode = callerNode.GetNode<MapUICanvasNode>(mapUICanvasNodePath);
        return mapUICanvasNode;
    }

    static IMapWorld2DNode CreateMapWorld2DNode (
        Node callerNode,
        NodePath mapWorld2dNodePath
    )
    {
        IMapWorld2DNode mapWorld2DNode = callerNode.GetNode<MapWorld2DNode>(mapWorld2dNodePath);
        return mapWorld2DNode;
    }
}