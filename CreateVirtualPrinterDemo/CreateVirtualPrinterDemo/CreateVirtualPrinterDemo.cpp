// CreateVirtualPrinterDemo.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include<Windows.h>
#include<tchar.h>

/// <summary>
/// 安装Microsoft Print To PDF
/// </summary>
/// <returns></returns>
BOOL InstallMicrosoftPrintToPDF()
{
    LPWSTR szCmd = _tcsdup(LR"(dism /Online /Enable-Feature /FeatureName:"Printing-PrintToPDFServices-Features" /NoRestart /Quiet)");
    STARTUPINFO si{};
    PROCESS_INFORMATION pi{};
    si.cb = sizeof(si);
    auto nRet = CreateProcess(NULL, szCmd, NULL, NULL, FALSE, 0, NULL, NULL, &si, &pi);

    if (!nRet)
    {
        if (pi.hThread)
        {
            CloseHandle(pi.hThread);
        }

        if (pi.hProcess)
        {
            CloseHandle(pi.hProcess);
        }
    }

    free(szCmd);
    return nRet;
}

/// <summary>
/// 创建本地打印机端口
/// </summary>
/// <returns></returns>
BOOL CreateLocalPort()
{
    LPWSTR szPortName = _tcsdup(L"C:\\test.pdf");

    LPPORT_INFO_2 pPrtInfo2 = NULL;
    DWORD pcbNeeded = 0;
    DWORD pcReturned = 0;

    //枚举本地打印机端口
    EnumPorts(NULL, 2, NULL, 0, &pcbNeeded, &pcReturned);


    //枚举本地打印机端口
    pPrtInfo2 = (LPPORT_INFO_2)LocalAlloc(LPTR, pcbNeeded);
    auto result = EnumPorts(NULL, 2, (LPBYTE)pPrtInfo2, pcbNeeded, &pcbNeeded, &pcReturned);

    if (!result || pPrtInfo2 == NULL)
        return FALSE;

    for (int i = 0; i < pcReturned; i++)
    {
        if (wcscmp((pPrtInfo2 + i)->pPortName, szPortName) == 0)
            return TRUE;
    }

    HANDLE hPrinter = NULL;
    PRINTER_DEFAULTS printerDefaults{};
    printerDefaults.pDatatype = NULL;
    printerDefaults.pDevMode = NULL;
    printerDefaults.DesiredAccess = SERVER_ACCESS_ADMINISTER;

    LPWSTR szPrinterName = _tcsdup(L",XcvMonitor Local Port");

    result = OpenPrinter(szPrinterName, &hPrinter, &printerDefaults);

    if (!result || hPrinter == NULL)
    {
        //查看错误
        //GetLastError();
        return FALSE;
    }

    DWORD dwPcbNeeded = 0;
    DWORD dwStatus = 0;

    result = XcvData(hPrinter, L"AddPort", (PBYTE)szPortName, (lstrlenW(szPortName) + 1) * sizeof(TCHAR), NULL, 0, &dwPcbNeeded, &dwStatus);

    if (!result)
    {
        //GetLastError();
        return FALSE;
    }

    ClosePrinter(hPrinter);

    free(szPortName);
    free(szPrinterName);
}

/// <summary>
/// 根据Microsoft Print To PDF创建新虚拟打印机
/// </summary>
/// <returns></returns>
BOOL CreateVirtualPrinter()
{
    LPTSTR szPrinterName = _tcsdup(L"虚拟打印机");
    LPTSTR szPortName = _tcsdup(L"C:\\test.pdf");

    DWORD pcbNeeded = 0;
    DWORD pcReturned = 0;

    auto result = EnumPrinters(PRINTER_ENUM_LOCAL | PRINTER_ENUM_NAME, NULL, 2, NULL, 0, &pcbNeeded, &pcReturned);
    LPPRINTER_INFO_2 pPrtInfo2 = (LPPRINTER_INFO_2)LocalAlloc(LPTR, pcbNeeded);
    result = EnumPrinters(PRINTER_ENUM_LOCAL | PRINTER_ENUM_NAME, NULL, 2, (LPBYTE)pPrtInfo2, pcbNeeded, &pcbNeeded, &pcReturned);

    if (!result || pPrtInfo2 == NULL)
        return FALSE;

    for (int i = 0; i < pcReturned; i++)
    {
        LPPRINTER_INFO_2 p_curPrtInfo = pPrtInfo2 + i;
        if (wcscmp(p_curPrtInfo->pPrinterName, L"Microsoft Print to PDF") == 0)
        {
            p_curPrtInfo->pPrinterName = szPrinterName;
            HANDLE hPrinter = AddPrinter(NULL, 2, (LPBYTE)p_curPrtInfo);
            LPPRINTER_INFO_2 p_tmpPrtInfo = NULL;
            DWORD dw_tmpNeeded = 0;
            GetPrinter(hPrinter, 2, (LPBYTE)p_tmpPrtInfo, dw_tmpNeeded, &dw_tmpNeeded);
            p_tmpPrtInfo = (LPPRINTER_INFO_2)LocalAlloc(LPTR, dw_tmpNeeded);
            result = GetPrinter(hPrinter, 2, (LPBYTE)p_tmpPrtInfo, dw_tmpNeeded, &dw_tmpNeeded);

            if (p_tmpPrtInfo == NULL || result == FALSE)
                return FALSE;

            p_tmpPrtInfo->pPortName = szPortName;
            result = SetPrinter(hPrinter, 2, (LPBYTE)p_tmpPrtInfo, 0);
            ClosePrinter(hPrinter);
            SetDefaultPrinter(szPrinterName);
            break;
        }
    }

    free(szPrinterName);
    free(szPortName);

    return TRUE;
}

int main()
{
    InstallMicrosoftPrintToPDF();
    CreateLocalPort();
    CreateVirtualPrinter();
}
