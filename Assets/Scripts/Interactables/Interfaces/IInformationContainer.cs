using System;

public interface IInformationContainer
{
	event EventHandler StateChanged;
	string GetMainInformation();
	string GetSecondaryInformation();
}
