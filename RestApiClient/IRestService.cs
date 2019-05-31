using System.Threading.Tasks;

namespace RestApiClient
{
    public interface IRestService
    {
        TResult Login<TResult>(string uri, string userName, string password);
        TResult GetData<TResult>(string url);
        Task<TResult> GetDataAsync<TResult>(string url);
        bool PostData<TData>(string url, TData data);
        TResult PostData<TData, TResult>(string url, TData data);
        void PostData(string url);
        TResult PutData<TData, TResult>(string url, TData data);
        void PutData<TData>(string url, TData data);
        TResult PutData<TResult>(string url);
        bool DeleteData(string url);
    }
}