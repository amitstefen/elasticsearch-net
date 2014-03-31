using System.Collections.Specialized;
using System.Threading.Tasks;
using Elasticsearch.Net.Serialization;

namespace Elasticsearch.Net.Connection
{
	public interface ITransport
	{
		IConnectionConfigurationValues Settings { get; }
		IElasticsearchSerializer Serializer { get; }
		
		ElasticsearchResponse<T> DoRequest<T>(
			string method, 
			string path, 
			object data = null, 
			IRequestParameters requestParameters = null);

		void Sniff(bool fromStartup = false);

		Task<ElasticsearchResponse<T>> DoRequestAsync<T>(
			string method, 
			string path, 
			object data = null, 
			IRequestParameters requestParameters = null);
	}

	public interface ITransportValues
	{
		IElasticsearchSerializer Serializer { get; }
	}
}