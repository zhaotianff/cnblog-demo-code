// UnionLib.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include<iostream>

union MYUNION
{
	int b;
	double d;
};

union MYUNION2
{
	int i;
	char str[128];
};

struct MYSTRUCTUNION
{
	UINT uType;
	union
	{
		LPWSTR pStr;
		char cStr[260];
	};
};


extern "C" __declspec(dllexport) MYUNION GetMyUnion();

extern "C" __declspec(dllexport) MYUNION GetMyUnion()
{
	MYUNION  myunion;	
	myunion.b = 10;
	return myunion;
}

extern "C" __declspec(dllexport) MYUNION2 GetMyUnion2();

extern "C" __declspec(dllexport) MYUNION2 GetMyUnion2()
{
	MYUNION2  myunion2;
	strcpy_s(myunion2.str, 11, "HelloWorld");
	return myunion2;
}

extern "C" __declspec(dllexport) void TestUnion2(MYUNION2 u, int type);

extern "C" __declspec(dllexport) void TestUnion2(MYUNION2 u, int type)
{
	if (type == 1)
	{
		std::cout << u.i << std::endl;
	}
	else
	{
		std::cout << u.str << std::endl;
	}
}

extern "C" __declspec(dllexport) MYSTRUCTUNION GetMyUnion3();

extern "C" __declspec(dllexport) MYSTRUCTUNION GetMyUnion3()
{
	MYSTRUCTUNION myunion;
	myunion.uType = 0;
	myunion.pStr = L"HelloWorld";
	return myunion;
}
	