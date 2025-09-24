// CppInvoke.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include<Windows.h>
#include<tchar.h>
#include"../CSBridge/interop.h"

int main()
{
    HMODULE hInstance = LoadLibrary(L"CSBridge.dll");
    if (hInstance)
    {
        funGetId getId = (funGetId)GetProcAddress(hInstance, "GetID");

        if (getId)
        {
            auto result = getId();
            std::cout << result << std::endl;
        }

        funGetComputer getComputer = (funGetComputer)GetProcAddress(hInstance, "GetComputer");
         

        if (getComputer)
        {
            auto computer = getComputer();
            std::wcout << computer.cpuId << "\t" << computer.cpuName << "\t" << computer.osVersion << std::endl;
        }

        funPrintComputer printComputer = (funPrintComputer)GetProcAddress(hInstance, "PrintComputer");
        interop_Computer testComputer;
        testComputer.cpuId = 18;
        testComputer.cpuName = _tcsdup(L"AMD");
        testComputer.osVersion = 7;
        if (printComputer)
        {
            printComputer(testComputer);
        }

        FreeLibrary(hInstance);
    }
}
