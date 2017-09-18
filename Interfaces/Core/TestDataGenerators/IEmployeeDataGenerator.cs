using System.Collections.Generic;

using Interfaces.Sql.Entities;

namespace Interfaces.Core.TestDataGenerators
{
	public interface IEmployeeDataGenerator
	{
		IEnumerable<IQuestion> Generate(int count);
	}
}
