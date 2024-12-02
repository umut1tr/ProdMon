using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestLibrary
{
    public interface ITestRepository
    {
        Task<IEnumerable<string>> GetAllTestEntriesAsync();
    }
}
