// demo_lib.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include<iostream>

extern "C" __declspec(dllexport) void PrintArray(int* pa, int size);


extern "C" __declspec(dllexport) void PrintArray(int* pa, int size)
{
	for (size_t i = 0; i < size; i++)
	{
		std::cout << *pa << std::endl;
		pa++;
	}
}