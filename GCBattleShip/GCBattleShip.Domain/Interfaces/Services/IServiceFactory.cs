namespace GCBattleShip.Domain.Interfaces.Services
{
    public interface IServiceFactory <out T> where T : class
    {
        T Build();
    }
}