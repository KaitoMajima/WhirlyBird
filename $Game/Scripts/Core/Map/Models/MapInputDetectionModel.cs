using System;

public class MapInputDetectionModel : IMapInputDetectionModel
{
    public event Action<InputType> OnMainActionTriggered;
    
    readonly IPauseModel pauseModel;

    public MapInputDetectionModel (IPauseModel pauseModel)
    {
        this.pauseModel = pauseModel;
    }
    
    public void MainActionTrigger (InputType inputType)
    {
        if (pauseModel.IsPaused)
            return;
        
        OnMainActionTriggered?.Invoke(inputType);
    }
}