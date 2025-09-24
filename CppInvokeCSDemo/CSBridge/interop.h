#pragma once

//这里用放用于交互的数据类型

struct interop_Computer
{
    int cpuId;
    wchar_t* cpuName;
    int osVersion;
};

typedef int(__stdcall* funGetId)();  //定义函数指针
typedef interop_Computer(__stdcall* funGetComputer)();
typedef void(__stdcall* funPrintComputer)(interop_Computer computer);
