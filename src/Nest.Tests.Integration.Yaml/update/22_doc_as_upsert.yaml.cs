using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Update
{
	public partial class Update22DocAsUpsertYaml22Tests
	{
		
		public class DocAsUpsert22Tests
		{
			private readonly RawElasticClient _client;
		
			public DocAsUpsert22Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void DocAsUpsertTests()
			{

				//do update 
				this._client.UpdatePost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);

				//do get 
				this._client.Get("test_1", "test", "1", nv=>nv);

				//do update 
				this._client.UpdatePost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);

				//do get 
				this._client.Get("test_1", "test", "1", nv=>nv);
			}
		}
	}
}