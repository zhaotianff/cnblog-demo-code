#include <Windows.h>
#include<msclr/marshal_cppstd.h>
#include"interop.h"

//引用C# dll
#using "./CSharpLib.dll"

//引用命名空间
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

//导出函数 供C++调用
//在这个函数里调用 C#的函数，做为中转层
cs_lib_api int GetID()
{
	CSharpLib::ExplortClass^ c = gcnew CSharpLib::ExplortClass(); //可以只用一个对象，这里仅做演示
	auto id = c->GetID();
	return id;
}

cs_lib_api interop_Computer GetComputer()
{
	CSharpLib::ExplortClass^ c = gcnew CSharpLib::ExplortClass();
	auto computer = c->GetComputer(); //调用C#中的函数
	System::IntPtr ptr = Marshal::AllocHGlobal(sizeof(interop_Computer));//需要提前分配空间
	System::Runtime::InteropServices::Marshal::StructureToPtr(computer, ptr, false);//将C#中的结构体拷贝到Intptr
	interop_Computer* rt = (interop_Computer*)(void*)(ptr.ToPointer());//将Intptr强制转换为interop_Computer
	Marshal::FreeHGlobal(ptr);//释放IntPtr
	return *rt;
}

cs_lib_api void PrintComputer(interop_Computer computer)
{
	CSharpLib::ExplortClass^ c = gcnew CSharpLib::ExplortClass();
	auto printDelegate = c->GetComputerDelegate();//获取委托 
	IntPtr ptr = Marshal::GetFunctionPointerForDelegate(printDelegate);//将委托转为IntPtr类型
	funPrintComputer funcPrint = (funPrintComputer)ptr.ToPointer();//将IntPtr转换为指针，再转换为funPrintComputer
	if (funcPrint)
	{
		funcPrint(computer); //调用C#中的函数
	}
}