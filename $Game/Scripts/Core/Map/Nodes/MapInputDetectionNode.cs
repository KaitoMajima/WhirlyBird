using Godot;

public partial class MapInputDetectionNode : Node, IMapInputDetectionNode
{
    [Export] 
    InputEventAction mainActionInputEvent;

    IMapInputDetectionModel inputDetectionModel;
    
    public void Setup (IMapInputDetectionModel inputDetectionModel)
    {
        this.inputDetectionModel = inputDetectionModel;
    }
    
    public override void _Process (double delta)
    {
        if (Input.IsActionPressed(mainActionInputEvent.Action))
            inputDetectionModel.MainActionTrigger(InputType.Pressed);
        
        if (Input.IsActionJustPressed(mainActionInputEvent.Action))
            inputDetectionModel.MainActionTrigger(InputType.JustPressed);
        
        if (Input.IsActionJustReleased(mainActionInputEvent.Action))
            inputDetectionModel.MainActionTrigger(InputType.JustReleased);
    }
}