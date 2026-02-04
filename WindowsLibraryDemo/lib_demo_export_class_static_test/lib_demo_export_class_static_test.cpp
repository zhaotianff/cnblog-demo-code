// lib_demo_export_class_static_test.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include<stdlib.h>
#include "../lib_demo_export_class_static/lib_demo.h"

#pragma comment(lib,"../Debug/lib_demo_export_class_static.lib")





int main()
{
    std::cout.precision(7);
    std::cout << CLibTest::ID << std::endl;
}
