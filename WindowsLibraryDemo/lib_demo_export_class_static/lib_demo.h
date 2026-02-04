#pragma once

#ifdef indll
#define export_api __declspec(dllexport)
#else
#define export_api __declspec(dllimport)
#endif

class CLibTest
{
public:
	CLibTest();
	~CLibTest();

public:
	static double ID;   //定义一个类静态变量
};