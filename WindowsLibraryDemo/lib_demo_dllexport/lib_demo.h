#pragma once

#ifdef indll
#define export_api __declspec(dllexport)
#else
#define export_api __declspec(dllimport)
#endif

//声明要导出的全局变量
export_api extern int global_variable;
export_api extern int global_variable_2;
