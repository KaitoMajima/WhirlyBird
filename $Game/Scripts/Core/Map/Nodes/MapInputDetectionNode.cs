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

    public void Initialize ()
    {
        AddModelListeners();
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
    
    void HandleMainActionBlocked ()
    {
        Input.ActionRelease(mainActionInputEvent.Action);
    }
    
    void AddModelListeners ()
    {
        inputDetectionModel.OnMainActionBlocked += HandleMainActionBlocked;
    }
    
    void RemoveModelListeners ()
    {
        inputDetectionModel.OnMainActionBlocked -= HandleMainActionBlocked;
    }

    public new void Dispose ()
    {
        RemoveModelListeners();
        base.Dispose();
    }
}