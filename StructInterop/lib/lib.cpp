// lib.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include  <Windows.h>

struct struct_basic
{
	WORD  value_1;
	LONG  value_2;
	DWORD value_3;
	UINT  value_4;
	BOOL  value_5;
};

struct struct_advanced
{
	WORD id;
	TCHAR message[256];
};

extern "C" __declspec(dllexport) void get_basic(struct_basic basic);

extern "C" __declspec(dllexport) void get_basic(struct_basic basic)
{
	basic.value_1 = 1;
	basic.value_2 = 2;
	basic.value_3 = 3;
	basic.value_4 = 4;
	basic.value_5 = TRUE;
}

extern "C" __declspec(dllexport) void get_advanced(struct_advanced* advanced);

extern "C" __declspec(dllexport) void get_advanced(struct_advanced* advanced)
{
	advanced->id = 101;
	lstrcpyW(advanced->message, L"HelloWorld");
}


