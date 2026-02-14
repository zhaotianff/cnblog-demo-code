// structdemo.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>

struct struct_1
{
    char a;
    int b;
    short c;
    long long d;
};

#pragma pack(2)
struct struct_2
{
    char a;
    int b;
    short c;
    long long d;
};


int main()
{
    
    std::cout << offsetof(struct_1,a) << std::endl;
    std::cout << offsetof(struct_1, b) << std::endl;
    std::cout << offsetof(struct_1, c) << std::endl;
    std::cout << offsetof(struct_1, d) << std::endl;
    std::cout << "size:"<<sizeof(struct_1) << std::endl;
    std::cout << std::endl;
    std::cout << offsetof(struct_2, a) << std::endl;
    std::cout << offsetof(struct_2, b) << std::endl;
    std::cout << offsetof(struct_2, c) << std::endl;
    std::cout << offsetof(struct_2, d) << std::endl;
    std::cout << "size:"<<sizeof(struct_2) << std::endl;
}

