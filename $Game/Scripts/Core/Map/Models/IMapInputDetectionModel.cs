using System;

public interface IMapInputDetectionModel
{
    event Action OnMainActionBlocked;
    event Action<InputType> OnMainActionTriggered;

    void MainActionTrigger (InputType inputType);
}