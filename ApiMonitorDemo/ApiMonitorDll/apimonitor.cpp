#include<Windows.h>

extern "C" __declspec(dllexport) void MessageBoxShow(LPTSTR str)
{
	MessageBox(NULL, str, L"title", MB_OK);
}