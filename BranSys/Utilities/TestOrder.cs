using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace BranSys.Utilities
{
    public class TestOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases.OrderBy(testCase =>
            {
                var priorityAttribute = testCase.TestMethod.Method
                    .GetCustomAttributes(typeof(TestPriorityAttribute))
                    .FirstOrDefault();
                return priorityAttribute == null ? 0 : priorityAttribute.GetNamedArgument<int>("Priority");
            });
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestPriorityAttribute : Attribute
    {
        public int Priority { get; }
        public TestPriorityAttribute(int priority) => Priority = priority;
    }
}
