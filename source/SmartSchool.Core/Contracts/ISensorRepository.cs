namespace SmartSchool.Core.Contracts
{
    public interface ISensorRepository
    {
        (string name, string location, double avg)[] GetAllAvgSensors();
    }
}