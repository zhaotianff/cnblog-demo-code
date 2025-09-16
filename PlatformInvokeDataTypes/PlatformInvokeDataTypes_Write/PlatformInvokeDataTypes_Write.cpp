// PlatformInvokeDataTypes_Write.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include<Windows.h>
#include<iostream>
#define MemorySize 1024

void WriteWithoutOffset();
void WriteWithOffset();

int main()
{
	
	//����û��ָ��ƫ�Ƶ����
	WriteWithoutOffset();

	//������ָ��ƫ�Ƶ����
	WriteWithOffset();

	std::cout << "д��ɹ�" << std::endl;
	std::cin.get();

	
    return 0;
}

/**
* ��C#��ʹ��IntPtr�������û��ָ��ƫ�ƣ�
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

			//�����˳�ʱ����Դ�ͷŲ���
			//.......
		}
	}
}

/**
* ��C#��ʹ��ָ����������ָ��ƫ�ƣ�
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
			//��д��rect
			memcpy_s(m_pView, MemorySize,(void *)&rect, len);

			char* p = (char *)m_pView;
			p = p + len;
			m_pView = (void *)p;

			//��д��Point
			memcpy_s(m_pView, MemorySize,(void *)&point, sizeof(point));
			
			//�����˳�ʱ����Դ�ͷŲ���
			//.......
		}
	}
}

