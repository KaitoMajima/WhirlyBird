using System;

public interface IMapInputDetectionModel
{
    event Action<InputType> OnMainActionTriggered;

    void MainActionTrigger (InputType inputType);
}