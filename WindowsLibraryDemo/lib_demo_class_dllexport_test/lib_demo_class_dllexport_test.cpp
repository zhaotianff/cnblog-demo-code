// lib_demo_class_dllexport_test.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include"../lib_demo_class_dllexport/lib_demo.h"

#pragma comment(lib,"../Debug/lib_demo_class_dllexport.lib")

int main()
{
    CLibTest test;
    auto result = test.TestMethod1();
    auto result_2 = test.TestMethod2();

    std::cout << result << std::endl;
    std::cout << result_2 << std::endl;
}