#pragma once

//�����÷����ڽ�������������

struct interop_Computer
{
    int cpuId;
    wchar_t* cpuName;
    int osVersion;
};

typedef int(__stdcall* funGetId)();  //���庯��ָ��
typedef interop_Computer(__stdcall* funGetComputer)();
typedef void(__stdcall* funPrintComputer)(interop_Computer computer);
