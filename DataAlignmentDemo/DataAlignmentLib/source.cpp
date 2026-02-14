#include "pch.h"

#pragma pack(2)
struct struct_2
{
    char a;
    int b;
    short c;
    long long d;
};

extern "C" __declspec(dllexport) struct_2 Test();

extern "C" __declspec(dllexport) struct_2 Test()
{
    struct_2 ss{};
    
    ss.a = 'a';
    ss.b = 2;
    ss.c = 3;
    ss.d = 4;

    return ss;
}
