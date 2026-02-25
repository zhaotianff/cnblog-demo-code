// MonitorFile.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include<windows.h>

int main()
{
	// 打开目录, 获取文件句柄
	HANDLE hDirectory = ::CreateFile(L"D:\\PrintFiles\\", FILE_LIST_DIRECTORY,
		FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_EXISTING,
		FILE_FLAG_BACKUP_SEMANTICS, NULL);
	if (INVALID_HANDLE_VALUE == hDirectory)
	{
		return 0;
	}


	DWORD dwRet = 0;
	DWORD dwBufferSize = 2048;

	BYTE* pBuf = new BYTE[dwBufferSize];
	FILE_NOTIFY_INFORMATION* pFileNotifyInfo = (FILE_NOTIFY_INFORMATION*)pBuf;
	
	BOOL bRet = ReadDirectoryChangesW(hDirectory,
		pFileNotifyInfo, 
		dwBufferSize,
		TRUE,
		FILE_NOTIFY_CHANGE_FILE_NAME|           //修改文件名
		FILE_NOTIFY_CHANGE_ATTRIBUTES |			// 修改文件属性
		FILE_NOTIFY_CHANGE_LAST_WRITE,			// 最后一次写入
		&dwRet,
		NULL, NULL);

	if (FALSE == bRet)
	{
		DWORD dwError = GetLastError();
		std::cout << "ReadDirectoryChangesW failed - " << dwError << std::endl;
	}

	std::wcout.imbue(std::locale("chs"));

	//判断操作类型
	switch (pFileNotifyInfo->Action)
	{
	case FILE_ACTION_ADDED:
		 std::wcout << "Create file " << pFileNotifyInfo->FileName << std::endl;
		break;
	default:
		break;
	}

	CloseHandle(hDirectory);
	delete[] pBuf;
}

