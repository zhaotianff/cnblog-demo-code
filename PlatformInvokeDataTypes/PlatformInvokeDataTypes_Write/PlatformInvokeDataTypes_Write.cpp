// PlatformInvokeDataTypes_Write.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include<Windows.h>
#include<iostream>
#define MemorySize 1024

void WriteWithoutOffset();
void WriteWithOffset();

int main()
{
	
	//测试没有指针偏移的情况
	WriteWithoutOffset();

	//测试有指针偏移的情况
	WriteWithOffset();

	std::cout << "写入成功" << std::endl;
	std::cin.get();

	
    return 0;
}

/**
* 在C#中使用IntPtr的情况（没有指针偏移）
*/
void WriteWithoutOffset()
{
	RECT rect{ 100,100,100,100 };
	auto len = sizeof(rect);

	HANDLE m_handle = CreateFileMappingA(INVALID_HANDLE_VALUE, NULL, PAGE_EXECUTE_READWRITE, 0, MemorySize, "HelloWorld");
	if (m_handle != NULL)
	{
		PVOID m_pView = MapViewOfFile(m_handle, FILE_MAP_ALL_ACCESS, 0, 0, MemorySize);
		if (m_pView != NULL)
		{
			memcpy_s(m_pView, MemorySize, (void *)&rect, len);		

			//程序退出时的资源释放操作
			//.......
		}
	}
}

/**
* 在C#中使用指针的情况（有指针偏移）
*/
void WriteWithOffset()
{
	RECT rect{ 10,10,10,10 };
	POINT point{ 3,3 };
	auto len = sizeof(rect);

	HANDLE m_handle = CreateFileMappingA(INVALID_HANDLE_VALUE, NULL, PAGE_EXECUTE_READWRITE, 0, MemorySize, "HelloWorld2");
	if (m_handle != NULL)
	{
		PVOID m_pView = MapViewOfFile(m_handle, FILE_MAP_ALL_ACCESS, 0, 0, MemorySize);
		if (m_pView != NULL)
		{
			//先写入rect
			memcpy_s(m_pView, MemorySize,(void *)&rect, len);

			char* p = (char *)m_pView;
			p = p + len;
			m_pView = (void *)p;

			//再写入Point
			memcpy_s(m_pView, MemorySize,(void *)&point, sizeof(point));
			
			//程序退出时的资源释放操作
			//.......
		}
	}
}

