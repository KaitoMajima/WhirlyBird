using System;

public class MapInputDetectionModel : IMapInputDetectionModel
{
    public event Action OnMainActionBlocked;
    public event Action<InputType> OnMainActionTriggered;
    
    readonly IPauseModel pauseModel;

    public MapInputDetectionModel (IPauseModel pauseModel)
    {
        this.pauseModel = pauseModel;
    }
    
    public void MainActionTrigger (InputType inputType)
    {
        if (pauseModel.IsPaused)
        {
            if (inputType == InputType.JustPressed)
                OnMainActionBlocked?.Invoke();
            return;
        }
        
        OnMainActionTriggered?.Invoke(inputType);
    }
}