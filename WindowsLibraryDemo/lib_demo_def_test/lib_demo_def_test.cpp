// lib_demo_def_test.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>

extern int global_variable;  //因为没有用__declspec(dllimport)，所以认为global_variable为指针
extern int __declspec(dllimport) global_variable_2;

int main()
{
    //输出导出变量
    std::cout << *(int*)global_variable << std::endl;

    //赋值操作
    *(int*)global_variable = 88;

    //输出赋值后的值 
    std::cout << *(int*)global_variable << std::endl;


    //输出导出变量
    std::cout << global_variable_2 << std::endl;

    //赋值操作
    global_variable_2 = 77;

    //输出赋值后的值 
    std::cout << global_variable_2 << std::endl;
}