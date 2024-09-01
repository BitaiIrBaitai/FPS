public interface IInteractable : IInformationContainer
{
    bool Active { get; }
	void Interact();
}
