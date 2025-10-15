#include "pch.h"
#include "CppUnitTest.h"
#include "../Dll/math.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace DllTests
{
	TEST_CLASS(DllTests)
	{
	public:
		
		TEST_METHOD(Math)
		{
			auto result = sum(2, 2);
			Assert::IsTrue(result == 4);
		}
	};
}
