using System;

public static class Events
{
    public static event EventHandler<IMachine> MachineCreatedEvent;

    public static void OnMachineCreated(object sender, IMachine machine)
    {
        MachineCreatedEvent?.Invoke(sender, machine);
    }
}
