using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Get7
{
	public partial class Get7YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class RealtimeRefresh1Tests : YamlTestsBase
		{
			[Test]
			public void RealtimeRefresh1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							refresh_interval= "-1",
							number_of_replicas= "0"
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.Add("wait_for_status", @"green")
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.Add("realtime", 1)
				));

				//is_true _response.found; 
				this.IsTrue(_response.found);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.Add("realtime", 0)
				), shouldCatch: @"missing");

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.Add("realtime", 0)
					.Add("refresh", 1)
				));

				//is_true _response.found; 
				this.IsTrue(_response.found);

			}
		}
	}
}

