using System;

public class MapInputDetectionModel : IMapInputDetectionModel
{
    public event Action<InputType> OnMainActionTriggered;
    
    public void MainActionTrigger (InputType inputType)
    {
        OnMainActionTriggered?.Invoke(inputType);
    }
}