#include "pch.h"
#include "CppUnitTest.h"
#include "../ConsoleApp/math.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace ConsoleAppTests
{
	TEST_CLASS(ConsoleAppTests)
	{
	public:
		
		TEST_METHOD(TestMath)
		{
			math ma;
			auto result = ma.sum(2, 2);

			Assert::IsTrue(result == 4);
		}
	};
}
