// demo_lib.cpp : ���� DLL Ӧ�ó���ĵ���������
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