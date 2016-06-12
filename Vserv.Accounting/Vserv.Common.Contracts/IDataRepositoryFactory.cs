namespace Vserv.Common.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataRepositoryFactory
    {
        /// <summary>
        /// Gets the data repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetDataRepository<T>() where T : IDataRepository;
    }
}
