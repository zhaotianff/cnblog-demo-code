#include <Windows.h>
#include<msclr/marshal_cppstd.h>
#include"interop.h"

//����C# dll
#using "./CSharpLib.dll"

//���������ռ�
using namespace msclr::interop;
using namespace System;
using namespace System::Runtime::InteropServices;
using namespace CSharpLib;

#define lib_export
#ifdef lib_export
#define cs_lib_api extern "C" __declspec(dllexport)
#else
#define cs_lib_api __declspec(dllimport)
#endif

//�������� ��C++����
//�������������� C#�ĺ�������Ϊ��ת��
cs_lib_api int GetID()
{
	CSharpLib::ExplortClass^ c = gcnew CSharpLib::ExplortClass(); //����ֻ��һ���������������ʾ
	auto id = c->GetID();
	return id;
}

cs_lib_api interop_Computer GetComputer()
{
	CSharpLib::ExplortClass^ c = gcnew CSharpLib::ExplortClass();
	auto computer = c->GetComputer(); //����C#�еĺ���
	System::IntPtr ptr = Marshal::AllocHGlobal(sizeof(interop_Computer));//��Ҫ��ǰ����ռ�
	System::Runtime::InteropServices::Marshal::StructureToPtr(computer, ptr, false);//��C#�еĽṹ�忽����Intptr
	interop_Computer* rt = (interop_Computer*)(void*)(ptr.ToPointer());//��Intptrǿ��ת��Ϊinterop_Computer
	Marshal::FreeHGlobal(ptr);//�ͷ�IntPtr
	return *rt;
}

cs_lib_api void PrintComputer(interop_Computer computer)
{
	CSharpLib::ExplortClass^ c = gcnew CSharpLib::ExplortClass();
	auto printDelegate = c->GetComputerDelegate();//��ȡί�� 
	IntPtr ptr = Marshal::GetFunctionPointerForDelegate(printDelegate);//��ί��תΪIntPtr����
	funPrintComputer funcPrint = (funPrintComputer)ptr.ToPointer();//��IntPtrת��Ϊָ�룬��ת��ΪfunPrintComputer
	if (funcPrint)
	{
		funcPrint(computer); //����C#�еĺ���
	}
}