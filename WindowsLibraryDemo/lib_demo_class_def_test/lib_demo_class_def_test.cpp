// lib_demo_class_def_test.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include"../lib_demo_class_def/lib_demo.h"

#pragma comment(lib,"../Debug/lib_demo_class_def.lib")

int main()
{
    CLibTest test;
    auto test_result = test.TestMethod1();
    auto test_restlt_2 = test.TestMethod2();

    std::cout << test_result << std::endl;
    std::cout << test_restlt_2 << std::endl;
}