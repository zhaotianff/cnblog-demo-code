// lib_demo_dllexport_test.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include "../lib_demo_dllexport/lib_demo.h"

#pragma comment(lib,"..\\Debug\\lib_demo_dllexport.lib")

int main()
{
    std::cout << global_variable << std::endl;
    std::cout << global_variable_2 << std::endl;

    global_variable = 88;
    global_variable_2 = 99;

    std::cout << global_variable << std::endl;
    std::cout << global_variable_2 << std::endl;
}
